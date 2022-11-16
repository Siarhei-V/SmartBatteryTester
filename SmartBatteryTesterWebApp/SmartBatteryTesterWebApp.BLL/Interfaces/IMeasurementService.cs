using SmartBatteryTesterWebApp.BLL.DTO;

namespace SmartBatteryTesterWebApp.BLL.Interfaces
{
    public interface IMeasurementService
    {
        List<MeasurementSetDTO> GetMeasurementSet();
        void MakeMeasurement(MeasurementDTO measurement);
        void MakeMeasurementSet(MeasurementSetDTO measurementSet);
    }
}
