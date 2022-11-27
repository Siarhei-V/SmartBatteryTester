namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortController
    {
        void OpenPort(Dictionary<string, string> parameters);
        void StartDischarging();
        void StopDischarging();
        void ClosePort();
    }
}
