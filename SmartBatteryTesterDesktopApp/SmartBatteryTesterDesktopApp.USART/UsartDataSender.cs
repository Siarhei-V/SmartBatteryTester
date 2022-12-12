using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    internal class UsartDataSender
    {
        SerialPort _port;
        IUsartInteractorInputPort _usartInteractorPort;

        internal UsartDataSender(SerialPort port, IUsartInteractorInputPort usartInteractorPort)
        {
            _port = port;
            _port.ReadTimeout = 1000;
            _usartInteractorPort = usartInteractorPort;

            _port.DataReceived += (sender, args) =>
            {
                string data;

                try
                {
                    data = ((SerialPort)sender).ReadLine();
                    data = data.Replace(".", ",");
                }
                catch (Exception)
                {
                    data = string.Empty;
                }

                try
                {
                    _usartInteractorPort.SendUsartData(data);
                }
                catch (Exception)
                {
                    throw;
                }
            };
        }
    }
}
