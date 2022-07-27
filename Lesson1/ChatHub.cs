using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lesson1_Core;
using Lesson1_BL.Services.AuthService;
using Lesson1_BL.Auth;
using Lesson1_BL.ChatEntites;
using System.Security.Claims;
using System.Linq;

namespace Lesson1_BL
{
    public class ChatHub : Hub<IClientHub>, IServerHub
    {
        private static List<Room> _chatRooms;
        private readonly IAuthService _authService;
        private readonly ITokenGenerator _tokenGenerator;

        private Func<Room, bool> ByReader = x => x.Reader == null;
        private Func<Room, bool> ByLibrarian = x => x.Librarian == null;

        static ChatHub()
        {
            _chatRooms = new List<Room>();
        }

        public ChatHub(
            IAuthService authService,
            ITokenGenerator tokenGenerator)
        {
            _authService = authService;
            _tokenGenerator = tokenGenerator;
        }       

        public override async Task OnDisconnectedAsync(Exception exception)
        {            
            var room = GetRoom(Context.ConnectionId);
            if (room != null)
            {
                var role = room.Librarian != null && room.Librarian.ConnectionId == Context.ConnectionId
                    ? Roles.Librarian : Roles.Reader;
                var targetId = room.Reader != null && room.Reader.ConnectionId == Context.ConnectionId
                    ? (room.Librarian != null ? room.Librarian.ConnectionId : string.Empty)
                    : (room.Reader != null ? room.Reader.ConnectionId : string.Empty);
                var name = role == Roles.Librarian ? room.Librarian.Name : room.Reader.Name;
                await Clients.Client(targetId).ReceiveMessage(name, "has been disconnected!");
                if (role == Roles.Reader)
                {
                    room.Reader = null;
                }
                else if(role == Roles.Librarian)
                {
                    room.Librarian = null;
                }
                if(room.Librarian == null
                    && room.Reader == null)
                {
                    _chatRooms.Remove(room);
                }
            }
        }

        public async Task SendMessage(string message)
        {
            var room = GetRoom(Context.ConnectionId);
            if (room != null
                && room.Reader != null
                && room.Librarian != null)
            {
                var name = room.Reader.ConnectionId == Context.ConnectionId
                    ? room.Reader.Name : room.Librarian.Name;
                var targetId = room.Reader.ConnectionId == Context.ConnectionId
                    ? room.Librarian.ConnectionId : room.Reader.ConnectionId;

                await Clients.Client(targetId).ReceiveMessage(name, message);
            }            
        }

        public async Task<bool> SignIn(string username, string password)
        {
            try
            {
                var token = await _authService.SignIn(username, password);
                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }
                var role = _tokenGenerator.GetClaimValueFromToken(
                    token,
                    ClaimsIdentity.DefaultRoleClaimType.ToString());
                var name = _tokenGenerator.GetClaimValueFromToken(
                    token,
                    ClaimsIdentity.DefaultNameClaimType.ToString());
                var predicate = role == Roles.Reader ? ByReader : ByLibrarian;
                var room = _chatRooms.FirstOrDefault(predicate);
                if(room == null)
                {
                    room = new Room();
                    _chatRooms.Add(room);
                }
                if (role == Roles.Reader)
                {
                    room.Reader = new ConnectionUser
                    {
                        ConnectionId = Context.ConnectionId,
                        Name = name
                    };
                    
                }
                else if (role == Roles.Librarian)
                {
                    room.Librarian = new ConnectionUser
                    {
                        ConnectionId = Context.ConnectionId,
                        Name = name
                    };
                }
                if(room.Librarian != null
                    && room.Reader != null)
                {
                    await Clients.Clients(new List<string> { room.Reader.ConnectionId, room.Librarian.ConnectionId })
                        .ReceiveSystemMessage("You're added to chat room!");
                    await Clients.Client(room.Reader.ConnectionId)
                        .ReceiveSystemMessage("You're speaking with " + room.Librarian.Name);
                    await Clients.Client(room.Librarian.ConnectionId)
                        .ReceiveSystemMessage("You're speaking with " + room.Reader.Name);
                }
                else
                {
                    var connectionId = role == Roles.Reader
                        ? room.Reader.ConnectionId : room.Librarian.ConnectionId;
                    await Clients.Client(connectionId)
                        .ReceiveSystemMessage("You're added to waiting list");
                }
                return true;
            }
            catch
            {
                return false;
            }            
        }

        private Room GetRoom(string connectionId)
        {
            return _chatRooms.FirstOrDefault(x =>
                (x.Librarian != null && x.Librarian.ConnectionId == connectionId)
                || (x.Reader != null && x.Reader.ConnectionId == connectionId));
        }
    }
}
