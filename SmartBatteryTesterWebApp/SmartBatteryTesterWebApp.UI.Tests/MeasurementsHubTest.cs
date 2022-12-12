using Moq;
using SmartBatteryTesterWebApp.UI.Infrastructure;
using SmartBatteryTesterWebApp.UI.Models;
using System.Collections.Generic;
using Xunit;

namespace SmartBatteryTesterWebApp.UI.Tests
{
    public class MeasurementsHubTest
    {
        Mock<IMeasurementChartDataCreator> _chartCreatorMock;
        MeasurementsHub _measurementsHub;

        public MeasurementsHubTest()
        {
            _chartCreatorMock = new Mock<IMeasurementChartDataCreator>();
            _measurementsHub = new MeasurementsHub(_chartCreatorMock.Object);
        }

        [Fact]
        public void Send_CheckGetLineChartDataMethodColling()
        {
            // Arrange
            var measurement = new MeasurementViewModel() { Current = 1, Voltage = 2 };
            var measurementsList = new List<MeasurementViewModel>() { measurement };

            // Act
            _ = _measurementsHub.Send(It.IsAny<string>(), measurement);

            // Assert
            _chartCreatorMock.Verify(m => m.GetLineChartData(measurementsList), Times.Once);
        }
    }
}
