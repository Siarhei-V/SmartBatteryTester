using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL.Interfaces
{
    public interface IDischargerInitializer
    {
        IDischarger InitializeDischarger();
    }
}