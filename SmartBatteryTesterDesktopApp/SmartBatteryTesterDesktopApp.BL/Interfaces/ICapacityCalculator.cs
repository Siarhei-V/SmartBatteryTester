namespace SmartBatteryTesterDesktopApp.BL.Interfaces
{
    internal interface ICapacityCalculator
    {
        internal DateTime DischargingStartDateTime { set; }
        internal DateTime DischargingEndDateTime { set; }
        internal decimal DischargingCurrent { set; }

        internal decimal GetCapacity();
    }
}