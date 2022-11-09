namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface INotifyDataChanged
    {
        event EventHandler<EventArgs> DataChanged;
        public void OnDataChanged(string voltage, string current);
    }
}
