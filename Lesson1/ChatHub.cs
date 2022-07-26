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

        private Func<Room, bool> ByReader = x => string.IsNullOrEmpty(x.ReaderConnectionId);
        private Func<Room, bool> ByLibrarian = x => string.IsNullOrEmpty(x.LibrarianConnectionId);

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

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.ReceiveMessage(Context.ConnectionId, " has been connected!");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.ReceiveMessage(Context.ConnectionId, " has been disconnected!");
        }

        public async Task SendMessage(string message)
        {
            var room = _chatRooms.FirstOrDefault(x =>
                x.LibrarianConnectionId == Context.ConnectionId
                || x.ReaderConnectionId == Context.ConnectionId);
            if(room != null)
            {
                var role = room.ReaderConnectionId == Context.ConnectionId
                    ? Roles.Reader : Roles.Librarian;
                var targetId = room.ReaderConnectionId == Context.ConnectionId
                    ? room.LibrarianConnectionId : room.ReaderConnectionId;

                await Clients.Client(targetId).ReceiveMessage(role, message);
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
                var predicate = role == Roles.Reader ? ByReader : ByLibrarian;
                var room = _chatRooms.FirstOrDefault(predicate);
                if(room == null)
                {
                    room = new Room();
                    _chatRooms.Add(room);
                }
                if (role == Roles.Reader)
                {
                    room.ReaderConnectionId = Context.ConnectionId;
                }
                else if (role == Roles.Librarian)
                {
                    room.LibrarianConnectionId = Context.ConnectionId;
                }
                if(!string.IsNullOrEmpty(room.LibrarianConnectionId) 
                    && !string.IsNullOrEmpty(room.ReaderConnectionId))
                {
                    await Clients.Clients(new List<string> { room.ReaderConnectionId, room.LibrarianConnectionId })
                        .ReceiveMessage("SYSTEM", "You're added to chat room!");
                }
                else
                {
                    await Clients.Clients(new List<string> { room.ReaderConnectionId, room.LibrarianConnectionId })
                        .ReceiveMessage("SYSTEM", "You're added to waiting list");
                }
                //TODO add notification about room filling (if yes)
                //TODO send message only to uuser in your room
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
