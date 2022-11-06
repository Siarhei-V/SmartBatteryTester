using SmartBatteryTesterDesktopApp.BL;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortDataHandler : IPortDataHandler
    {
        static PortDataHandler? _portDataHandler;
        IDischargerFacade? _dischargerFacade;
        IDischargerController _dischargerController;
        IPortDataTransmitter _portTransmitter;

        private PortDataHandler()
        {
            _dischargerFacade = new DischargerFacade();
            _dischargerController = new PortController();
            _dischargerController.ControllerNotify += (sender, args) => _portTransmitter?.StopDataTransfer();
        }

        public static PortDataHandler Instance
        {
            get => _portDataHandler ?? (_portDataHandler = new PortDataHandler());
        }

        public IPortDataTransmitter PortTransmitter
        {
            set => _portTransmitter = value;
        }
        public void InitializeDataHandler(IDischargerDataSaver dischargerDataSaver) // TODO: this is a temporary parameter
        {
            _dischargerFacade.InitializeDischarger(dischargerDataSaver, _dischargerController);
        }

        public void StartDischarging(Dictionary<string, string> portConnectionParameters)
        {
            _portTransmitter.StartDataTransfer(portConnectionParameters);
        }

        public void HandleStartValues(decimal lowerDischargerVoltage, decimal startVoltage, decimal valuesChangeDiscreteness)
        {
            _dischargerFacade.StartDischarging(lowerDischargerVoltage, startVoltage, valuesChangeDiscreteness);
        }

        public void HandleIntermediateValues(decimal voltage, decimal current, DateTime dateTime)
        {
            _dischargerFacade.Discharge(voltage, current);
        }
    }
}
