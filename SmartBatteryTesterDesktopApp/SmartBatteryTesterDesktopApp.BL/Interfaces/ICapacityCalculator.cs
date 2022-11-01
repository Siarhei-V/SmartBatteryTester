namespace SmartBatteryTesterDesktopApp.BL.Interfaces
{
    internal interface ICapacityCalculator
    {
        DateTime DischargingStartDateTime { get; set; }
        DateTime DischargingEndDateTime { get; set; }
        decimal DischargingCurrent { get; set; }

        decimal GetCapacity();
    }
}