using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    public interface IDischargerFacade
    {
        void InitializeDischarger(IDischargerDataSaver dataSaver, IDischargerController dischargerController);
        void StartDischarging(decimal lowerDischargeThreshold, decimal voltageBeforeDischarging, decimal valuesChangeDiscreteness);
        void Discharge(decimal voltage, decimal current);
    }
}