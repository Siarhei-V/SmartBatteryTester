using SmartBatteryTesterDesktopApp.BL.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.BL.Tests
{
    public class ResultsCalculatorTest
    {
        IResultsCalculator _resultsCalculator;
        DischargerModel _dataModel;

        public ResultsCalculatorTest()
        {
            _dataModel = new DischargerModel();
            _resultsCalculator = new ResultsCalculator(_dataModel);
        }

        [Fact]
        public void GetCapacity_CapacityCheck()
        {
            // Arrange
            DateTime startDateTime = new DateTime(2022, 11, 1, 10, 0, 0);
            DateTime endDateTime = new DateTime(2022, 11, 2, 6, 0, 0);

            _resultsCalculator.DischargingStartDateTime = startDateTime;
            _resultsCalculator.DischargingEndDateTime = endDateTime;
            var timeDifference = endDateTime - startDateTime;

            _resultsCalculator.DischargingCurrent = 3.5m;

            // Act
            _resultsCalculator.CalculateResults();

            // Assert
            Assert.Equal(70, _dataModel.ResultCapacity);
            Assert.Equal(20, timeDifference.TotalHours);
        }
    }
}
