using SmartBatteryTesterDesktopApp.BL.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.BL.Tests
{
    public class ResultsCalculatorFactoryTest
    {
        IResultsCalculatorFactory _resultsCalculatorFactory;

        public ResultsCalculatorFactoryTest()
        {
            _resultsCalculatorFactory = new ResultsCalculatorFactory();
        }

        [Fact]
        public void MakeResultsCaclulator_CheckReturnedType()
        {
            // Arrange

            // Act
            var result = _resultsCalculatorFactory.MakeResultsCalculator();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ResultsCalculator", result.GetType().Name);
        }
    }
}
