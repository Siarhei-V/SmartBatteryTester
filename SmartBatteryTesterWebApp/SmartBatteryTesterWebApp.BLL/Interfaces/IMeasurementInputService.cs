using SmartBatteryTesterWebApp.BLL.DTO;

namespace SmartBatteryTesterWebApp.BLL.Interfaces
{
    public interface IMeasurementInputService
    {
        void MakeMeasurement(MeasurementDTO measurement);
        void MakeMeasurementSet(MeasurementSetDTO measurementSet);
        public void UpdateMeasurementSet(MeasurementSetDTO measurementSetDto);
    }
}
