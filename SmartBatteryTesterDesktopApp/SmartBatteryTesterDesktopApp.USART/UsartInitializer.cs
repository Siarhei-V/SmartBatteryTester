using SmartBatteryTesterDesktopApp.PORT;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    public class UsartInitializer
    {
        SerialPort _port;
        IUsartInteractorInputPort _usartInteractorInputPort;
        IUsartParametersConverter _paramsConverter;

        public UsartInitializer()
        {
            _port = new SerialPort();
            _paramsConverter = new UsartParametersConverter();

            _usartInteractorInputPort = PortInteractor.Instance;
            _usartInteractorInputPort.PortController = new UsartController(_port, _paramsConverter);

            new UsartDataSender(_port, _usartInteractorInputPort);
        }
    }
}
