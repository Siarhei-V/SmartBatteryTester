using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortParametersSetter : IPortParametersSetter
    {
        private static PortParametersSetter? _portParametersSetter;
        object? _parameters;

        private PortParametersSetter() { }

        public static PortParametersSetter Instance
        {
            get => _portParametersSetter ?? (_portParametersSetter = new PortParametersSetter());
        }

        public void SetParameters(object parameters)
        {
            _parameters = parameters;
        }

        public object GetParameters()
        {
            return _parameters;
        }
    }
}
