using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    internal class UsartDataConverter : IUsartDataConverter
    {
        //void IUsartDataConverter.ConvertUsartArgsFromStringToDecimal(EventArgs e, out decimal lowerVoltageThreshol,
        //    out decimal valuesChangeDiscreteness)
        //{
        //    string convertedStringValue;

        //    convertedStringValue = ((UsartEventArgs)e).LowerVoltageThreshol;
        //    lowerVoltageThreshol = Convert.ToDecimal(convertedStringValue);

        //    convertedStringValue = ((UsartEventArgs)e).ValuesChangeDiscretennes;
        //    valuesChangeDiscreteness = Convert.ToDecimal(convertedStringValue);
        //}

        decimal IUsartDataConverter.ConvertDataFromUsart(object? sender)
        {
            var convertedData = ((SerialPort)sender).ReadLine();
            convertedData = convertedData.Replace(".", ",");
            return Convert.ToDecimal(convertedData);
        }
    }
}
