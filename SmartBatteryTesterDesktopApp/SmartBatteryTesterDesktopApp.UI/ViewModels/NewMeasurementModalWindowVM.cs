using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.UI.Models;

namespace SmartBatteryTesterDesktopApp.UI.ViewModels
{
    public class NewMeasurementModalWindowVM
    {
        DischargingParameters _dischargingParameters;

        public NewMeasurementModalWindowVM(DischargingParameters dischargingParameters)
        {
            _dischargingParameters = dischargingParameters;
        }

        public string? TestName
        {
            set
            {
                _dischargingParameters.TestName = value;
            }
        }
    }
}
