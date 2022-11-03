using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortInitializer : IPortInitializer
    {
        private IPortDataHandler _portDataHandler;

        public IPortDataHandler PortDataHandlerImplementation
        {
            get => _portDataHandler;
        }

        public PortInitializer(object connectionParameters)
        {
            _portDataHandler = new PortDataHandler(connectionParameters);
        }
    }
}
