using SmartBatteryTesterDesktopApp.PORT;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    public class UsartInitializer
    {
        SerialPort _port;
        IControllerInstanceSetter _portInteractor;
        IUsartParametersConverter _converter;

        public UsartInitializer()
        {
            _port = new SerialPort();
            _converter = new UsartParametersConverter();

            _portInteractor = PortInteractor.Instance;
            _portInteractor.PortController = new UsartController(_port, _converter);
        }
    }
}
