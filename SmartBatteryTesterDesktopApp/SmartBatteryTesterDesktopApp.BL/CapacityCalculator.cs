using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    internal class CapacityCalculator : ICapacityCalculator
    {
        private DischargerInfoDto _dischargerInfoDto;

        private DateTime _dischargingStartDateTime;
        DateTime ICapacityCalculator.DischargingStartDateTime 
        {
            set => _dischargingStartDateTime = value;
        }

        private DateTime _dischargingEndDateTime;
        DateTime ICapacityCalculator.DischargingEndDateTime
        {
            set => _dischargingEndDateTime = value;
        }

        private decimal _dischargingCurrent;
        decimal ICapacityCalculator.DischargingCurrent
        {
            set => _dischargingCurrent = value;
        }

        internal CapacityCalculator()
        {
            _dischargerInfoDto = new DischargerInfoDto();
        }

        decimal ICapacityCalculator.GetCapacity()
        {
            return _dischargerInfoDto.ResultCapacity = Convert.ToDecimal((_dischargingEndDateTime - _dischargingStartDateTime)
                .TotalHours) * _dischargingCurrent;
        }
    }
}
