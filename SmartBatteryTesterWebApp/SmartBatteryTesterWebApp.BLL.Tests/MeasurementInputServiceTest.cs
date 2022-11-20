using Moq;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.BLL.Services;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;
using Xunit;

namespace SmartBatteryTesterWebApp.BLL.Tests
{
    public class MeasurementInputServiceTest
    {
        Mock<IMeasurementRepository> _measurementRepositoryMock;
        Mock<IMeasurementSetRepository> _measurementSetRepositoryMock;
        IMeasurementInputService _measurementInputService;

        public MeasurementInputServiceTest()
        {
            _measurementRepositoryMock = new Mock<IMeasurementRepository>();
            _measurementSetRepositoryMock = new Mock<IMeasurementSetRepository>();
            _measurementInputService = new MeasurementInputService(_measurementRepositoryMock.Object,
                _measurementSetRepositoryMock.Object);
        }

        [Fact]
        public void MakeMeasurementAsync_CheckCreateAsyncMethodColling()
        {
            // Arrange

            // Act

            // Assert

        }
    }
}