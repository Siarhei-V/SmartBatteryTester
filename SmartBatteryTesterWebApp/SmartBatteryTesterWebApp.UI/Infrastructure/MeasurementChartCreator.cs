using SmartBatteryTesterWebApp.UI.Models;
using SmartBatteryTesterWebApp.UI.Models.Chart;

namespace SmartBatteryTesterWebApp.UI.Infrastructure
{
    public class MeasurementChartCreator : IMeasurementChartCreator
    {
        public ChartJsData GetLineChartData(List<MeasurementViewModel> measurements)
        {
            ChartJsData chartModel = new ChartJsData();

            // labels
            var labels = new List<string>();
            var dateTimeList = new List<string>();

            foreach (var item in measurements)
            {
                dateTimeList.Add(item.MeasurementDateTime.ToString());
            }

            labels.AddRange(dateTimeList);
            chartModel.Labels = labels;

            // datasets
            var datasets = new List<ChartDataset>();

            // voltage
            var voltageDataset = new ChartDataset();
            var voltagedataList = new List<decimal>();

            foreach (var item in measurements)
            {
                voltagedataList.Add(item.Voltage);
            }

            voltageDataset.Label = "Voltage";
            voltageDataset.BorderWidth = 3;
            voltageDataset.BorderColor = "blue";
            voltageDataset.Data = voltagedataList;

            // current
            var currentDataset = new ChartDataset();
            var currentDataList = new List<decimal>();

            foreach (var item in measurements)
            {
                currentDataList.Add(item.Current);
            }

            currentDataset.Label = "Current";
            currentDataset.BorderWidth = 3;
            currentDataset.BorderColor = "green";
            currentDataset.Data = currentDataList;

            // add to model
            datasets.AddRange(new[] { voltageDataset, currentDataset });
            chartModel.Datasets = datasets;

            return chartModel;
        }
    }
}
