using SmartBatteryTesterDesktopApp.USART.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.USART.Tests
{
    public class EventsHandlerTest
    {
        public event EventHandler<EventArgs>? TestEventsHandler;

        UsartEventArgs _testEventArgs;
        bool _isEventTriggered = false;
        Dictionary<string, string> _params;
        string _firstParameter;
        string _secondParameter;

        public EventsHandlerTest()
        {
            _testEventArgs = new UsartEventArgs();

            TestEventsHandler += (sender, args) =>
            {
                _isEventTriggered = true;
                _firstParameter = ((UsartEventArgs)args).LowerVoltageThreshol;
                _secondParameter = ((UsartEventArgs)args).ValuesChangeDiscretennes;
            };
        }

        //[Fact]
        //public void InvokeEventHandlerWithArgs_CheckInvokeWithArgs()
        //{
        //    // Arrange
        //    IEventsHandler eventsHandler = new EventsHandler();
        //    _params = new Dictionary<string, string>()
        //    {
        //        ["LowDischargeVoltage"] = "The first parameter",
        //        ["ValuesChangeDiscreteness"] = "The second parameter"
        //    };

        //    // Act
        //    eventsHandler.InvokeEventWithArgs(this, _testEventArgs, _params, TestEventsHandler);

        //    // Assert
        //    Assert.True(_isEventTriggered);
        //    Assert.Equal("The first parameter", _firstParameter);
        //    Assert.Equal("The second parameter", _secondParameter);
        //}
    }
}