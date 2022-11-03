namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortDataHandler
    {
        public void Connect();
        public void GetData(decimal voltage, decimal current, DateTime currentDateTime);
        public void Disconnect();
    }
}
