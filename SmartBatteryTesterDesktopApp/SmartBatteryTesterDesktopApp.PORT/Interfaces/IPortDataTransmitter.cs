﻿namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortDataTransmitter
    {
        public void StartDataTransfer(Dictionary<string, string> parameters);
        public void StopDataTransfer();
    }
}
