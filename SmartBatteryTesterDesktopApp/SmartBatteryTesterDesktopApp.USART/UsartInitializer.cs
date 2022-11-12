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
        IUsartInteractorInputPort _usartInteractorInputPort;
        IUsartParametersConverter _paramsConverter;

        public UsartInitializer()
        {
            _port = new SerialPort();
            _paramsConverter = new UsartParametersConverter();

            _portInteractor = PortInteractor.Instance;
            _portInteractor.PortController = new UsartController(_port, _paramsConverter);

            _usartInteractorInputPort = PortInteractor.Instance;
            new UsartDataSender(_port, _usartInteractorInputPort);
        }
    }
}
