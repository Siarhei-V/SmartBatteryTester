using SmartBatteryTesterWebApp.BLL.DTO;

namespace SmartBatteryTesterWebApp.BLL.Interfaces
{
    public interface IMeasurementInputService
    {
        Task MakeMeasurementAsync(MeasurementDTO measurement);
        Task MakeMeasurementSetAsync(MeasurementSetDTO measurementSet);
        Task UpdateMeasurementSetAsync(MeasurementSetDTO measurementSetDto);
    }
}
