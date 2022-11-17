using SmartBatteryTesterWebApp.UI.Models;
using SmartBatteryTesterWebApp.UI.Models.Chart;

namespace SmartBatteryTesterWebApp.UI.Infrastructure
{
    public interface IMeasurementChartCreator
    {
        ChartJsData GetLineChartData(List<MeasurementViewModel> measurements);
    }
}