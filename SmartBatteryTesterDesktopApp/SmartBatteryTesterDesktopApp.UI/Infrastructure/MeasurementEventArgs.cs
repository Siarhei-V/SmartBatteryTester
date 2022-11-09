using System;

namespace SmartBatteryTesterDesktopApp.UI.Infrastructure
{
    internal class MeasurementEventArgs : EventArgs
    {
        internal string? Voltage { get; set; }
        internal string? Current { get; set; }
    }
}
    