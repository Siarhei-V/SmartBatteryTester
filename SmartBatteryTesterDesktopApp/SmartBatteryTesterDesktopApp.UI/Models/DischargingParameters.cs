using System.Collections.Generic;

namespace SmartBatteryTesterDesktopApp.UI.Models
{
    public class DischargingParameters
    {
        internal string? Voltage { get; set; }

        internal string? Current { get; set; }

        internal List<string> ValuesChangeDiscretennesList { get; } = new List<string>()
        {
            "0,1",
            "0,5",
            "1,0"
        };

        internal string? ValuesChangeDiscretennes { get; set; }

        internal string? LowerVoltageThreshold { get; set; }

        internal string? TestName { get; set; }
        internal string? PortConnectionStatus { get; set; } = "Готов";
        internal string? WebConnectionStatus { get; set; } = "Готов";
    }
}
