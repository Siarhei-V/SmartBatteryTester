namespace SmartBatteryTesterDesktopApp.PORT.Interfaces
{
    public interface IPortParametersSetter
    {
        void SetParameters(object parameters);
        object GetParameters();
    }
}