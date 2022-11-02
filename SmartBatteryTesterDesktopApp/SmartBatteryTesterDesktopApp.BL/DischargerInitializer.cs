using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    public class DischargerInitializer : IDischargerInitializer
    {
        private ICapacityCalculatorFactory _capacityCalculatorFactory;
        private IDischarger _discharger;

        public IDischarger DischargerImplementation
        {
            get => _discharger;
        }

        public DischargerInitializer(IValuesSaver valuesSaver, IInfoSaver infoSaver, ISwitchable dischargerSwitch)
        {
            _capacityCalculatorFactory = new CapacityCalculatorFactory();
            _discharger = new Discharger(valuesSaver, infoSaver, dischargerSwitch);
            _discharger.CapacityCalculatorFactory = _capacityCalculatorFactory;
        }
    }
}
