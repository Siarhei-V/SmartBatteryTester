using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortDataHandler
    {
        public IPortDataTransmitter PortTransmitter { set; }
        public void InitializeDataHandler(IDischargerDataSaver dischargerDataSaver);
        void StartDischarging(Dictionary<string, string> portConnectionParameters);
        void HandleStartValues(decimal lowerDischargerVoltage, decimal startVoltage, decimal valuesChangeDiscreteness);
        void HandleIntermediateValues(decimal voltage, decimal current, DateTime dateTime);
    }
}
