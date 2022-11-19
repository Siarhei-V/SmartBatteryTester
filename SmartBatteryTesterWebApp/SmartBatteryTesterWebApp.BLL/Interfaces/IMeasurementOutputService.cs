using SmartBatteryTesterWebApp.BLL.DTO;

namespace SmartBatteryTesterWebApp.BLL.Interfaces
{
    public interface IMeasurementOutputService
    {
        List<MeasurementSetDTO> GetMeasurementSet();
        List<MeasurementDTO> GetMeasurement(int measurementSetId);
        public MeasurementSetDTO FindMeasurementSet(string measurementSetStatus);
    }
}
