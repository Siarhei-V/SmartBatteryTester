using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using System;

namespace SmartBatteryTesterDesktopApp.UI.Infrastructure
{
    public class DataChangedNotifier : INotifyDataChanged
    {
        public event EventHandler<EventArgs> DataChanged;

        public void OnDataChanged(string voltage, string current)
        {
            MeasurementEventArgs e = new MeasurementEventArgs();
            e.Voltage = voltage;
            e.Current = current;
            DataChanged?.Invoke(this, e);
        }
    }
}
