namespace SmartBatteryTesterWebApp.DAL.Entities
{
    public class Measurement
    {
        public int Id { get; set; }
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
        public string? MeasurementDateTime { get; set; }
        public int MeasurementSetId { get; set; }
        public MeasurementSet? SetOfMeasurements { get; set; }
    }
}
