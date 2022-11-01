namespace SmartBatteryTesterDesktopApp.BL.Interfaces
{
    public interface IDischarger
    {
        void Start(decimal lowerDischargeThreshold, decimal voltageBeforeDischarging, decimal valuesChangeDiscreteness);

        void Discharge(decimal voltage, decimal current);
    }
}