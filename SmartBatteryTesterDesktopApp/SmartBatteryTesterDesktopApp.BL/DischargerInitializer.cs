using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    public class DischargerInitializer : IDischargerInitializer
    {
        private IResultsCalculatorFactory _ResultsCalculatorFactory;
        private IDischarger _discharger;

        public IDischarger DischargerImplementation
        {
            get => _discharger;
        }

        public DischargerInitializer(IValuesSaver valuesSaver, IInfoSaver infoSaver, ISwitchable dischargerSwitch)
        {
            _ResultsCalculatorFactory = new ResultsCalculatorFactory();
            _discharger = new Discharger(valuesSaver, infoSaver, dischargerSwitch);
            _discharger.ResultsCalculatorFactory = _ResultsCalculatorFactory;
        }
    }
}
