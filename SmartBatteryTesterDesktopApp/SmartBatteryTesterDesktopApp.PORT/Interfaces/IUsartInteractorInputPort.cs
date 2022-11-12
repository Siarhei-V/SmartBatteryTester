namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IUsartInteractorInputPort
    {
        void SendUsartData(string data);
        IPortController PortController { set; }

    }
}
