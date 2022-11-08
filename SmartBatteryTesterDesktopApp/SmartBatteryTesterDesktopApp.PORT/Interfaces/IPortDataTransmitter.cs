namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortDataTransmitter
    {
        public Dictionary<string, string> Parameters { get; set; }
        public void StartDataTransfer();
        public void StopDataTransfer();
    }
}
