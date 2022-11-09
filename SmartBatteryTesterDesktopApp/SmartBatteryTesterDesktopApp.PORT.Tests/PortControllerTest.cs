using SmartBatteryTesterDesktopApp.BL.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.PORT.Tests
{
    public class PortControllerTest
    {
        int _eventTriggeredCount = 0;

        [Fact]
        public void StopDischarging_TestEvent()
        {
            // Arrange
            IDischargerController dischargerController = new PortController();
            dischargerController.ControllerNotify += HandleControllerNotify;
            _eventTriggeredCount = 0;

            // Act
            dischargerController.AutoStopDischarging();

            // Assert
            Assert.Equal(1, _eventTriggeredCount);
        }

        [Fact]
        public void EventHandler_CheckSubscriptionAndUnsubscription()
        {
            // Arrange
            IDischargerController dischargerController = new PortController();
            _eventTriggeredCount = 0;

            // Act
            dischargerController.ControllerNotify += HandleControllerNotify;
            dischargerController.AutoStopDischarging();
            dischargerController.ControllerNotify -= HandleControllerNotify;
            dischargerController.AutoStopDischarging();

            // Assert
            Assert.Equal(1, _eventTriggeredCount);
        }

        private void HandleControllerNotify(object? sender, EventArgs e)
        {
            _eventTriggeredCount += 1;
        }
    }
}
