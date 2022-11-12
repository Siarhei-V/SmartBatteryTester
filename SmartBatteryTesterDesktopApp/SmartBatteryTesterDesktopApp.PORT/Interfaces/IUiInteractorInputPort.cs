namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IUiInteractorInputPort
    {
        void StartDischarging(Dictionary<string, string> portConnectionParameters);
        void StopDischarging();
    }
}
