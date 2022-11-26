namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortController
    {
        void Connect(Dictionary<string, string> parameters);
        void Start();
        void StopDischarging();
    }
}
