namespace SmartBatteryTesterWebApp.DAL.Entities
{
    public class MeasurementSet
    {
        public int Id { get; set; }
        public string? MeasurementName { get; set; }
        public string? MeasurementStatus { get; set; }
        public TimeSpan? DischargeDuration { get; set; }
        public decimal? ResultCapacity { get; set; }
    }
}
