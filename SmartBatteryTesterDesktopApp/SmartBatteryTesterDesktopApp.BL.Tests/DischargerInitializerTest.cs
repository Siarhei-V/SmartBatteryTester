using Moq;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.BL.Tests
{
    public class DischargerInitializerTest
    {
        IDischargerInitializer _dischargerInitializer;
        Mock<IValuesSaver> _valuesSaver;
        Mock<IInfoSaver> _infoSaver;
        Mock<ISwitchable> _switch;

        public DischargerInitializerTest()
        {
            _valuesSaver = new Mock<IValuesSaver>();
            _switch = new Mock<ISwitchable>();
            _infoSaver = new Mock<IInfoSaver>();

            _dischargerInitializer = new DischargerInitializer(_valuesSaver.Object, _infoSaver.Object, _switch.Object);
        }

        [Fact]
        public void DischargerImplementation_CheckReturnedType()
        {
            // Arrange

            // Act
            var result = _dischargerInitializer.DischargerImplementation;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Discharger>(result);
        }
    }
}
