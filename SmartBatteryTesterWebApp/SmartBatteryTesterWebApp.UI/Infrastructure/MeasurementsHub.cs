using Microsoft.AspNetCore.SignalR;
using SmartBatteryTesterWebApp.UI.Models;
using System.Text.Json;

namespace SmartBatteryTesterWebApp.UI.Infrastructure
{
    public class MeasurementsHub : Hub
    {
        public async Task Send(string message, MeasurementViewModel measurement)
        {
            await Clients.All.SendAsync("Receive", message, measurement);
        }
    }
}
