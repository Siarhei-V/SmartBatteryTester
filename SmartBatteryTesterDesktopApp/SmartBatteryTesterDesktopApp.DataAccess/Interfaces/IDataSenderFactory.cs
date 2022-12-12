namespace SmartBatteryTesterDesktopApp.DataAccess.Interfaces
{
    internal interface IDataSenderFactory
    {
        internal IDataSender MakeDataToWebApiSender();
        internal IDataSender MakeDataToSignalRSender();
    }
}
