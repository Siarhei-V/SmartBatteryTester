using SmartBatteryTesterDesktopApp.DataAccess.Interfaces;
using SmartBatteryTesterDesktopApp.PORT;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.DataAccess
{
    public class DataAccessInitializer
    {
        IDataSaver _dataSaver;
        IDataSenderFactory _dataSenderFactory;
        IDaInteractorInputPort _inputPort;

        public DataAccessInitializer()
        {
            _dataSenderFactory = new DataSenderFactory();
            _dataSaver = new DataSaverFacade(_dataSenderFactory);

            _inputPort = PortInteractor.Instance;
            _inputPort.DataSaver = _dataSaver;
        }
    }
}
