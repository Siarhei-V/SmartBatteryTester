using Moq;
using SmartBatteryTesterWebApp.API.Controllers;
using SmartBatteryTesterWebApp.API.Models;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using System;
using Xunit;

namespace SmartBatteryTesterWebApp.API.Tests
{
    public class MeasurementsControllerTest
    {
        Mock<IMeasurementInputService> _measurementInputServiceMock;
        Mock<IMeasurementOutputService> _measurementOutputServiceMock;
        MeasurementsController _measurementsController;

        public MeasurementsControllerTest()
        {
            _measurementInputServiceMock = new Mock<IMeasurementInputService>();
            _measurementOutputServiceMock = new Mock<IMeasurementOutputService>();
            _measurementsController = new MeasurementsController(_measurementInputServiceMock.Object, 
                _measurementOutputServiceMock.Object);
        }

        [Fact]
        public void AddMeasurementSetAsync_CheckMakeMeasurementSetAsyncColling()
        {
            // Arrange

            // Act
            _ = _measurementsController.AddMeasurementSetAsync(new MeasurementSetModel());

            // Assert
            _measurementInputServiceMock.Verify(m => m.MakeMeasurementSetAsync(It.IsAny<MeasurementSetDTO>()), Times.Once);
        }

        [Fact]
        public void AddMeasurementAsync_CheckMakeMeasurementAsyncColling()
        {
            // Arrange

            // Act
            _ = _measurementsController.AddMeasurementAsync(new MeasurementModel());

            // Assert
            _measurementInputServiceMock.Verify(m => m.MakeMeasurementAsync(It.IsAny<MeasurementDTO>()), Times.Once);
        }

        [Fact]
        public void UpdateMeasurementSetAsync_UpdateMeasurementSetAsyncColling()
        {
            // Arrange

            // Act
            _ = _measurementsController.UpdateMeasurementSetAsync(new MeasurementSetModel());

            // Assert
            _measurementInputServiceMock.Verify(m => m.UpdateMeasurementSetAsync(It.IsAny<MeasurementSetDTO>()), Times.Once);
        }

        [Fact]
        public async void FindMeasurementSetAsync_CheckReturnedValue()
        {
            // Arrange
            string measurementName = "test 1";
            string status = "mySuperPuperStatus";
            var duration = new TimeSpan(1, 2, 3);
            decimal capacity = 100500;

            var measurementSetDTO = new MeasurementSetDTO()
            {
                Id = 1,
                MeasurementName = measurementName,
                MeasurementStatus = status,
                DischargeDuration = duration,
                ResultCapacity = capacity
            };

            _measurementOutputServiceMock.Setup(m => m.FindMeasurementSetAsync(status)).ReturnsAsync(measurementSetDTO);

            // Act
            var result = await _measurementsController.FindMeasurementSetAsync(status);

            // Assert
            Assert.IsType<MeasurementSetModel>(result);
            Assert.Equal(measurementSetDTO.Id, result.Id);
            Assert.Equal(measurementSetDTO.MeasurementName, result.MeasurementName);
            Assert.Equal(measurementSetDTO.MeasurementStatus, result.MeasurementStatus);
            Assert.Equal(measurementSetDTO.DischargeDuration, result.DischargeDuration);
            Assert.Equal(measurementSetDTO.ResultCapacity, result.ResultCapacity);

        }
    }
}