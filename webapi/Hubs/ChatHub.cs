using Microsoft.AspNetCore.SignalR;

namespace webapi.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync("showString", username, message);
        }
    }
}