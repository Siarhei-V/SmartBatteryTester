namespace SmartBatteryTesterDesktopApp.BL
{
    public class DischargerDto
    {
        public bool IsDischargingCompleted { get; internal set; }
        public DateTime CurrentDateTime { get; internal set; }
        public decimal Voltage { get; internal set; }
        public decimal Current { get; internal set; }
        public TimeSpan DischargeDuration { get; internal set; }
        public decimal ResultCapacity { get; internal set; }
    }
}
