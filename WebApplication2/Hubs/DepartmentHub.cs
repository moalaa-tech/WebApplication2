using Microsoft.AspNetCore.SignalR;

namespace CRM.WebApp.Hubs
{
    public class DepartmentHub : Hub
    {
        // Optional: you can add methods for client calls
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
