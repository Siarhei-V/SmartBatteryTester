using Moq;
using Xunit;

namespace SmartBatteryTesterDesktopApp.BL.Tests
{
    public class DischargerTest
    {
        Mock<IResultSaver> _resultSaver;
        Mock<ISwitchable> _switchable;

        public DischargerTest()
        {
            _resultSaver = new Mock<IResultSaver>();
            _switchable = new Mock<ISwitchable>();

            _resultSaver.Setup(m => m.Save()).Returns("ok");
            _switchable.Setup(m => m.TurnOn());
        }

        [Fact]
        public void Test_CheckParamsGetting()
        {
            // Arrange
            Discharger _discharger = new Discharger(_resultSaver.Object, _switchable.Object);

            // Act
            _discharger.Start(1, 1);

            // Assert
            _resultSaver.Verify(m => m.Save(), Times.Once);
            _switchable.Verify(m => m.TurnOn(), Times.Once);
        }
    }
}