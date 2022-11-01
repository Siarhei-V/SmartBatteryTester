namespace SmartBatteryTesterDesktopApp.BL
{
    public class Discharger
    {
        private DischargerDto _dataModel;
        private decimal _lowerDischargeThreshold;
        private IResultSaver _resultSaver;
        private ISwitchable _dischargerSwitch;

        private decimal _valuesChangeDiscreteness;
        private decimal _prevVoltage;
        private decimal _prevCurrent;

        public Discharger(IResultSaver resultSaver, ISwitchable dischargerSwitch)
        {
            _dataModel = new DischargerDto();
            _resultSaver = resultSaver;
            _dischargerSwitch = dischargerSwitch;
        }

        public void Start(decimal lowerDischargeThreshold, decimal voltageBeforeDischarging, decimal valuesChangeDiscreteness)
        {
            _dataModel.Voltage = voltageBeforeDischarging;
            _prevVoltage = voltageBeforeDischarging;

            _lowerDischargeThreshold = lowerDischargeThreshold;
            _valuesChangeDiscreteness = valuesChangeDiscreteness;

            _resultSaver.Save();
            _dischargerSwitch.TurnOn();
        }

        public void Discharge(decimal voltage, decimal current)
        {
            if (voltage <= _lowerDischargeThreshold)
            {
                _dischargerSwitch.TurnOff();
                _resultSaver.Save();
                return;
            }

            if (_prevVoltage - voltage >= _valuesChangeDiscreteness || 
                Math.Abs(_prevCurrent - current) >= _valuesChangeDiscreteness)
            {
                _prevCurrent = current;
                _prevVoltage = voltage;

                _resultSaver.Save();
            }
        }
    }
}