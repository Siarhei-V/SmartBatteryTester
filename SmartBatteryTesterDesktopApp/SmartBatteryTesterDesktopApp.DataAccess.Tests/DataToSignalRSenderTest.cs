using SmartBatteryTesterDesktopApp.DataAccess.Interfaces;

namespace SmartBatteryTesterDesktopApp.DataAccess.Tests
{
    public class DataToSignalRSenderTest
    {
        IDataSender _dataToSignalRSender;

        public DataToSignalRSenderTest()
        {
            _dataToSignalRSender = new DataToSignalRSender();
        }
    }
}
