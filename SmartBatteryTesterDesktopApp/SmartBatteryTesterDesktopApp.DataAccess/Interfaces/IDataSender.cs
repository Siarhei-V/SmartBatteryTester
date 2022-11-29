using SmartBatteryTesterDesktopApp.DataAccess.Models;

namespace SmartBatteryTesterDesktopApp.DataAccess.Interfaces
{
    internal interface IDataSender
    {
        internal Task StartDataTransfer(TestDataModel testModel);
        internal Task TransmitData(MeasurementDataModel measurementModel);
        internal Task FinishDataTransfer();
    }
}
