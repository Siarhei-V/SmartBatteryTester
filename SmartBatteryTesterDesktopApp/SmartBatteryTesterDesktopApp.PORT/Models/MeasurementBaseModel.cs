namespace SmartBatteryTesterDesktopApp.PORT.Models
{
    public class MeasurementBaseModel
    {
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
        public string? MeasurementDateTime { get; set; }
    }
}
