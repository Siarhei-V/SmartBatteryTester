using SmartBatteryTesterDesktopApp.BL;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortDataHandler : IPortDataHandler
    {
        IDischargerInitializer? _dischargerInitializer;
        public IDischarger? _discharger;

        public PortDataHandler(IValuesSaver valuesSaver, IInfoSaver infoSaver, ISwitchable dischargerSwitch)  // TODO: change ctor?
        {
            _dischargerInitializer = new DischargerInitializer(valuesSaver, infoSaver, dischargerSwitch);
            _discharger = _dischargerInitializer.DischargerImplementation;
        }

        public void HandleStartValues(decimal lowerDischargerVoltage, decimal startVoltage, decimal valuesChangeDiscreteness)
        {
            _discharger.Start(lowerDischargerVoltage, startVoltage, valuesChangeDiscreteness);
        }

        public void HandleIntermediateValues(decimal voltage, decimal current, DateTime dateTime)
        {
            _discharger.Discharge(voltage, current);
        }
    }
}
