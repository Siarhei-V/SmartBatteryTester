using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.UI.Controllers;
using SmartBatteryTesterWebApp.UI.Infrastructure;
using SmartBatteryTesterWebApp.UI.Models;
using SmartBatteryTesterWebApp.UI.Models.Chart;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartBatteryTesterWebApp.UI.Tests
{
    public class HistoryControllerTest
    {
        Mock<IMeasurementOutputService> _measurementServiceMock;
        Mock<IMeasurementChartDataCreator> _chartCreatorMock;
        HistoryController _historyController;

        public HistoryControllerTest()
        {
            _measurementServiceMock = new Mock<IMeasurementOutputService>();
            _chartCreatorMock = new Mock<IMeasurementChartDataCreator>();

            _historyController = new HistoryController(_measurementServiceMock.Object, _chartCreatorMock.Object);
        }

        [Fact]
        public async void IndexAsync_CheckMethodsColling()
        {
            // Arrange

            // Act
            await _historyController.IndexAsync(It.IsAny<int>());

            // Assert
            _measurementServiceMock.Verify(m => m.GetMeasurementSetAsync(), Times.Once);
            _measurementServiceMock.Verify(m => m.GetMeasurementAsync(It.IsAny<int>()), Times.Once);
            _chartCreatorMock.Verify(m => m.GetLineChartData(It.IsAny<List<MeasurementViewModel>>()), Times.Once);
        }

        [Fact]
        public async void IndexAsync_CheckReturnedValue()
        {
            // Arrange
            _chartCreatorMock.Setup(m => m.GetLineChartData(It.IsAny<List<MeasurementViewModel>>()))
                .Returns(new ChartJsData()
                {
                    Labels = new List<string>() { "first", "second" },
                    Datasets = new List<ChartDataset>()
                    {
                        new ChartDataset(),
                        new ChartDataset(),
                        new ChartDataset(),
                    }
                });

            // Act
            var result = await _historyController.IndexAsync(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ChartJsData>(viewResult.Model);
            Assert.NotNull(model);
            Assert.Equal(2, model.Labels.Count);
            Assert.Equal(3, model.Datasets.Count);
        }
    }
}