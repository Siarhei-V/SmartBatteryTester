namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IDataGetter
    {
        event EventHandler DataChanged;
        void GetData(string data);
        void GetPortStatus(string status);
        void GetWebStatus(string status);
    }
}
