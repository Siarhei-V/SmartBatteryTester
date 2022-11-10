namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface INotifyDataChanged
    {
        event EventHandler<EventArgs> DataChanged;
        void OnDataChanged(string voltage, string current, bool isDischargingStarted);
    }
}
