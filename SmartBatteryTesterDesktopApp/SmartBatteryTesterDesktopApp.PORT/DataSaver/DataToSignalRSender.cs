using SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver;
using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT.DataSaver
{
    internal class DataToSignalRSender : IDataSaver
    {
        async Task IDataSaver.StartDataTransfer(TestModel testModel)
        {
            throw new NotImplementedException();
        }

        async Task IDataSaver.TransmitData(MeasurementModel portDataModel)
        {
            throw new NotImplementedException();
        }
        async Task IDataSaver.FinishDataTransfer()
        {
            throw new NotImplementedException();
        }
    }
}
