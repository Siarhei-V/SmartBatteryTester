using SmartBatteryTesterWebApp.BLL.DTO;

namespace SmartBatteryTesterWebApp.BLL.Interfaces
{
    public interface IMeasurementOutputService
    {
        Task<List<MeasurementSetDTO>> GetMeasurementSetAsync();
        Task<List<MeasurementDTO>> GetMeasurementAsync(int measurementSetId);
        Task<MeasurementSetDTO> FindMeasurementSetAsync(string measurementSetStatus);
    }
}
