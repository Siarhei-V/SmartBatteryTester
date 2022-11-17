using Microsoft.AspNetCore.SignalR;

namespace SmartBatteryTesterWebApp.UI.Infrastructure
{
    public class MeasurementsHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }
    }
}
