using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortDataHandler
    {
        void HandleStartValues(decimal lowerDischargerVoltage, decimal startVoltage, decimal valuesChangeDiscreteness);
        void HandleIntermediateValues(decimal voltage, decimal current, DateTime dateTime);
    }
}
