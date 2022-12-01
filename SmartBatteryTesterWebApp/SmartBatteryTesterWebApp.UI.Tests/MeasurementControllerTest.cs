using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.UI.Controllers;
using SmartBatteryTesterWebApp.UI.Infrastructure;
using SmartBatteryTesterWebApp.UI.Models;
using SmartBatteryTesterWebApp.UI.Models.Chart;
using System.Collections.Generic;
using Xunit;

namespace SmartBatteryTesterWebApp.UI.Tests
{
    public class MeasurementControllerTest
    {
        Mock<IMeasurementOutputService> _measurementServiceMock;
        Mock<IMeasurementChartDataCreator> _chartCreatorMock;
        MeasurementController _measurementController;

        public MeasurementControllerTest()
        {
            _measurementServiceMock = new Mock<IMeasurementOutputService>();
            _chartCreatorMock = new Mock<IMeasurementChartDataCreator>();
            
            _measurementServiceMock.Setup(m => m.FindMeasurementSet("Батарея разряжается")).Returns(new MeasurementSetDTO());

            _measurementController = new MeasurementController(_measurementServiceMock.Object, _chartCreatorMock.Object);
        }

        [Fact]
        public void Index_CheckMethodsCollingWhenMeasurementSetWasFound()
        {
            // Arrange

            // Act
            _measurementController.Index();

            // Assert
            _measurementServiceMock.Verify(m => m.FindMeasurementSet(It.IsAny<string>()), Times.Once);
            _measurementServiceMock.Verify(m => m.GetMeasurement(It.IsAny<int>()), Times.Once);
            _chartCreatorMock.Verify(m => m.GetLineChartData(It.IsAny<List<MeasurementViewModel>>()), Times.Once);
        }

        [Fact]
        public void Index_CheckReturnedValue()
        {
            // Arrange
            _chartCreatorMock.Setup(m => m.GetLineChartData(It.IsAny<List<MeasurementViewModel>>()))
                .Returns(CreateChartData());

            // Act
            var result = _measurementController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ChartJsData>(viewResult.Model);
            Assert.NotNull(model);
            Assert.Equal(2, model.Labels.Count);
            Assert.Equal(3, model.Datasets.Count);
            Assert.Equal("first", model.Labels[0]);
        }

        [Fact]
        public void Index_CheckNullResult()
        {
            // Arrange
            _measurementServiceMock.Setup(m => m.FindMeasurementSet("Батарея разряжается"));

            // Act
            var result = _measurementController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
            _measurementServiceMock.Verify(m => m.FindMeasurementSet("Батарея разряжается"), Times.Once);
            _chartCreatorMock.Verify(m => m.GetLineChartData(It.IsAny<List<MeasurementViewModel>>()), Times.Once);
            _measurementServiceMock.Verify(m => m.GetMeasurement(It.IsAny<int>()), Times.Never);
        }

        #region Private Methods
        private ChartJsData CreateChartData()
        {
            return new ChartJsData()
            {
                Labels = new List<string>() { "first", "second" },
                Datasets = new List<ChartDataset>()
                    {
                        new ChartDataset(),
                        new ChartDataset(),
                        new ChartDataset(),
                    }
            };
        }
        #endregion
    }
}
