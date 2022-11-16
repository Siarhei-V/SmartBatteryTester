using SmartBatteryTesterWebApp.BLL.DTO;

namespace SmartBatteryTesterWebApp.BLL.Interfaces
{
    public interface IMeasurementOutputService
    {
        List<MeasurementSetDTO> GetMeasurementSet();
    }
}
