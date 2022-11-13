using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.UI.Models;
using System;

namespace SmartBatteryTesterDesktopApp.UI.Infrastructure
{
    public class PortDataGetter : IDataGetter
    {
        DischargingParameters _dischargingParameters;
        public event EventHandler? DataChanged;

        internal PortDataGetter(DischargingParameters parameters)
        {
            _dischargingParameters = parameters;
        }

        public void GetData(string data)
        {
            _dischargingParameters.Voltage = data;
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
