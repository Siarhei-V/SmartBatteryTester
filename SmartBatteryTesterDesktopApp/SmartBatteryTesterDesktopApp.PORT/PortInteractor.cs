using SmartBatteryTesterDesktopApp.BL;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortInteractor : IPortInteractor
    {
        static PortInteractor? _portDataHandler;
        IDischargerFacade? _dischargerFacade;
        IDischargerController _dischargerController;
        IPortDataTransmitter _portTransmitter;
        INotifyDataChanged _notifyDataChanged;
        bool _isDischargingStarted;

        private PortInteractor()
        {
            _dischargerFacade = new DischargerFacade();
            _dischargerController = new PortController();
            _dischargerController.ControllerNotify += (sender, args) => 
            {
                _portTransmitter?.StopDataTransfer();
                _isDischargingStarted = false;
                _notifyDataChanged.OnDataChanged("", "", _isDischargingStarted);
            };
        }

        public static PortInteractor Instance
        {
            get => _portDataHandler ?? (_portDataHandler = new PortInteractor());
        }

        public IPortDataTransmitter PortTransmitter
        {
            set => _portTransmitter = value;
        }

        public INotifyDataChanged NotifyDataChanged
        {
            set => _notifyDataChanged = value;
        }

        public void InitializeDataHandler(IDischargerDataSaver dischargerDataSaver) // TODO: this is a temporary parameter
        {
            _dischargerFacade.InitializeDischarger(dischargerDataSaver, _dischargerController);
        }

        public void StartDischarging(Dictionary<string, string> portConnectionParameters)
        {
            _isDischargingStarted = true;
            _portTransmitter.Parameters = portConnectionParameters;
            _portTransmitter.StartDataTransfer();
        }

        public void StopDischarging()
        {
            _portTransmitter.StopDataTransfer();
            _isDischargingStarted = false;
            _notifyDataChanged.OnDataChanged("", "", _isDischargingStarted);
        }

        public void HandleStartValues(string lowerDischargerVoltage, string startVoltage, string valuesChangeDiscreteness)
        {
            _notifyDataChanged.OnDataChanged(startVoltage, "", _isDischargingStarted);

            _dischargerFacade.StartDischarging(Convert.ToDecimal(lowerDischargerVoltage), 
                Convert.ToDecimal(startVoltage), 
                Convert.ToDecimal(valuesChangeDiscreteness));
        }

        public void HandleIntermediateValues(string voltage, string current, DateTime dateTime)
        {
            _notifyDataChanged.OnDataChanged(voltage, current, _isDischargingStarted);
            _dischargerFacade.Discharge(Convert.ToDecimal(voltage),  Convert.ToDecimal(current));
        }
    }
}
