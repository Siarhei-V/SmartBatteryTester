using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver
{
    internal interface IDataSaverFacade
    {
        internal Task StartDataTransfer(string testName);
        internal Task TransmitData(MeasurementModel portDataModel);
        internal Task FinishDataTransfer();
    }
}
