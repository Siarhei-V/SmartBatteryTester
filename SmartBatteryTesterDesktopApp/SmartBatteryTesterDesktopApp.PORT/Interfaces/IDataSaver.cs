using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IDataSaver
    {
        public Task StartDataTransfer(string testName);
        public Task TransmitData(MeasurementModel portDataModel);
        public Task FinishDataTransfer(TimeSpan dischargingDuration, decimal resultCapacity, string status);
    }
}
