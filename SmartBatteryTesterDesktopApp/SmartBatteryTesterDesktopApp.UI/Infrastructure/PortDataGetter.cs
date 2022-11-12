using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.UI.Models;
using System;

namespace SmartBatteryTesterDesktopApp.UI.Infrastructure
{
    internal class PortDataGetter : IDataGetter
    {
        DischargingParameters _dischargingParameters;
        public event EventHandler DataChanged;

        public PortDataGetter(DischargingParameters parameters)
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
