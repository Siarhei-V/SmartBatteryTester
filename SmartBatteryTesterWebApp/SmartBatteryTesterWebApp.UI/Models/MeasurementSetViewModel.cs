namespace SmartBatteryTesterWebApp.UI.Models
{
    public class MeasurementSetViewModel
    {
        public int Id { get; set; }
        public string? MeasurementName { get; set; }
        public string? MeasurementStatus { get; set; }
        public TimeSpan? DischargeDuration { get; set; }
        public decimal? ResultCapacity { get; set; }
    }
}
