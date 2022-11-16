namespace SmartBatteryTesterWebApp.DAL.Entities
{
    public class MeasurementSet
    {
        public int Id { get; set; }
        public string? MeasurementName { get; set; }
        public List<Measurement> MeasurementList { get; set; } = new List<Measurement>();
    }
}
