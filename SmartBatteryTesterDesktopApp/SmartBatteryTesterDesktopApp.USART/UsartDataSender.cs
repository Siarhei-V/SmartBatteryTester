using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    internal class UsartDataSender
    {
        SerialPort _port;
        IUsartInteractorInputPort _usartInteractorPort;
        IUsartDataConverter _usartDataConverter;

        internal UsartDataSender(SerialPort port, IUsartInteractorInputPort usartInteractorPort,
            IUsartDataConverter usartDataConverter)
        {
            _port = port;
            _usartInteractorPort = usartInteractorPort;
            _usartDataConverter = usartDataConverter;

            _port.DataReceived += (sender, args) =>
            {
                string data;

                data = _usartDataConverter.ConvertDataFromUsart(sender);
                _usartInteractorPort.SendUsartData(data);
            };
        }
    }
}
