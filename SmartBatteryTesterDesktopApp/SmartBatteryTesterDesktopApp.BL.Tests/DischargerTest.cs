using Moq;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.BL.Tests
{
    public class DischargerTest
    {
        Mock<IResultsCalculator> _resultsCalculatorMock;
        DischargerDto _dischargerDto;

        IDischarger _discharger;

        public DischargerTest()
        {
            _dischargerDto = new DischargerDto();
            _resultsCalculatorMock = new Mock<IResultsCalculator>();

            _discharger = new Discharger(_dischargerDto);
            _discharger.ResultsCalculator = _resultsCalculatorMock.Object;
        }

        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(1.1)]
        public void SetDischargingParams_CheckDischargingCurrentSaving(decimal dischargingCurrent)
        {
            // Arrange

            // Act
            _discharger.SetDischargingParams(0, 0, dischargingCurrent);

            // Assert
            Assert.Equal(dischargingCurrent, _dischargerDto.Current);
        }

        [Fact]
        public void Discharge_CheckFirstDataHandling()
        {
            // Arrange
            _dischargerDto.IsDischargingCompleted = true;

            // Act
            _discharger.Discharge(1, 1, DateTime.Now);

            // Assert
            Assert.False(_dischargerDto.IsDischargingCompleted);
        }

        [Fact]
        public void Discharge_CheckDataSavingToModel()
        {
            // Arrange
            decimal firstVoltage = 2.5m;
            decimal secondVoltage = 500;
            var firstDateTime = new DateTime(1, 2, 3);
            var secondDateTime = new DateTime(3, 4, 5);

            // Act
            _discharger.Discharge(firstVoltage, 0, firstDateTime);

            // Assert
            Assert.Equal(firstVoltage, _dischargerDto.Voltage);
            Assert.Equal(firstDateTime, _dischargerDto.CurrentDateTime);

            _discharger.Discharge(secondVoltage, 0, secondDateTime);
            Assert.Equal(secondVoltage, _dischargerDto.Voltage);
            Assert.Equal(secondDateTime, _dischargerDto.CurrentDateTime);
        }

        [Theory]
        [InlineData(1, 0, 3, 1, 1)]
        [InlineData(10, 0, 1, 5, 0)]
        [InlineData(1, 2, 1, 1, 1)]
        public void Discharge_CheckNewDataReceiving(decimal voltage, decimal current, decimal valuesChangeDiscreteness,
            decimal prevVoltage, decimal prevCurrent)
        {
            // Assert
            _discharger.SetDischargingParams(0, valuesChangeDiscreteness, 0);

            // Act
            _discharger.Discharge(voltage, current, DateTime.Now);

            // Arrange
            if (Math.Abs(prevVoltage - voltage) >= valuesChangeDiscreteness ||
                    Math.Abs(prevCurrent - current) >= valuesChangeDiscreteness)
            {
                Assert.True(_discharger.IsNewDataReceived);
            }
            else
            {
                Assert.False(_discharger.IsNewDataReceived);
            }
        }

        [Fact]
        public void Discharge_CheckDischargingFinish()
        {
            // Arrange
            var date = new DateTime(11, 12, 13);
            _discharger.SetDischargingParams(3, 2, 4);

            // Act
            _discharger.Discharge(1, 1.1m, date);
         
            // Assert
            _resultsCalculatorMock.Verify(m => m.CalculateResults(), Times.Once());
            Assert.True(_dischargerDto.IsDischargingCompleted);
            Assert.True(_discharger.IsNewDataReceived);
            Assert.Equal(1, _dischargerDto.Voltage);
            Assert.Equal(date, _dischargerDto.CurrentDateTime);

            _discharger.Discharge(500, 0, date);
            Assert.False(_dischargerDto.IsDischargingCompleted);
        }

        [Fact]
        public void GetDischargingData_CheckReturnedData()
        {
            // Arrange
            var dateTime = new DateTime(10, 10, 10);
            _discharger.Discharge(1, 2, dateTime);

            // Act
            var result = _discharger.GetDischargingData();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DischargerDto>(result);
            Assert.Equal(1, result.Voltage);
            Assert.Equal(dateTime, result.CurrentDateTime);
        }
    }
}