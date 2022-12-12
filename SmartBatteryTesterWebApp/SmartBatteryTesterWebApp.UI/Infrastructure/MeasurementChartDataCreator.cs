using SmartBatteryTesterWebApp.UI.Models;
using SmartBatteryTesterWebApp.UI.Models.Chart;

namespace SmartBatteryTesterWebApp.UI.Infrastructure
{
    public class MeasurementChartDataCreator : IMeasurementChartDataCreator
    {
        ChartJsData _chartJsData;
        ChartDataset _firstDataset;
        ChartDataset _secondDataset;

        public MeasurementChartDataCreator()
        {
            _chartJsData = new ChartJsData();
            _firstDataset = new ChartDataset();
            _secondDataset = new ChartDataset();
        }

        public ChartJsData GetLineChartData(List<MeasurementViewModel> measurements)
        {
            // labels and datasets
            foreach (var item in measurements)
            {
                _chartJsData.Labels.Add(item.MeasurementDateTime);
                _firstDataset.Data.Add(item.Voltage);
                _secondDataset.Data.Add(item.Current);
            }

            // voltage
            _firstDataset.Label = "Voltage";
            _firstDataset.BorderWidth = 3;
            _firstDataset.BorderColor = "blue";

            _chartJsData.Datasets.Add(_firstDataset);

            // current
            _secondDataset.Label = "Current";
            _secondDataset.BorderWidth = 3;
            _secondDataset.BorderColor = "green";

            _chartJsData.Datasets.Add(_secondDataset);

            return _chartJsData;
        }
    }
}
