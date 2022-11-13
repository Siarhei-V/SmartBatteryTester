using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.UI.Infrastructure;
using SmartBatteryTesterDesktopApp.UI.Models;
using Xunit;

namespace SmartBatteryTesterDesktopApp.UI.Tests
{
    public class PortDataGetterTest
    {
        DischargingParameters _parameters;
        IDataGetter _portDataGetter;
        bool _isEventTriggered;

        public PortDataGetterTest()
        {
            _parameters = new DischargingParameters();
            _portDataGetter = new PortDataGetter(_parameters);
        }

        [Fact]
        public void GetData_CheckEvent()
        {
            // Arrange
            _portDataGetter.DataChanged += (sender, args) => _isEventTriggered = true;

            // Act
            _portDataGetter.GetData("");

            // Assert
            Assert.True(_isEventTriggered);
        }

        [Theory]
        [InlineData("")]
        [InlineData("10")]
        [InlineData("500")]
        [InlineData("aaa")]
        public void GetData_CheckSavingParameters(string inputData)
        {
            // Arrange

            // Act
            _portDataGetter.GetData(inputData);

            // Assert
            Assert.Equal(inputData, _parameters.Voltage);
        }
    }
}