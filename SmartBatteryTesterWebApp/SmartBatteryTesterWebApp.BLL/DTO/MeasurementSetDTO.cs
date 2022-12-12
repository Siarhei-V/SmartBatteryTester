namespace SmartBatteryTesterWebApp.BLL.DTO
{
    public class MeasurementSetDTO
    {
        public int Id { get; set; }
        public string? MeasurementName { get; set; }
        public string? MeasurementStatus { get; set; }
        public TimeSpan? DischargeDuration { get; set; }
        public decimal? ResultCapacity { get; set; }
    }
}
