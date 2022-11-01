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

            _switchable.Setup(m => m.TurnOn());
        }

        [Fact]
        public void Start_CheckSaveAndTurnOnMethodsColling()
        {
            // Arrange
            Discharger _discharger = new Discharger(_resultSaver.Object, _switchable.Object);

            // Act
            _discharger.Start(1, 1, 1);

            // Assert
            _resultSaver.Verify(m => m.Save(), Times.Once);
            _switchable.Verify(m => m.TurnOn(), Times.Once);
        }

        [Theory]
        [InlineData(11,     1,      1)]
        [InlineData(11,     0,      1)]
        [InlineData(12,     1,      1)]
        [InlineData(11.8,   0,      1)]
        [InlineData(12,     0.2,    1)]
        public void Discharge_CheckSaveMethodColling(decimal voltage, decimal current, decimal valuesChangeDiscreteness)
        {
            // Arrange
            Discharger _discharger = new Discharger(_resultSaver.Object, _switchable.Object);

            // Act
            _discharger.Start(1, 12, valuesChangeDiscreteness);
            _discharger.Discharge(voltage, current);

            // Assert
            if (Math.Abs(12 - voltage) >= valuesChangeDiscreteness || 
                Math.Abs(0 - current) >= valuesChangeDiscreteness)
            {
                _resultSaver.Verify(m => m.Save(), Times.Exactly(2));
            }
            else
            {
                _resultSaver.Verify(m => m.Save(), Times.Once);
            }
        }

        [Fact]
        public void Discharge_CheckPrevValuesSetting()
        {
            // Arrange
            Discharger _discharger = new Discharger(_resultSaver.Object, _switchable.Object);

            List<decimal> voltageValues = new List<decimal>() { 11, 10, 9.9m};
            List<decimal> currentValues = new List<decimal>() { 1, 1, 1 };

            decimal valuesChangeDiscreteness = 1;

            // Act
            _discharger.Start(1, 12, valuesChangeDiscreteness);

            // Assert
            _discharger.Discharge(voltageValues[0], currentValues[0]);
            _resultSaver.Verify(m => m.Save(), Times.Exactly(2));

            _discharger.Discharge(voltageValues[1], currentValues[1]);
            _resultSaver.Verify(m => m.Save(), Times.Exactly(3));

            _discharger.Discharge(voltageValues[2], currentValues[2]);
            _resultSaver.Verify(m => m.Save(), Times.Exactly(3));
        }

        [Fact]
        public void Discharge_ChekDischargingFinish()
        {
            // Arrange
            Discharger _discharger = new Discharger(_resultSaver.Object, _switchable.Object);

            // Act
            _discharger.Start(10.5m, 12, 1);
            _discharger.Discharge(10, 1);

            // Assert
            _resultSaver.Verify(m => m.Save(), Times.Exactly(2));
            _switchable.Verify(m => m.TurnOff(), Times.Once);
        }
    }
}