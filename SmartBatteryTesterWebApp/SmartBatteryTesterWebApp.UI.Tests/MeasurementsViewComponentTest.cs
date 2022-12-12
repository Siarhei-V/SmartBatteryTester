using Microsoft.AspNetCore.Mvc.ViewComponents;
using SmartBatteryTesterWebApp.UI.Models.Chart;
using SmartBatteryTesterWebApp.UI.ViewComponents;
using System.Collections.Generic;
using Xunit;

namespace SmartBatteryTesterWebApp.UI.Tests
{
    public class MeasurementsViewComponentTest
    {
        [Fact]
        public void Invoke_CheckReturnedValue()
        {
            // Arrange
            var component = new MeasurementsViewComponent();

            // Act
            var methodResult = component.Invoke(new ChartJsData()
            {
                Labels = new List<string>() { "first", "second" },
                Datasets = new List<ChartDataset>()
                    {
                        new ChartDataset(),
                        new ChartDataset(),
                        new ChartDataset(),
                    }
            });

            // Assert
            Assert.NotNull(methodResult);

            var typeResult = Assert.IsType<ViewViewComponentResult>(methodResult);
            var result = Assert.IsAssignableFrom<ViewViewComponentResult>(typeResult);
            Assert.Equal("MeasurementsChart", result.ViewName);

            var model = Assert.IsAssignableFrom<ChartJsData>(typeResult.ViewData.Model);
            Assert.Equal(2, model.Labels.Count);
            Assert.Equal(3, model.Datasets.Count);
        }
    }
}
