namespace SmartBatteryTesterDesktopApp.BL
{
    public class DischargerValuesDto
    {
        public DateTime CurrentDateTime { get; internal set; }
        public decimal Voltage { get; internal set; }
        public decimal Current { get; internal set; }
    }
}
