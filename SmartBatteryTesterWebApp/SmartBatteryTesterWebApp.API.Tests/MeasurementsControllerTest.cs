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
        public void AddMeasurementSet_CheckMakeMeasurementSetColling()
        {
            // Arrange

            // Act
            _measurementsController.AddMeasurementSet(new MeasurementSetModel());

            // Assert
            _measurementInputServiceMock.Verify(m => m.MakeMeasurementSet(It.IsAny<MeasurementSetDTO>()), Times.Once);
        }

        [Fact]
        public void AddMeasurement_CheckMakeMeasurementColling()
        {
            // Arrange

            // Act
            _measurementsController.AddMeasurement(new MeasurementModel());

            // Assert
            _measurementInputServiceMock.Verify(m => m.MakeMeasurement(It.IsAny<MeasurementDTO>()), Times.Once);
        }

        [Fact]
        public void UpdateMeasurementSet_UpdateMeasurementSetColling()
        {
            // Arrange

            // Act
            _measurementsController.UpdateMeasurementSet(new MeasurementSetModel());

            // Assert
            _measurementInputServiceMock.Verify(m => m.UpdateMeasurementSet(It.IsAny<MeasurementSetDTO>()), Times.Once);
        }

        [Fact]
        public void FindMeasurementSet_CheckReturnedValue()
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

            _measurementOutputServiceMock.Setup(m => m.FindMeasurementSet(status)).Returns(measurementSetDTO);

            // Act
            var result = _measurementsController.FindMeasurementSet(status);

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