namespace SmartBatteryTesterDesktopApp.BL.Interfaces
{
    public interface IDischarger
    {
        internal IResultsCalculatorFactory CapacityCalculatorFactory { set; }

        public void Start(decimal lowerDischargeThreshold, decimal voltageBeforeDischarging, decimal valuesChangeDiscreteness);

        public void Discharge(decimal voltage, decimal current);
    }
}