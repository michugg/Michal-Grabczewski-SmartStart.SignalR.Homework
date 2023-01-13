using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace signalchat
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var hubConnection = new HubConnectionBuilder()
                //info: Microsoft.Hosting.Lifetime[14]
                //Now listening on: http://localhost:5125
                .WithUrl("http://localhost:5125/hubs/chat")
                .ConfigureLogging(x =>
                {
                    x.AddConsole();
                    x.SetMinimumLevel(LogLevel.Error);
                })
                .Build();

            hubConnection.On<string, string>("showString", ShowMessage);
            await hubConnection.StartAsync();

            Console.WriteLine("Write username: ");
            var username = Console.ReadLine();
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Write message (or type \"/exit\" to leave chat): ");
                var message = Console.ReadLine();
                if (message == "/exit")
                {
                    flag = false;
                    break;
                }
                await hubConnection.InvokeAsync("SendMessage", username, message);
            }
        }

        private static void ShowMessage(string username, string message)
        {
            Console.WriteLine($"<{username}>: {message}");
        }
    }
}