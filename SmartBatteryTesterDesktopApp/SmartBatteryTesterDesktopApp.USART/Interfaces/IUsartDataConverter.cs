namespace SmartBatteryTesterDesktopApp.USART.Interfaces
{
    internal interface IUsartDataConverter
    {
        //internal void ConvertUsartArgsFromStringToDecimal(EventArgs e, out decimal lowerVoltageThreshol,
        //    out decimal valuesChangeDiscreteness);

        internal decimal ConvertDataFromUsart(object? sender);
    }
}
