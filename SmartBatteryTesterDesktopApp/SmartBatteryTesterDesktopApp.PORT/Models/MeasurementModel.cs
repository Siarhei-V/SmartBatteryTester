namespace SmartBatteryTesterDesktopApp.PORT.Models
{
    public class MeasurementModel
    {
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
        public string? MeasurementDateTime { get; set; }
        public int MeasurementSetId { get; set; }
    }
}
