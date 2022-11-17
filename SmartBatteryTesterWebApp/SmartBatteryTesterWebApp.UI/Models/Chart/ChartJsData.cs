namespace SmartBatteryTesterWebApp.UI.Models.Chart
{
    public class ChartJsData
    {
        public ChartJsData()
        {
            Labels = new List<string>();
            Datasets = new List<ChartDataset>();
        }
        public List<string> Labels { get; set; }
        public List<ChartDataset> Datasets { get; set; }
    }
}
