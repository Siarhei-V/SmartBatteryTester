namespace SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver
{
    internal interface IDataSaverFactory
    {
        IDataSaver MakeDataToWebApiSender();
        IDataSaver MakeDataToSignalRSender();
    }
}
