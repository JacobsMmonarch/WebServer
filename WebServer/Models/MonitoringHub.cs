using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebServer.Hubs
{
    public class MonitoringHub : Hub
    {
        public async Task SendResult(string result)
        {
            await Clients.All.SendAsync("ReceiveResult", result);
        }
    }
}