using System.Collections.Generic;

namespace SmartBatteryTesterDesktopApp.UI.Models
{
    internal class DischargingParameters
    {
        #region new code
        public string? Voltage { get; set; }

        #endregion

        public List<string> ValuesChangeDiscretennesList { get; } = new List<string>()
        {
            "0,1",
            "0,5",
            "1,0"
        };
        
        public string? ValuesChangeDiscretennes { get; set; }

        public string? LowerVoltageThreshold { get; set; }

        public string? Current { get; set; }
    }
}
