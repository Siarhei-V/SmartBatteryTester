using SmartBatteryTesterWebApp.UI.Models;
using SmartBatteryTesterWebApp.UI.Models.Chart;

namespace SmartBatteryTesterWebApp.UI.Infrastructure
{
    public interface IMeasurementChartDataCreator
    {
        ChartJsData GetLineChartData(List<MeasurementViewModel> measurements);
    }
}