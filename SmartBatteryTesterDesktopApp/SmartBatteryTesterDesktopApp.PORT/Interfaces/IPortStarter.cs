using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortStarter
    {
        IPortDataTransmitter PortDataTransmitter { set; }

        void StartDataTransfer();
    }
}