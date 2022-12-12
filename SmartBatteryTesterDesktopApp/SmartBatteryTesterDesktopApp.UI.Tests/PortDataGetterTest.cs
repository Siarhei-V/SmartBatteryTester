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

        public PortDataGetterTest()
        {
            _parameters = new DischargingParameters();
            _portDataGetter = new PortDataGetter(_parameters);
        }

        [Fact]
        public void GetData_CheckEvent()
        {
            // Arrange
            bool isEventTriggered = false;
            _portDataGetter.DataChanged += (sender, args) => isEventTriggered = true;

            // Act
            _portDataGetter.GetData("");

            // Assert
            Assert.True(isEventTriggered);
        }

        [Fact]
        public void GetPortStatus_CheckEvent()
        {
            // Arrange
            bool isEventTriggered = false;
            _portDataGetter.DataChanged += (sender, args) => isEventTriggered = true;

            // Act
            _portDataGetter.GetPortStatus("");

            // Assert
            Assert.True(isEventTriggered);
        }

        [Fact]
        public void GetWebtStatus_CheckEvent()
        {
            // Arrange
            bool isEventTriggered = false;
            _portDataGetter.DataChanged += (sender, args) => isEventTriggered = true;

            // Act
            _portDataGetter.GetWebStatus("");

            // Assert
            Assert.True(isEventTriggered);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("10", "Port Status", "Web Status")]
        public void GetData_CheckSavingParameters(string data, string portStatus, string webStatus)
        {
            // Arrange

            // Act
            _portDataGetter.GetData(data);
            _portDataGetter.GetPortStatus(portStatus);
            _portDataGetter.GetWebStatus(webStatus);

            // Assert
            Assert.Equal(data, _parameters.Voltage);
            Assert.Equal(portStatus, _parameters.PortConnectionStatus);
            Assert.Equal(webStatus, _parameters.WebConnectionStatus);
        }
    }
}