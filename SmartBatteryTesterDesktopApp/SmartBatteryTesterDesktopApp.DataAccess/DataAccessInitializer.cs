using SmartBatteryTesterDesktopApp.DataAccess.Interfaces;
using SmartBatteryTesterDesktopApp.DataAccess.Models;
using SmartBatteryTesterDesktopApp.PORT;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.DataAccess
{
    public class DataAccessInitializer
    {
        IDataSaver _dataSaver;
        IDataSenderFactory _dataSenderFactory;
        IDaInteractorInputPort _inputPort;
        TestDataModel _testModel;

        public DataAccessInitializer()
        {
            _dataSenderFactory = new DataSenderFactory();
            _testModel = new TestDataModel();
            _dataSaver = new DataSaverFacade(_dataSenderFactory, _testModel);

            _inputPort = PortInteractor.Instance;
            _inputPort.DataSaver = _dataSaver;
        }
    }
}
