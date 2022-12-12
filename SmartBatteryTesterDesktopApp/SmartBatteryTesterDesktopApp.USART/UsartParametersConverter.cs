using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    internal class UsartParametersConverter : IUsartParametersConverter
    {
        Parity IUsartParametersConverter.ConvertStringToParity(string str)
        {
            Parity parity;

            switch (str)
            {
                case "None"     : parity = Parity.None;     break;
                case "Odd"      : parity = Parity.Odd;      break;
                case "Even"     : parity = Parity.Even;     break;
                case "Mark"     : parity = Parity.Mark;     break;
                case "Space"    : parity = Parity.Space;    break;

                default         : parity = Parity.None;     break;
            }
            return parity;
        }

        StopBits IUsartParametersConverter.ConvertStringToStopBits(string str)
        {
            StopBits stopBits;

            switch (str)
            {
                case "None"         : stopBits = StopBits.None;         break;
                case "One"          : stopBits = StopBits.One;          break;
                case "Two"          : stopBits = StopBits.Two;          break;
                case "OnePointFive" : stopBits = StopBits.OnePointFive; break;

                default             : stopBits = StopBits.One;          break;
            }
            return stopBits;
        }
    }
}
