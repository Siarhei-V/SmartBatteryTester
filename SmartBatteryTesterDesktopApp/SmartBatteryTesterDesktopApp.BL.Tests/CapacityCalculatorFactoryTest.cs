using SmartBatteryTesterDesktopApp.BL.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.BL.Tests
{
    public class CapacityCalculatorFactoryTest
    {
        ICapacityCalculatorFactory _capacityCalculatorFactory;

        public CapacityCalculatorFactoryTest()
        {
            _capacityCalculatorFactory = new CapacityCalculatorFactory();
            
        }

        [Fact]
        public void MakeCapacityCaclulator_CheckReturnedType()
        {
            // Arrange

            // Act
            var result = _capacityCalculatorFactory.MakeCapacityCalculator();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("CapacityCalculator", result.GetType().Name);
        }
    }
}
