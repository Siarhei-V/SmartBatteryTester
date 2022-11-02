using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    internal class ResultsCalculator : IResultsCalculator
    {
        private DischargerInfoDto _dischargerInfoDto;
        DischargerInfoDto IResultsCalculator.DischargerInfoDto
        {
            set => _dischargerInfoDto = value;
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
            _dischargerInfoDto.ResultCapacity = Convert.ToDecimal((_dischargingEndDateTime - _dischargingStartDateTime)
                .TotalHours) * _dischargingCurrent;

            _dischargerInfoDto.DischargeDuration = _dischargingEndDateTime - _dischargingStartDateTime;
        }
    }
}
