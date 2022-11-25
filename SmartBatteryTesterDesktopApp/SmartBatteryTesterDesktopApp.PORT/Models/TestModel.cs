namespace SmartBatteryTesterDesktopApp.PORT.Models
{
    internal class TestModel
    {
        public int Id { get; set; }
        public string? MeasurementName { get; set; }
        public string? MeasurementStatus { get; set; }
        public TimeSpan DischargeDuration { get; internal set; }
        public decimal ResultCapacity { get; internal set; }
    }
}
