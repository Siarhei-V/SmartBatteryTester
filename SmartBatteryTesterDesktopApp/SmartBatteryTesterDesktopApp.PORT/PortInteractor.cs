using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortInteractor : IUiInteractorInputPort, IUsartInteractorInputPort, IControllerInstanceSetter, IDataSenderInstanceSetter
    {
        static PortInteractor? _portInteractor;
        IPortController? _portController;
        IDataGetter _dataGetter;

        private PortInteractor() { }

        public static PortInteractor Instance
        {
            get => _portInteractor ?? (_portInteractor = new PortInteractor());
        }

        public IPortController PortController
        {
            set => _portController = value;
        }
        public IDataGetter DataSender 
        { 
            set => _dataGetter = value; 
        }

        public void SendUsartData(string data)
        {
            _dataGetter.GetData(data);
        }

        public void StartDischarging(Dictionary<string, string> portConnectionParameters)
        {
            _portController.StartDischarging(portConnectionParameters);
        }

        public void StopDischarging()
        {
            _portController.StopDischarging();
        }
    }
}
