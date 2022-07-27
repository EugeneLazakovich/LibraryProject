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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{new string(' ', Console.WindowWidth - user.Length - 2)}{user}:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{new string(' ', Console.WindowWidth - message.Length - 1)}{message}");
                Console.ForegroundColor = ConsoleColor.White;
            });
            connection.On<string>(nameof(IClientHub.ReceiveSystemMessage), (message) =>
            {
                Console.WriteLine($"SYSTEM: {message}");
            });

            await connection.StartAsync();
            bool isAuthorized = false;
            Console.Write("Please authorize using '/signin': ");
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
                            if (!isAuthorized)
                            {
                                var parametrs = message.Substring(message.IndexOf(' ') + 1).Split(' ');
                                if (parametrs.Length < 2)
                                {
                                    break;
                                }
                                string login = parametrs[0];
                                string password = string.Join(' ', parametrs[1..]);
                                isAuthorized = await connection.InvokeAsync<bool>(
                                    nameof(IServerHub.SignIn),
                                    login,
                                    password);
                                if (isAuthorized)
                                {
                                    Console.WriteLine($"SYSTEM: Hello {login}!");
                                }
                                else
                                {
                                    Console.WriteLine("SYSTEM: Wrong credentials!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("SYSTEM: You have been already authorized!");
                            }
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
