namespace SmartBatteryTesterDesktopApp.BL.Interfaces
{
    internal interface IResultsCalculator
    {
        internal DateTime DischargingStartDateTime { set; }
        internal DateTime DischargingEndDateTime { set; }
        internal decimal DischargingCurrent { set; }
        internal DischargerInfoDto DischargerInfoDto { set; }

        internal void CalculateResults();
    }
}