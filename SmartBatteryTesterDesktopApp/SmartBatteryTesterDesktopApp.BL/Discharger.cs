using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    internal class Discharger : IDischarger
    {
        private DischargerDto _dataModel;

        private IDischargerDataSaver _dataSaver;
        private IResultsCalculator _resultsCalculator;
        private IDischargerController _controller;
        private decimal _lowerDischargeThreshold;
        private decimal _valuesChangeDiscreteness;
        private decimal _prevVoltage;
        private decimal _prevCurrent;

        public Discharger(IDischargerDataSaver dataSaver, IResultsCalculator resultsCalculator, DischargerDto dischargerDto, 
            IDischargerController dischargerController)
        {
            _dataModel = dischargerDto;
            _resultsCalculator = resultsCalculator;
            _dataSaver = dataSaver;
            _controller = dischargerController;
        }

        void IDischarger.Start(decimal lowerDischargeThreshold, decimal voltageBeforeDischarging, decimal valuesChangeDiscreteness)
        {
            _dataModel.Voltage = voltageBeforeDischarging;
            _dataModel.CurrentDateTime = DateTime.Now;

            _prevVoltage = voltageBeforeDischarging;
            _lowerDischargeThreshold = lowerDischargeThreshold;
            _valuesChangeDiscreteness = valuesChangeDiscreteness;

            _dataModel.IsDischargingStarted = true;
            _resultsCalculator.DischargingStartDateTime = _dataModel.CurrentDateTime;

            _dataSaver.Save(_dataModel);
        }

        void IDischarger.Discharge(decimal voltage, decimal current)
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

            _dataSaver.Save(_dataModel);
        }

        private void StopDischarging(decimal voltage, decimal current)
        {
            _dataModel.Voltage = voltage;
            _dataModel.Current = current;
            _dataModel.CurrentDateTime = DateTime.Now;

            _resultsCalculator.DischargingCurrent = current;
            _resultsCalculator.DischargingEndDateTime = _dataModel.CurrentDateTime;
            _resultsCalculator.CalculateResults();

            _dataModel.IsDischargingCompleted = true;
            _controller.AutoStopDischarging();
            _dataSaver.Save(_dataModel);
        }
        #endregion
    }
}