using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    public class Discharger : IDischarger
    {
        private DischargerValuesDto _dataModel;
        private DischargerInfoDto _infoModel;

        private IValuesSaver _valuesSaver;
        private IInfoSaver _infoSaver;
        private ISwitchable _dischargerSwitch;
        private IResultsCalculator _resultsCalculator;
        private decimal _lowerDischargeThreshold;
        private decimal _valuesChangeDiscreteness;
        private decimal _prevVoltage;
        private decimal _prevCurrent;

        private IResultsCalculatorFactory _resultsCalculatorFactory;
        IResultsCalculatorFactory IDischarger.ResultsCalculatorFactory
        {
            set => _resultsCalculatorFactory = value;
        }

        public Discharger(IValuesSaver valuesSaver, IInfoSaver infoSaver, ISwitchable dischargerSwitch)
        {
            _dataModel = new DischargerValuesDto();
            _infoModel = new DischargerInfoDto();

            _valuesSaver = valuesSaver;
            _infoSaver = infoSaver;
            _dischargerSwitch = dischargerSwitch;
        }

        public void Start(decimal lowerDischargeThreshold, decimal voltageBeforeDischarging, decimal valuesChangeDiscreteness)
        {
            _resultsCalculator = _resultsCalculatorFactory.MakeResultsCalculator();
            _resultsCalculator.DischargerInfoDto = _infoModel;

            _dataModel.Voltage = voltageBeforeDischarging;
            _dataModel.CurrentDateTime = DateTime.Now;

            _prevVoltage = voltageBeforeDischarging;
            _lowerDischargeThreshold = lowerDischargeThreshold;
            _valuesChangeDiscreteness = valuesChangeDiscreteness;

            _dischargerSwitch.TurnOn();
            _resultsCalculator.DischargingStartDateTime = _dataModel.CurrentDateTime;

            _valuesSaver.Save(_dataModel);
        }

        public void Discharge(decimal voltage, decimal current)
        {
            if (voltage <= _lowerDischargeThreshold)
            {
                StopDischarging(voltage, current);
                return;
            }

            if (_prevVoltage - voltage >= _valuesChangeDiscreteness ||
                Math.Abs(_prevCurrent - current) >= _valuesChangeDiscreteness)
            {
                HandleCurrentValues(voltage, current);
            }
        }

        #region PrivateMethods
        private void HandleCurrentValues(decimal voltage, decimal current)
        {
            _dataModel.Voltage = voltage;
            _dataModel.Current = current;
            _dataModel.CurrentDateTime = DateTime.Now;

            _prevVoltage = voltage;
            _prevCurrent = current;

            _valuesSaver.Save(_dataModel);
        }

        private void StopDischarging(decimal voltage, decimal current)
        {
            _dataModel.Voltage = voltage;
            _dataModel.Current = current;
            _dataModel.CurrentDateTime = DateTime.Now;

            _resultsCalculator.DischargingCurrent = current;
            _resultsCalculator.DischargingEndDateTime = _dataModel.CurrentDateTime;
            _resultsCalculator.CalculateResults();

            _dischargerSwitch.TurnOff();

            _valuesSaver.Save(_dataModel);
            _infoSaver.Save(_infoModel);
        }
        #endregion
    }
}