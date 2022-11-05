using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortStarter : IPortStarter
    {
        private static PortStarter? _portStarter;
        private IPortDataTransmitter? _portDataTransmitter;

        private PortStarter() { }

        public static PortStarter Instance
        {
            get => _portStarter ?? (_portStarter = new PortStarter());
        }

        public IPortDataTransmitter PortDataTransmitter
        {
            set => _portDataTransmitter = value;
        }

        public void StartDataTransfer()
        {
            _portDataTransmitter.TransmitPortData();
        }
    }
}
