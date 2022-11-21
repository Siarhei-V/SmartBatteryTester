namespace SmartBatteryTesterDesktopApp.BL.Interfaces
{
    public interface IDischarger
    {
        internal IResultsCalculator ResultsCalculator { set; }
        bool IsNewDataReceived { get; }
        public DischargerModel GetDischargingData();
        void SetDischargingParams(decimal lowerDischargeThreshold, decimal valuesChangeDiscreteness, decimal dischargingCurrent);
        void Discharge(decimal voltage, decimal current, DateTime dateTime);
    }
}