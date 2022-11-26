namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IUiInteractorInputPort
    {
        void SetDischargingParams(string lowerDischargeThreshold, string valuesChangeDiscreteness, string dischargingCurrent);
        void ConnectToPort(Dictionary<string, string> portConnectionParameters);
        void StartDischarging(bool isOnlineMode);
        void StopDischarging();
        void DisconnectWeb();
        void CreateNewTest(string testName);

        IDataGetter DataSender { set; }
    }
}
