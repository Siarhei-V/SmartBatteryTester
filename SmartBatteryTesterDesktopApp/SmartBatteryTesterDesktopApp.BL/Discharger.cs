using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    public class Discharger : IDischarger
    {
        private DischargerDto _dataModel;

        private IResultsCalculator? _resultsCalculator;
        private decimal _lowerDischargeThreshold;
        private decimal _valuesChangeDiscreteness;
        private decimal _prevVoltage;
        private decimal _prevCurrent;
        private bool _isFirstData = true;
        private bool _isNewDataReceived;

        public Discharger(DischargerDto dischargerDto)
        {
            _dataModel = dischargerDto;
        }

        IResultsCalculator IDischarger.ResultsCalculator 
        { 
            set => _resultsCalculator = value;
        }

        public bool IsNewDataReceived
        {
            get => _isNewDataReceived;
        }

        public void SetDischargingParams(decimal lowerDischargeThreshold, decimal valuesChangeDiscreteness, decimal dischargingCurrent)
        {
            _lowerDischargeThreshold = lowerDischargeThreshold;
            _valuesChangeDiscreteness = valuesChangeDiscreteness;
            _dataModel.Current = dischargingCurrent;
        }

        public void Discharge(decimal voltage, decimal current, DateTime dateTime)
        {
            if (voltage <= _lowerDischargeThreshold)
            {
                StopDischarging(voltage, current, dateTime);
                return;
            }

            if (Math.Abs(_prevVoltage - voltage) >= _valuesChangeDiscreteness ||
                Math.Abs(_prevCurrent - current) >= _valuesChangeDiscreteness)
            {
                HandleNewData(voltage, current, dateTime);
                return;
            }
         
            _isNewDataReceived = false;
        }

        public DischargerDto GetDischargingData()
        {
            return _dataModel;
        }

        #region PrivateMethods
        private void HandleNewData(decimal voltage, decimal current, DateTime dateTime)
        {
            if (_isFirstData)
            {
                _resultsCalculator.DischargingStartDateTime = dateTime;
                _dataModel.IsDischargingCompleted = false;
                _isFirstData = false;
            }

            _dataModel.Voltage = voltage;
            _dataModel.CurrentDateTime = dateTime;

            _prevVoltage = voltage;

            _isNewDataReceived = true;
        }

        private void StopDischarging(decimal voltage, decimal current, DateTime dateTime)
        {
            _dataModel.Voltage = voltage;
            _dataModel.CurrentDateTime = dateTime;

            _resultsCalculator.DischargingCurrent = _dataModel.Current;
            _resultsCalculator.DischargingEndDateTime = dateTime;
            _resultsCalculator.CalculateResults();

            _isNewDataReceived = true;
            _dataModel.IsDischargingCompleted = true;

            PrepareToNewMeasurement();
        }

        private void PrepareToNewMeasurement()
        {
            _isFirstData = true;
            _prevCurrent = 0;
            _prevVoltage = 0;
        }
        #endregion
    }
}