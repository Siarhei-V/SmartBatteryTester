using Moq;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.BLL.Services;
using SmartBatteryTesterWebApp.DAL.Entities;
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
            _measurementInputService.MakeMeasurementAsync(It.IsAny<MeasurementDTO>());

            // Assert
            _measurementRepositoryMock.Verify(m => m.CreateAsync(It.IsAny<Measurement>()), Times.Once);
        }

        [Fact]
        public void MakeMeasurementSetAsync_CheckCreateAsyncMethodColling()
        {
            // Arrange

            // Act
            _measurementInputService.MakeMeasurementSetAsync(It.IsAny<MeasurementSetDTO>());

            // Assert
            _measurementSetRepositoryMock.Verify(m => m.CreateAsync(It.IsAny<MeasurementSet>()), Times.Once);
        }

        [Fact]
        public void UpdateMeasurementSetAsync_CheckUpdateAsyncMethodColling()
        {
            // Arrange

            // Act
            _measurementInputService.UpdateMeasurementSetAsync(It.IsAny<MeasurementSetDTO>());

            // Assert
            _measurementSetRepositoryMock.Verify(m => m.UpdateAsync(It.IsAny<MeasurementSet>()), Times.Once);
        }
    }
}