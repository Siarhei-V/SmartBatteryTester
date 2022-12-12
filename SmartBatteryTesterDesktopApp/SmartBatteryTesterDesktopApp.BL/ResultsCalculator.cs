using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    internal class ResultsCalculator : IResultsCalculator
    {
        private DischargerModel _dischargerModel;

        internal ResultsCalculator(DischargerModel dischargerModel)
        {
            _dischargerModel = dischargerModel;
        }

        private DateTime _dischargingStartDateTime;
        DateTime IResultsCalculator.DischargingStartDateTime 
        {
            set => _dischargingStartDateTime = value;
        }

        private DateTime _dischargingEndDateTime;
        DateTime IResultsCalculator.DischargingEndDateTime
        {
            set => _dischargingEndDateTime = value;
        }

        private decimal _dischargingCurrent;
        decimal IResultsCalculator.DischargingCurrent
        {
            set => _dischargingCurrent = value;
        }

        void IResultsCalculator.CalculateResults()
        {
            _dischargerModel.ResultCapacity = Convert.ToDecimal((_dischargingEndDateTime - _dischargingStartDateTime)
                .TotalHours) * _dischargingCurrent;

            _dischargerModel.DischargeDuration = _dischargingEndDateTime - _dischargingStartDateTime;
        }
    }
}
