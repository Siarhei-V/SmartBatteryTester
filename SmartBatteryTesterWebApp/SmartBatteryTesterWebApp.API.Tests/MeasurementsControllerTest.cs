using Moq;
using SmartBatteryTesterWebApp.API.Controllers;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using Xunit;

namespace SmartBatteryTesterWebApp.API.Tests
{
    public class MeasurementsControllerTest
    {
        Mock<IMeasurementInputService> _measurementInputServiceMock;
        MeasurementsController _measurementsController;

        public MeasurementsControllerTest()
        {
            _measurementInputServiceMock = new Mock<IMeasurementInputService>();
            _measurementsController = new MeasurementsController(_measurementInputServiceMock.Object);
        }

        [Fact]
        public void AddMeasurementSetAsync_Check()
        {
            // Arrange

            // Act
            //_ = _measurementsController.AddMeasurementSetAsync(new MeasurementSetDTO());

            // Assert

        }
    }
}