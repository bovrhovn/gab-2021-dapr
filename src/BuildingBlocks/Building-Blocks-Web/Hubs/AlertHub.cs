using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Building_Blocks_Web.Hubs
{
    public class AlertHub : Hub
    {
        public Task AlertMessage(string message) =>
            Clients.All.SendAsync("alertMessage", message);
    }
}