using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART.Interfaces
{
    internal interface IUsartParametersConverter
    {
        internal Parity ConvertStringToParity(string str);
        internal StopBits ConvertStringToStopBits(string str);
    }
}
