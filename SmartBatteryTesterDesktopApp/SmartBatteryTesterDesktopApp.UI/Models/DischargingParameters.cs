using System.Collections.Generic;

namespace SmartBatteryTesterDesktopApp.UI.Models
{
    internal class DischargingParameters
    {
        public List<string> ValuesChangeDiscretennesList { get; } = new List<string>()
        {
            "0,1",
            "0,5",
            "1,0"
        };
        
        public string? ValuesChangeDiscretennes { get; set; }

        public string? LowerVoltageThreshold { get; set; }
    }
}
