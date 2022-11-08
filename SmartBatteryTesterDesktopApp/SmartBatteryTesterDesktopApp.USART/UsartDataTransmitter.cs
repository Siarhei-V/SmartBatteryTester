using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    internal class UsartDataTransmitter : IPortDataTransmitter
    {
        SerialPort _port;
        IUsartParametersConverter _converter;
        Dictionary<string, string>? _parameters;

        internal UsartDataTransmitter(SerialPort serialPort, IUsartParametersConverter usartDataConverter)
        {
            _port = serialPort;
            _converter = usartDataConverter;
        }

        public Dictionary<string, string> Parameters
        {
            get => _parameters;
            set => _parameters = value;
        }

        public void StartDataTransfer()
        {
            _port.PortName  = _parameters["PortName"];
            _port.BaudRate  = Convert.ToInt32(_parameters["BaudRate"]);
            _port.DataBits  = Convert.ToInt32(_parameters["DataBits"]);
            _port.Parity    = _converter.ConvertStringToParity(_parameters["Parity"]);
            _port.StopBits  = _converter.ConvertStringToStopBits(_parameters["StopBits"]);

            try
            {
                _port.Open();
            }
            catch (Exception)
            {
                throw;      // TODO: Implement exceptions handling everywhere
            }
        }

        public void StopDataTransfer()
        {
            _port?.Close();
        }
    }
}