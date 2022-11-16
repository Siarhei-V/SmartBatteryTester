namespace SmartBatteryTesterWebApp.BLL.DTO
{
    public class MeasurementDTO
    {
        public int Id { get; set; }
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
        public string? MeasurementDateTime { get; set; }
        public string? MeasurementName { get; set; }
    }
}
