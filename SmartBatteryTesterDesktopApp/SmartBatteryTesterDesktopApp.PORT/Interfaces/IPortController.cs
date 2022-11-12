namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortController
    {
        public void StartDischarging(Dictionary<string, string> parameters);
        public void StopDischarging();
    }
}
