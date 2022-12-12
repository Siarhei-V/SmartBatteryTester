namespace SmartBatteryTesterWebApp.UI.Models.Chart
{
    public class ChartDataset
    {
        public ChartDataset()
        {
            Data = new List<decimal>();
        }
        public string? Label { get; set; }
        public string? BackgroundColor { get; set; }
        public int BorderWidth { get; set; }
        public string? BorderColor { get; set; }
        public List<decimal> Data { get; set; }
    }
}
