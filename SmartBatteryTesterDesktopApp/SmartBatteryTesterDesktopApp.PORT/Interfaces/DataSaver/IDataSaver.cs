using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver
{
    internal interface IDataSaver
    {
        internal Task StartDataTransfer(TestModel testModel);
        internal Task TransmitData(MeasurementModel measurementModel);
        internal Task FinishDataTransfer();
    }
}
