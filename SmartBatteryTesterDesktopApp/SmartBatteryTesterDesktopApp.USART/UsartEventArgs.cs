namespace SmartBatteryTesterDesktopApp.USART
{
    internal class UsartEventArgs : EventArgs
    {
        internal string LowerVoltageThreshol { get; set; }
        internal string ValuesChangeDiscretennes { get; set; }
    }
}
