using SmartBatteryTesterDesktopApp.BL.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.BL.Tests
{
    public class CapacityCalculatorFactoryTest
    {
        IResultsCalculatorFactory _capacityCalculatorFactory;

        public CapacityCalculatorFactoryTest()
        {
            _capacityCalculatorFactory = new ResultsCalculatorFactory();
        }

        [Fact]
        public void MakeCapacityCaclulator_CheckReturnedType()
        {
            // Arrange

            // Act
            var result = _capacityCalculatorFactory.MakeResultsCalculator();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ResultsCalculator", result.GetType().Name);
        }
    }
}
