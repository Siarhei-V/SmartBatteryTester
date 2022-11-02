using Moq;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.BL.Tests
{
    public class DischargerTest
    {
        Mock<IValuesSaver> _valueSaver;
        Mock<IInfoSaver> _infoSaver;
        Mock<ISwitchable> _switchable;

        public DischargerTest()
        {
            _valueSaver = new Mock<IValuesSaver>();
            _switchable = new Mock<ISwitchable>();
            _infoSaver = new Mock<IInfoSaver>();
        }

        [Fact]
        public void Start_CheckSaveAndTurnOnMethodsColling()
        {
            // Arrange
            IDischarger discharger = new Discharger(_valueSaver.Object, _infoSaver.Object, _switchable.Object);
            discharger.CapacityCalculatorFactory = new ResultsCalculatorFactory();     // TODO: mock this

            // Act
            discharger.Start(1, 1, 1);

            // Assert
            _valueSaver.Verify(m => m.Save(It.IsAny<DischargerValuesDto>()), Times.Once);
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
            IDischarger discharger = new Discharger(_valueSaver.Object, _infoSaver.Object, _switchable.Object);
            discharger.CapacityCalculatorFactory = new ResultsCalculatorFactory();     // TODO: mock this

            // Act
            discharger.Start(1, 12, valuesChangeDiscreteness);
            discharger.Discharge(voltage, current);

            // Assert
            if (Math.Abs(12 - voltage) >= valuesChangeDiscreteness || 
                Math.Abs(0 - current) >= valuesChangeDiscreteness)
            {
                _valueSaver.Verify(m => m.Save(It.IsAny<DischargerValuesDto>()), Times.Exactly(2));
            }
            else
            {
                _valueSaver.Verify(m => m.Save(It.IsAny<DischargerValuesDto>()), Times.Once);
            }
        }

        [Fact]
        public void Discharge_CheckPrevValuesSetting()
        {
            // Arrange
            IDischarger discharger = new Discharger(_valueSaver.Object, _infoSaver.Object, _switchable.Object);
            discharger.CapacityCalculatorFactory = new ResultsCalculatorFactory();     // TODO: mock this

            List<decimal> voltageValues = new List<decimal>() { 11, 10, 9.9m};
            List<decimal> currentValues = new List<decimal>() { 1, 1, 1 };

            decimal valuesChangeDiscreteness = 1;

            // Act
            discharger.Start(1, 12, valuesChangeDiscreteness);

            // Assert
            discharger.Discharge(voltageValues[0], currentValues[0]);
            _valueSaver.Verify(m => m.Save(It.IsAny<DischargerValuesDto>()), Times.Exactly(2));

            discharger.Discharge(voltageValues[1], currentValues[1]);
            _valueSaver.Verify(m => m.Save(It.IsAny<DischargerValuesDto>()), Times.Exactly(3));

            discharger.Discharge(voltageValues[2], currentValues[2]);
            _valueSaver.Verify(m => m.Save(It.IsAny<DischargerValuesDto>()), Times.Exactly(3));
        }

        [Fact]
        public void Discharge_ChekDischargingFinish()
        {
            // Arrange
            IDischarger discharger = new Discharger(_valueSaver.Object, _infoSaver.Object, _switchable.Object);
            discharger.CapacityCalculatorFactory = new ResultsCalculatorFactory();     // TODO: mock this

            // Act
            discharger.Start(10.5m, 12, 1);
            discharger.Discharge(10, 1);

            // Assert
            _valueSaver.Verify(m => m.Save(It.IsAny<DischargerValuesDto>()), Times.Exactly(2));
            _infoSaver.Verify(m => m.Save(It.IsAny<DischargerInfoDto>()), Times.Once);
            _switchable.Verify(m => m.TurnOff(), Times.Once);
        }
    }
}