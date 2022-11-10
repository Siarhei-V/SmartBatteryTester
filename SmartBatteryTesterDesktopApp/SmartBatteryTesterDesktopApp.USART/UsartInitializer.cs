using SmartBatteryTesterDesktopApp.PORT;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    public class UsartInitializer
    {
        SerialPort _port;
        IUsartParametersConverter _converter;
        IPortDataTransmitter _portDataTransmitter;
        IUsartDataHandler _usartDataHandler;
        IPortInteractor _portInteractor;
        IUsartDataConverter _usartDataConverter;

        public UsartInitializer()
        {
            _port = new SerialPort();
            _converter = new UsartParametersConverter();
            _usartDataConverter = new UsartDataConverter();

            _portDataTransmitter = new UsartDataTransmitter(_port, _converter);
            _portInteractor = PortInteractor.Instance;
            _usartDataHandler = new UsartDataHanlder(_portDataTransmitter, _portInteractor, _usartDataConverter, _port);
        }
    }
}
