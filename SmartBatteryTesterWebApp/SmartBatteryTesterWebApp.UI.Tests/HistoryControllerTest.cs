using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class HistoryControllerTest
    {
        Mock<IMeasurementOutputService> _measurementServiceMock;
        Mock<IMeasurementChartDataCreator> _chartCreatorMock;
        HistoryController _historyController;
        List<MeasurementSetDTO> _measurementSetDTOList;

        public HistoryControllerTest()
        {
            _measurementServiceMock = new Mock<IMeasurementOutputService>();
            _chartCreatorMock = new Mock<IMeasurementChartDataCreator>();

            _historyController = new HistoryController(_measurementServiceMock.Object, _chartCreatorMock.Object);

            _measurementSetDTOList = new List<MeasurementSetDTO>()
            {
                new MeasurementSetDTO() { Id = 1, MeasurementName = "The first test" },
                new MeasurementSetDTO() { Id = 2, MeasurementName = "The second test" }
            };
            _measurementServiceMock.Setup(m => m.GetMeasurementSetAsync()).ReturnsAsync(_measurementSetDTOList);
        }

        [Fact]
        public async void IndexAsync_CheckMethodsCollingWhenParameterIsZero()
        {
            // Arrange

            // Act
            await _historyController.IndexAsync(0);

            // Assert
            _measurementServiceMock.Verify(m => m.GetMeasurementSetAsync(), Times.Once);
            _measurementServiceMock.Verify(m => m.GetMeasurementAsync(It.IsAny<int>()), Times.Never);
            _chartCreatorMock.Verify(m => m.GetLineChartData(It.IsAny<List<MeasurementViewModel>>()), Times.Never);
        }

        [Fact]
        public async void IndexAsync_CheckMethodsCollingWhenParameterIsNotZero()
        {
            // Arrange
            int measurementSetNumber = 1;

            // Act
            await _historyController.IndexAsync(1);

            // Assert
            _measurementServiceMock.Verify(m => m.GetMeasurementSetAsync(), Times.Once);
            _measurementServiceMock.Verify(m => m.GetMeasurementAsync(measurementSetNumber), Times.Once);
            _chartCreatorMock.Verify(m => m.GetLineChartData(It.IsAny<List<MeasurementViewModel>>()), Times.Once);
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 3)]
        public async void IndexAsync_CheckViewBagItemsCount(int methodParameter, int viewBagItemsCount)
        {
            // Arrange

            // Act
            var result = await _historyController.IndexAsync(methodParameter) as ViewResult;

            // Assert
            Assert.Equal(viewBagItemsCount, result.ViewData.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async void IndexAsync_CheckReturnedValueWhenParameterIsZero(int index)
        {
            // Arrange

            // Act
            var result = await _historyController.IndexAsync(0);
            var measurementSetViewModelList = GetResultsFromMeasurementSetListViewBag(result);
            var measurementSetViewModel = GetResultsFromMeasurementResultsViewBag(result);

            // Assert
            Assert.Equal(_measurementSetDTOList[index].Id, measurementSetViewModelList[index].Id);
            Assert.Equal(_measurementSetDTOList[index].MeasurementName, measurementSetViewModelList[index].MeasurementName);

            Assert.Null(measurementSetViewModel);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        public async void IndexAsync_CheckMeasurementResultsViewBag(int index, int measurementSetNumber)
        {
            // Arrange

            // Act
            var result = await _historyController.IndexAsync(measurementSetNumber);
            var measurementSetViewModel = GetResultsFromMeasurementResultsViewBag(result);

            // Assert
            Assert.Equal(_measurementSetDTOList[index].Id, measurementSetViewModel.Id);
            Assert.Equal(_measurementSetDTOList[index].MeasurementName, measurementSetViewModel.MeasurementName);
        }

        [Fact]
        public async void IndexAsync_CheckChartDataViewBag()
        {
            // Arrange
            var chartData = new ChartJsData()
            {
                Labels = new List<string>() { "first", "second" },
                Datasets = new List<ChartDataset>()
                        {
                            new ChartDataset() { Label = "label 1" },
                            new ChartDataset() { Label = "label 2" },
                            new ChartDataset() { Label = "label 3" },
                        }
            };

            _chartCreatorMock.Setup(m => m.GetLineChartData(It.IsAny<List<MeasurementViewModel>>()))
                    .Returns(chartData);

            // Act
            var result = await _historyController.IndexAsync(1);
            var resultChartData = GetResultsFromChartDataViewBag(result);

            // Assert
            Assert.Equal(chartData.Labels.Count, resultChartData.Labels.Count);
            Assert.Equal(chartData.Datasets.Count, resultChartData.Datasets.Count);
            Assert.Equal("first", resultChartData.Labels[0]);
            Assert.Equal("label 2", resultChartData.Datasets[1].Label);
        }

        #region Private Methods
        private List<MeasurementSetViewModel> GetResultsFromMeasurementSetListViewBag(IActionResult result)
        {
            List<MeasurementSetViewModel> measurementSetViewModelList = new List<MeasurementSetViewModel>();

            var viewDataValues = ((ViewResult)result).ViewData.Values.GetEnumerator();
            viewDataValues.MoveNext();
            
            var currentSelectList = ((SelectList)viewDataValues.Current).Items.GetEnumerator();
                
            while (currentSelectList.MoveNext())
            {
                measurementSetViewModelList.Add((MeasurementSetViewModel)currentSelectList.Current);
            }

            return measurementSetViewModelList;
        }

        private MeasurementSetViewModel GetResultsFromMeasurementResultsViewBag(IActionResult result)
        {
            var viewDataValues = ((ViewResult)result).ViewData.Values.GetEnumerator();
            viewDataValues.MoveNext();
            viewDataValues.MoveNext();

            var resultMeasurementSet = ((MeasurementSetViewModel)viewDataValues.Current);

            return resultMeasurementSet;
        }

        private ChartJsData GetResultsFromChartDataViewBag(IActionResult result)
        {
            var resultChartData = new ChartJsData();

            var viewDataValues = ((ViewResult)result).ViewData.Values.GetEnumerator();
            viewDataValues.MoveNext();
            viewDataValues.MoveNext();
            viewDataValues.MoveNext();

            resultChartData.Labels = ((ChartJsData)viewDataValues.Current).Labels;
            resultChartData.Datasets = ((ChartJsData)viewDataValues.Current).Datasets;

            return resultChartData;
        }
        #endregion
    }
}