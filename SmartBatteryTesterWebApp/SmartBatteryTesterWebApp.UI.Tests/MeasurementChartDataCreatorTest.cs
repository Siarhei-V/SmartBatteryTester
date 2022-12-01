using SmartBatteryTesterWebApp.UI.Infrastructure;
using SmartBatteryTesterWebApp.UI.Models;
using SmartBatteryTesterWebApp.UI.Models.Chart;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartBatteryTesterWebApp.UI.Tests
{
    public class MeasurementChartDataCreatorTest
    {
        IMeasurementChartDataCreator _dataCreator;

        public MeasurementChartDataCreatorTest()
        {
            _dataCreator = new MeasurementChartDataCreator();
        }

        [Fact]
        public void GetLineChartData_CheckReturnedvalues()
        {
            // Arrange
            var measurementViewModelList = new List<MeasurementViewModel>()
            {
                new MeasurementViewModel() { Voltage = 10, Current = 5, 
                    MeasurementDateTime = new DateTime(1, 1, 1, 2, 2, 2).ToString()},
                new MeasurementViewModel() { Voltage = 20, Current = 15, 
                    MeasurementDateTime = new DateTime(3, 3, 3, 4, 4, 4).ToString()},
                new MeasurementViewModel() { Voltage = 30, Current = 25, 
                    MeasurementDateTime = new DateTime(5, 5, 5, 6, 6, 6).ToString()},
            };

            // Act
            var methodResult = _dataCreator.GetLineChartData(measurementViewModelList);

            // Assert
            Assert.NotNull(methodResult);
            var typeResult = Assert.IsType<ChartJsData>(methodResult);
            var result = Assert.IsAssignableFrom<ChartJsData>(typeResult);
            Assert.Equal(3, result.Labels.Count);
            Assert.Equal(5, result.Datasets[1].Data[0]);
            Assert.Equal(30, result.Datasets[0].Data[2]);
            Assert.Equal(new DateTime(5, 5, 5, 6, 6, 6).ToString(), result.Labels[2].ToString());
        }
    }
}
