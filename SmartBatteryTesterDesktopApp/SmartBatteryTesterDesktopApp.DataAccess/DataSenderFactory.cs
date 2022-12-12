using SmartBatteryTesterDesktopApp.DataAccess.Interfaces;

namespace SmartBatteryTesterDesktopApp.DataAccess
{
    internal class DataSenderFactory : IDataSenderFactory
    {
        IDataSender IDataSenderFactory.MakeDataToSignalRSender()
        {
            return new DataToSignalRSender();
        }

        IDataSender IDataSenderFactory.MakeDataToWebApiSender()
        {
            return new DataToWebApiSender();
        }
    }
}
