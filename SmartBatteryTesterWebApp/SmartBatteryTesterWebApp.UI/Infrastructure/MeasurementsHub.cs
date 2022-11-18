using Microsoft.AspNetCore.SignalR;
using SmartBatteryTesterWebApp.UI.Models;

namespace SmartBatteryTesterWebApp.UI.Infrastructure
{
    public class MeasurementsHub : Hub
    {
        IMeasurementChartDataCreator _measurementChartDataCreator;

        public MeasurementsHub(IMeasurementChartDataCreator measurementChartDataCreator)
        {
            _measurementChartDataCreator = measurementChartDataCreator;
        }

        public async Task Send(string message, MeasurementViewModel measurement)
        {
            var chartData = _measurementChartDataCreator.GetLineChartData(new List<MeasurementViewModel> () { measurement});
            await Clients.All.SendAsync("Receive", message, chartData);
        }
    }
}
