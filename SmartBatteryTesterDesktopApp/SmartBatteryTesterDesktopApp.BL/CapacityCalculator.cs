using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    internal class CapacityCalculator : ICapacityCalculator
    {
        private DischargerInfoDto _dischargerInfoDto;

        public DateTime DischargingStartDateTime { get; set; }
        public DateTime DischargingEndDateTime { get; set; }
        public decimal DischargingCurrent { get; set; }

        public CapacityCalculator()
        {
            _dischargerInfoDto = new DischargerInfoDto();
        }

        public decimal GetCapacity()
        {
            return _dischargerInfoDto.ResultCapacity = Convert.ToDecimal((DischargingEndDateTime - DischargingStartDateTime)
                .TotalHours) * DischargingCurrent;
        }
    }
}
