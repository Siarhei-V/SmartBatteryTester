namespace SmartBatteryTesterDesktopApp.DataAccess.Models
{
    internal class MeasurementBaseDataModel
    {
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
        public string? MeasurementDateTime { get; set; }
    }
}
