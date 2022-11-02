using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    public interface IDischargerInitializer
    {
        public IDischarger DischargerImplementation { get; }
    }
}