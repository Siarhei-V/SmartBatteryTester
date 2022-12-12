using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;
using Xunit;

namespace SmartBatteryTesterDesktopApp.USART
{
    public class UsartParametersConverterTest
    {
        IUsartParametersConverter _converter;

        public UsartParametersConverterTest()
        {
            _converter = new UsartParametersConverter();
        }

        [Theory]
        [InlineData("None", Parity.None)]
        [InlineData("Odd", Parity.Odd)]
        [InlineData("Even", Parity.Even)]
        [InlineData("Mark", Parity.Mark)]
        [InlineData("Space", Parity.Space)]
        [InlineData("", Parity.None)]
        public void ConvertStringToParity_CheckConverting(string str, Parity expectedResult)
        {
            // Arrange

            // Act
            var result = _converter.ConvertStringToParity(str);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("None", StopBits.None)]
        [InlineData("One", StopBits.One)]
        [InlineData("Two", StopBits.Two)]
        [InlineData("OnePointFive", StopBits.OnePointFive)]
        [InlineData("", StopBits.One)]
        public void ConvertStringToStopBits_CheckConverting(string str, StopBits expectedResult)
        {
            // Arrange

            // Act
            var result = _converter.ConvertStringToStopBits(str);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
