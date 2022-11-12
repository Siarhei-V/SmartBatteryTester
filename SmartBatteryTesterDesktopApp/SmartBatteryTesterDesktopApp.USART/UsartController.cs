using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    internal class UsartController : IPortController
    {
        SerialPort _port;
        IUsartParametersConverter _converter;

        internal UsartController(SerialPort serialPort, IUsartParametersConverter usartDataConverter)
        {
            _port = serialPort;
            _converter = usartDataConverter;
        }

        public void StartDischarging(Dictionary<string, string> parameters)
        {
            _port.PortName  = parameters["PortName"];
            _port.BaudRate  = Convert.ToInt32(parameters["BaudRate"]);
            _port.DataBits  = Convert.ToInt32(parameters["DataBits"]);
            _port.Parity    = _converter.ConvertStringToParity(parameters["Parity"]);
            _port.StopBits  = _converter.ConvertStringToStopBits(parameters["StopBits"]);

            try
            {
                _port.Open();
                _port.WriteLine("2");
            }
            catch (Exception)
            {
                throw;      // TODO: Implement exceptions handling everywhere
            }
        }

        public void StopDischarging()
        {
            _port?.WriteLine("1");
            _port?.Close();
        }
    }
}