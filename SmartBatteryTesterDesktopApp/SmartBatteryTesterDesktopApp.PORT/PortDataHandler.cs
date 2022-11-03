using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortDataHandler : IPortDataHandler
    {
        private object _connectionParameters;

        public PortDataHandler(object connectionParameters)
        {
            _connectionParameters = connectionParameters;
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }
        public void GetData(decimal voltage, decimal current, DateTime currentDateTime)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
