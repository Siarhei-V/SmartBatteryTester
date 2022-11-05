namespace SmartBatteryTesterDesktopApp.PORT
{
    public class UsartDischargerDto
    {
        public string? PortName { get; set; }
        public int BaudRate { get; set; }
        public string? Parity { get; set; }
        public int DataBits { get; set; }
        public string? StopBits { get; set; }
        public decimal LowerDischargeVoltage { get; set; }
        public decimal ValuesChangeDiscretennes { get; set; }
    }
}
