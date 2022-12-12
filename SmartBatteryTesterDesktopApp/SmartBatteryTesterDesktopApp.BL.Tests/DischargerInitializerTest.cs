using SmartBatteryTesterDesktopApp.BL.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.BL.Tests
{
    public class DischargerInitializerTest
    {
        [Fact]
        public void InitializeDischarger_CheckReturnedValueType()
        {
            // Arrange
            IDischargerInitializer dischargerInitializer = new DischargerInitializer();

            // Act
            var result = dischargerInitializer.InitializeDischarger();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Discharger>(result);
        }
    }
}
