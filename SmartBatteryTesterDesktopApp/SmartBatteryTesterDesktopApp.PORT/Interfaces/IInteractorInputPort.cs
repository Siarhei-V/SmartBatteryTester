namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IInteractorInputPort
    {
        void StartDischarging(Dictionary<string, string> portConnectionParameters);
        void StopDischarging();
    }
}
