﻿namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IUiInteractorInputPort
    {
        void SetDischargingParams(string lowerDischargeThreshold, string valuesChangeDiscreteness, string dischargingCurrent);
        void StartDischarging(Dictionary<string, string> portConnectionParameters);
        void StopDischarging();


        // TODO: remove this
        public ITempDataSaver TempDataSaver { set; }

    }
}
