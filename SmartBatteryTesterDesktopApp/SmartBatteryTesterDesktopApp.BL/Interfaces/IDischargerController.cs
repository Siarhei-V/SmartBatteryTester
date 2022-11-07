namespace SmartBatteryTesterDesktopApp.BL.Interfaces
{
    public interface IDischargerController
    {
        public event EventHandler ControllerNotify;
        public void StopDischarging();
    }
}
