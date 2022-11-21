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
            
            _measurementServiceMock.Setup(m => m.FindMeasurementSetAsync("Батарея разряжается")).ReturnsAsync(new MeasurementSetDTO());

            _measurementController = new MeasurementController(_measurementServiceMock.Object, _chartCreatorMock.Object);
        }

        [Fact]
        public void IndexAsync_CheckMethodsColling()
        {
            // Arrange

            // Act
            _ = _measurementController.IndexAsync();

            // Assert
            _measurementServiceMock.Verify(m => m.FindMeasurementSetAsync(It.IsAny<string>()), Times.Once);
            _measurementServiceMock.Verify(m => m.GetMeasurementAsync(It.IsAny<int>()), Times.Once);
            _chartCreatorMock.Verify(m => m.GetLineChartData(It.IsAny<List<MeasurementViewModel>>()), Times.Once);
        }

        [Fact]
        public async void IndexAsync_CheckReturnedValue()
        {
            // Arrange
            _chartCreatorMock.Setup(m => m.GetLineChartData(It.IsAny<List<MeasurementViewModel>>()))
                .Returns(CreateChartData());

            // Act
            var result = await _measurementController.IndexAsync();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ChartJsData>(viewResult.Model);
            Assert.NotNull(model);
            Assert.Equal(2, model.Labels.Count);
            Assert.Equal(3, model.Datasets.Count);
        }

        [Fact]
        public async void IndexAsync_CheckNullResult()
        {
            // Arrange
            _measurementServiceMock.Setup(m => m.FindMeasurementSetAsync("Батарея разряжается"));

            // Act
            var result = await _measurementController.IndexAsync();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
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
