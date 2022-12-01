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
        public void MakeMeasurement_CheckCreateMethodColling()
        {
            // Arrange

            // Act
            _measurementInputService.MakeMeasurement(It.IsAny<MeasurementDTO>());

            // Assert
            _measurementRepositoryMock.Verify(m => m.Create(It.IsAny<Measurement>()), Times.Once);
        }

        [Fact]
        public void MakeMeasurementSet_CheckCreateMethodColling()
        {
            // Arrange

            // Act
            _measurementInputService.MakeMeasurementSet(It.IsAny<MeasurementSetDTO>());

            // Assert
            _measurementSetRepositoryMock.Verify(m => m.Create(It.IsAny<MeasurementSet>()), Times.Once);
        }

        [Fact]
        public void UpdateMeasurementSet_CheckUpdateMethodColling()
        {
            // Arrange

            // Act
            _measurementInputService.UpdateMeasurementSet(It.IsAny<MeasurementSetDTO>());

            // Assert
            _measurementSetRepositoryMock.Verify(m => m.Update(It.IsAny<MeasurementSet>()), Times.Once);
        }
    }
}