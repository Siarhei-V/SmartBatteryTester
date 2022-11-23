using SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver;

namespace SmartBatteryTesterDesktopApp.PORT.DataSaver
{
    internal class DataSaverFactory : IDataSaverFactory
    {
        public IDataSaver MakeDataToSignalRSender()
        {
            return new DataToSignalRSender();
        }

        public IDataSaver MakeDataToWebApiSender()
        {
            return new DataToWebApiSender();
        }
    }
}
