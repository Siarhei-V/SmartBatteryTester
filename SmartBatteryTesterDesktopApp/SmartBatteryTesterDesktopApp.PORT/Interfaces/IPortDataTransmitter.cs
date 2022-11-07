namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortDataTransmitter
    {
        public event EventHandler DataReceivedNotify;
        public void StartDataTransfer(Dictionary<string, string> parameters);
        public void StopDataTransfer();
    }
}
