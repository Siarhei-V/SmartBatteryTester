using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    internal class UsartDataConverter : IUsartDataConverter
    {
        string IUsartDataConverter.ConvertDataFromUsart(object? sender)
        {
            string receivedData;
            try
            {
                receivedData = ((SerialPort)sender).ReadLine();
                receivedData = receivedData.Replace(".", ",");
            }
            catch (Exception)
            {
                throw;
            }
            return receivedData;
        }
    }
}
