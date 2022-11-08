//using SmartBatteryTesterDesktopApp.USART.Interfaces;

//namespace SmartBatteryTesterDesktopApp.USART
//{
//    internal class EventsHandler : IEventsHandler
//    {
//        void IEventsHandler.InvokeEventWithArgs(object sender, UsartEventArgs usartEventArgs, 
//            Dictionary<string, string> parameters, EventHandler<EventArgs>? dataReceivedNotify)
//        {
//            usartEventArgs.LowerVoltageThreshol = parameters["LowDischargeVoltage"];
//            usartEventArgs.ValuesChangeDiscretennes = parameters["ValuesChangeDiscreteness"];
//            dataReceivedNotify?.Invoke(sender, usartEventArgs);
//        }
//    }
//}
