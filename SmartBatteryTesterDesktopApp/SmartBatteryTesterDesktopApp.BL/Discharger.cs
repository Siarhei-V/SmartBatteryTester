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
        private ICapacityCalculator _capacityCalculator;

        private decimal _lowerDischargeThreshold;
        private decimal _valuesChangeDiscreteness;
        private decimal _prevVoltage;
        private decimal _prevCurrent;

        public Discharger(IValuesSaver valuesSaver, IInfoSaver infoSaver, ISwitchable dischargerSwitch)
        {
            _capacityCalculator = new CapacityCalculator();

            _dataModel = new DischargerValuesDto();
            _infoModel = new DischargerInfoDto();

            _valuesSaver = valuesSaver;
            _infoSaver = infoSaver;
            _dischargerSwitch = dischargerSwitch;

        }

        public void Start(decimal lowerDischargeThreshold, decimal voltageBeforeDischarging, decimal valuesChangeDiscreteness)
        {
            _dataModel.Voltage = voltageBeforeDischarging;
            _dataModel.CurrentDateTime = DateTime.Now;

            _prevVoltage = voltageBeforeDischarging;
            _lowerDischargeThreshold = lowerDischargeThreshold;
            _valuesChangeDiscreteness = valuesChangeDiscreteness;

            _dischargerSwitch.TurnOn();
            _capacityCalculator.DischargingStartDateTime = _dataModel.CurrentDateTime;

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

            _capacityCalculator.DischargingCurrent = current;
            _capacityCalculator.DischargingEndDateTime = _dataModel.CurrentDateTime;
            _infoModel.ResultCapacity = _capacityCalculator.GetCapacity();

            _dischargerSwitch.TurnOff();

            _valuesSaver.Save(_dataModel);
            _infoSaver.Save(_infoModel);
        }
        #endregion
    }
}