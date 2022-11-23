namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IUiInteractorInputPort
    {
        void SetDischargingParams(string lowerDischargeThreshold, string valuesChangeDiscreteness, string dischargingCurrent);
        void StartDischarging(Dictionary<string, string> portConnectionParameters);
        void StopDischarging();
        void CreateNewTest(string testName);

        IDataGetter DataSender { set; }
    }
}
