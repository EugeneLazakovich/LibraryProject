using Lesson1_Core;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Lesson1_ConsoleChat
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HubConnection connection;
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/chat")
                .Build();
            connection.On<string, string>(nameof(IClientHub.ReceiveMessage), (user, message) =>
            {
                Console.WriteLine($"{user}: {message}");
            });

            await connection.StartAsync();
            Console.Write("Enter your name: ");
            string username = Console.ReadLine();
            string message = string.Empty;
            do
            {
                message = Console.ReadLine();
                if (message.StartsWith('/'))
                {
                    string command = message.Substring(1, message.IndexOf(' ') - 1).ToLower();
                    switch (command)
                    {
                        case "signin":
                            var parametrs = message.Substring(message.IndexOf(' ') + 1).Split(' ');
                            if(parametrs.Length < 2)
                            {
                                break;
                            }
                            string login = parametrs[0];
                            string password = string.Join(' ', parametrs[1..]);
                            var response = await connection.InvokeAsync<bool>(
                                nameof(IServerHub.SignIn),
                                login,
                                password);
                            break;
                    }
                }
                else
                {
                    await connection.InvokeAsync(nameof(IServerHub.SendMessage), message);
                }
                
            } while (!string.IsNullOrEmpty(message));            
        }
    }
}
