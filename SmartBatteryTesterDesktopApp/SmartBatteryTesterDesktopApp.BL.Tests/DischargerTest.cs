using Moq;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.BL.Tests
{
    public class DischargerTest
    {
        Mock<IDischargerDataSaver> _dataSaver;
        IResultsCalculator _resultsCalculator;
        Mock<DischargerDto> _dischargerDto;
        Mock<IDischargerController> _controller;

        IDischarger _discharger;

        public DischargerTest()
        {
            _dataSaver = new Mock<IDischargerDataSaver>();
            _dischargerDto = new Mock<DischargerDto>();
            _resultsCalculator = new ResultsCalculator(_dischargerDto.Object);  // TODO: Mock this
            _controller = new Mock<IDischargerController>();

            _discharger = new Discharger(_dataSaver.Object, _resultsCalculator, _dischargerDto.Object, _controller.Object);
        }

        [Theory]
        [InlineData(11, 1, 1)]
        [InlineData(11, 0, 1)]
        [InlineData(12, 1, 1)]
        [InlineData(11.8, 0, 1)]
        [InlineData(12, 0.2, 1)]
        public void Discharge_CheckSaveMethodColling(decimal voltage, decimal current, decimal valuesChangeDiscreteness)
        {
           // Arrange

            // Act
            _discharger.Start(1, 12, valuesChangeDiscreteness);
            _discharger.Discharge(voltage, current);

            // Assert
            if (Math.Abs(12 - voltage) >= valuesChangeDiscreteness ||
                Math.Abs(0 - current) >= valuesChangeDiscreteness)
            {
                _dataSaver.Verify(m => m.Save(It.IsAny<DischargerDto>()), Times.Exactly(2));
            }
            else
            {
                _dataSaver.Verify(m => m.Save(It.IsAny<DischargerDto>()), Times.Once);
            }
        }

        [Fact]
        public void Discharge_CheckPrevValuesSetting()
        {
            // Arrange

            List<decimal> voltageValues = new List<decimal>() { 11, 10, 9.9m };
            List<decimal> currentValues = new List<decimal>() { 1, 1, 1 };

            decimal valuesChangeDiscreteness = 1;

            // Act
            _discharger.Start(1, 12, valuesChangeDiscreteness);

            // Assert
            _discharger.Discharge(voltageValues[0], currentValues[0]);
            _dataSaver.Verify(m => m.Save(It.IsAny<DischargerDto>()), Times.Exactly(2));

            _discharger.Discharge(voltageValues[1], currentValues[1]);
            _dataSaver.Verify(m => m.Save(It.IsAny<DischargerDto>()), Times.Exactly(3));

            _discharger.Discharge(voltageValues[2], currentValues[2]);
            _dataSaver.Verify(m => m.Save(It.IsAny<DischargerDto>()), Times.Exactly(3));
        }

        [Fact]
        public void Discharge_ChekDischargingFinish()
        {
            // Arrange

            // Act
            _discharger.Start(10.5m, 12, 1);
            _discharger.Discharge(10, 1);

            // Assert
            _dataSaver.Verify(m => m.Save(It.IsAny<DischargerDto>()), Times.Exactly(2));
            _controller.Verify(m => m.StopDischarging(), Times.Exactly(1));
        }
    }
}