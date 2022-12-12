namespace SmartBatteryTesterWebApp.UI.Models
{
    public class MeasurementViewModel
    {
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
        public string? MeasurementDateTime { get; set; }
    }
}
