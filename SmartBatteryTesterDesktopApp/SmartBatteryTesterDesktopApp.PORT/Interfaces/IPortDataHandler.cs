using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortDataHandler
    {
        public IPortDataTransmitter PortTransmitter { set; }
        public void InitializeDataHandler(IDischargerDataSaver dischargerDataSaver);
        void StartDischarging(Dictionary<string, string> portConnectionParameters);
        void HandleStartValues(string lowerDischargerVoltage, string startVoltage, string valuesChangeDiscreteness);
        void HandleIntermediateValues(string voltage, string current, DateTime dateTime);
    }
}
