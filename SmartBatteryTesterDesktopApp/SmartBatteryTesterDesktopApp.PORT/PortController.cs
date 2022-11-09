using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    internal class PortController : IDischargerController
    {
        internal event EventHandler? ControllerNotify;

        object _objectLock = new object();

        event EventHandler IDischargerController.ControllerNotify
        {
            add
            {
                lock (_objectLock)
                {
                    ControllerNotify += value;
                }
            }

            remove
            {
                lock (_objectLock)
                {
                    ControllerNotify -= value;
                }
            }
        }

        public void AutoStopDischarging()
        {
            ControllerNotify?.Invoke(this, new EventArgs());
        }
    }
}
