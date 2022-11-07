namespace SmartBatteryTesterDesktopApp.BL.Interfaces
{
    internal interface IDischarger
    {
        internal void Start(decimal lowerDischargeThreshold, decimal voltageBeforeDischarging, decimal valuesChangeDiscreteness);

        internal void Discharge(decimal voltage, decimal current);
    }
}