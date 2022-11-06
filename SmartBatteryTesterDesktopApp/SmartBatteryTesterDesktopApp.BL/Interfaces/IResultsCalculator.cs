namespace SmartBatteryTesterDesktopApp.BL.Interfaces
{
    internal interface IResultsCalculator
    {
        internal DateTime DischargingStartDateTime { set; }
        internal DateTime DischargingEndDateTime { set; }
        internal decimal DischargingCurrent { set; }

        internal void CalculateResults();
    }
}