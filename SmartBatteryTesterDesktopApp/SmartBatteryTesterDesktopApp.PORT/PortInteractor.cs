using SmartBatteryTesterDesktopApp.BL;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.DataSaver;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver;
using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortInteractor : IUiInteractorInputPort, IUsartInteractorInputPort
    {
        static PortInteractor? _portInteractor;
        IPortController? _portController;
        IDataGetter _dataGetter;
        IDischargerInitializer _dischargerInitializer;
        IDischarger _discharger;
        DischargerModel _dischargerModel;
        MeasurementModel _portDataModel;
        IDataSaverFacade _dataSaver;
        IDataSaverFactory _dataSaverFactory;


        private PortInteractor() 
        {
            _dischargerInitializer = new DischargerInitializer();
            _portDataModel = new MeasurementModel();
            _dataSaverFactory = new DataSaverFactory();
            _dataSaver = new DataSaverFacade(_dataSaverFactory);
            _discharger = _dischargerInitializer.InitializeDischarger();
        }

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

        public async void SendUsartData(string data)
        {
            try
            {
                _discharger.Discharge(Convert.ToDecimal(data), 0, DateTime.Now);
            }
            catch (Exception)
            {
            }

            if (_discharger.IsNewDataReceived)
            {
                _dischargerModel = _discharger.GetDischargingData();
                _portDataModel.Voltage = _dischargerModel.Voltage;
                _portDataModel.Current = _dischargerModel.Current;
                _portDataModel.MeasurementDateTime = _dischargerModel.CurrentDateTime.ToString();

                try
                {
                    await _dataSaver.SaveData(_portDataModel);
                }
                catch (Exception)
                {
                    throw;
                }


                if (_dischargerModel.IsDischargingCompleted)
                {
                    try
                    {
                        await _dataSaver.FinishTest();
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    _portController.StopDischarging();

                    _dataGetter.GetData("Порт закрыт");
                    return;
                }

            }
            _dataGetter.GetData(data);
        }

        public void SetDischargingParams(string lowerDischargeThreshold, string valuesChangeDiscreteness, string dischargingCurrent)
        {
            _discharger.SetDischargingParams(Convert.ToDecimal(lowerDischargeThreshold),
                Convert.ToDecimal(valuesChangeDiscreteness), 
                Convert.ToDecimal(dischargingCurrent));
        }

        public void StartDischarging(Dictionary<string, string> portConnectionParameters)
        {
            _portController.StartDischarging(portConnectionParameters);
        }

        public void StopDischarging()
        {
            _portController.StopDischarging();
        }

        public async void CreateNewTest(string testName)
        {
            try
            {
                _portDataModel.MeasurementSetId = await _dataSaver.CreateNewTest(testName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
