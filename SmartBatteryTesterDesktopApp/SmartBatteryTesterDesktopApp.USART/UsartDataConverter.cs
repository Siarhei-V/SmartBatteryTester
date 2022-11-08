using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    internal class UsartDataConverter : IUsartDataConverter
    {
        decimal IUsartDataConverter.ConvertDataFromUsart(object? sender)
        {
            var convertedData = ((SerialPort)sender).ReadLine();
            convertedData = convertedData.Replace(".", ",");
            return Convert.ToDecimal(convertedData);
        }
    }
}
