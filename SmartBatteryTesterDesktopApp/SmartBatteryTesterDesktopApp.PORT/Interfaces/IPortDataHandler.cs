using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortDataHandler
    {
        IPortDataTransmitter PortTransmitter { set; }
        string Voltage { get; }
        string Current { get; }
        void InitializeDataHandler(IDischargerDataSaver dischargerDataSaver);
        void StartDischarging(Dictionary<string, string> portConnectionParameters);
        void HandleStartValues(string lowerDischargerVoltage, string startVoltage, string valuesChangeDiscreteness);
        void HandleIntermediateValues(string voltage, string current, DateTime dateTime);
    }
}
