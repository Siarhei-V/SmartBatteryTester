using SmartBatteryTesterWebApp.DAL.Entities;

namespace SmartBatteryTesterWebApp.BLL.DTO
{
    public class MeasurementSetDTO
    {
        public int Id { get; set; }
        public string? MeasurementName { get; set; }
        public List<Measurement> MeasurementList { get; set; } = new List<Measurement>();

    }
}
