using SmartBatteryTesterDesktopApp.USART.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.USART.Tests
{
    public class UsartDataConverterTest
    {
        IUsartDataConverter _usartDataConverter;
        UsartEventArgs _eventArgs;

        public UsartDataConverterTest()
        {
            _usartDataConverter = new UsartDataConverter();
            _eventArgs = new UsartEventArgs();
        }

        //[Fact]
        //public void ConvertUsartArgsFromStringToDecimal_CheckConvertion()
        //{
        //    // Arrange
        //    _eventArgs.LowerVoltageThreshol = "20";
        //    _eventArgs.ValuesChangeDiscretennes = "10";

        //    // Act
        //    _usartDataConverter.ConvertUsartArgsFromStringToDecimal(_eventArgs, out decimal firstValue, out decimal secondValue);

        //    // Assert
        //    Assert.Equal(20, firstValue);
        //    Assert.Equal(10, secondValue);

        //    Assert.IsType<decimal>(firstValue);
        //    Assert.IsType<decimal>(secondValue);
        //}
    }
}
