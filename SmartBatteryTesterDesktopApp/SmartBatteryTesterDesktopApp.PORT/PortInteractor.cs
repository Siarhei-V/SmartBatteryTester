using SmartBatteryTesterDesktopApp.BL;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortInteractor : IUiInteractorInputPort, IUsartInteractorInputPort, IDaInteractorInputPort
    {
        static PortInteractor? _portInteractor;
        IPortController? _portController;
        IDataGetter _dataGetter;
        IDischargerInitializer _dischargerInitializer;
        IDischarger _discharger;
        DischargerModel _dischargerModel;
        MeasurementModel _measurementModel;
        IDataSaver? _dataSaver;
        bool _isOnlineModeEnabled;
        bool _isDischargingStarted;
        bool _isNewTestCreated;

        private PortInteractor() 
        {
            _measurementModel = new MeasurementModel();
            _dischargerInitializer = new DischargerInitializer();           // TODO: inject somehow
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

        public IDataSaver DataSaver
        {
            set => _dataSaver = value;
        }

        public void SetDischargingParams(string lowerDischargeThreshold, string valuesChangeDiscreteness, string dischargingCurrent)
        {
            _discharger.SetDischargingParams(Convert.ToDecimal(lowerDischargeThreshold),
                Convert.ToDecimal(valuesChangeDiscreteness), 
                Convert.ToDecimal(dischargingCurrent));
        }

        public void ConnectToPort(Dictionary<string, string> portConnectionParameters)
        {
            _portController.OpenPort(portConnectionParameters);
            _measurementModel.Current = Convert.ToDecimal(portConnectionParameters["TempDischargingCurrent"]);  // TODO: temp
        }

        public void StartDischarging(bool isOnlineMode)
        {
            _isOnlineModeEnabled = isOnlineMode;
            _isDischargingStarted = true;
            _portController.StartDischarging();
        }

        public async void SendUsartData(string data)
        {
            _dischargerModel = _discharger.GetDischargingData();
            HandleDataFromUsart(data);

            if (!_discharger.IsNewDataReceived) return;

            if (_isOnlineModeEnabled)
                await SendDataToWeb();

            if (_dischargerModel.IsDischargingCompleted)
                await HandleDischargingFinish();
        }

        public void StopDischarging()
        {
            _portController.StopDischarging();

            if (!_isOnlineModeEnabled) 
                return;

            try
            {
                _dataSaver.FinishDataTransfer(new TimeSpan(), 0, "Разряд батареи прерван");
            }
            catch (Exception)
            {
                _dataGetter.GetWebStatus("Ошибка подключения к веб");
            }

            _dataGetter.GetWebStatus("Соединение с веб остановлено");
            _isNewTestCreated = false;
            _isOnlineModeEnabled = false;
            _discharger.SetDischargingParams(0, 0, 0);
            _isDischargingStarted = false;
        }

        public void DisconnectDevice()
        {
            _portController.ClosePort();
        }

        public void DisconnectWeb()
        {
            if (_isNewTestCreated)
            {
                _dataSaver.FinishDataTransfer(new TimeSpan(), 0, "Разряд батареи не был запущен");
            }

            _dataGetter.GetWebStatus("Соединение с веб остановлено");
        }

        public async void CreateNewTest(string testName)
        {
            _isNewTestCreated = true;

            try
            {
                _dataGetter.GetWebStatus("Попытка подключения к веб");
                await _dataSaver.StartDataTransfer(testName);
                _dataGetter.GetWebStatus("Связь установлена");
            }
            catch (Exception)
            {
                _dataGetter.GetWebStatus("Ошибка подключения к веб");
            }
        }

        #region Private Methods

        private void HandleDataFromUsart(string data)
        {
            _dataGetter.GetData(data);

            if (!_isDischargingStarted) return;

            try
            {
                _discharger.Discharge(Convert.ToDecimal(data), 0, DateTime.Now);
            }
            catch (Exception)
            {
            }
        }

        private async Task SendDataToWeb()
        {
            _measurementModel.Voltage = _dischargerModel.Voltage;
            _measurementModel.Current = _dischargerModel.Current;
            _measurementModel.MeasurementDateTime = _dischargerModel.CurrentDateTime.ToString();

            try
            {
                await _dataSaver.TransmitData(_measurementModel);
            }
            catch (Exception)
            {
                _dataGetter.GetWebStatus("Ошибка подключения к веб");
            }
        }

        private async Task HandleDischargingFinish()
        {
            if (_isOnlineModeEnabled)
            {
                try
                {
                    await _dataSaver.FinishDataTransfer(_dischargerModel.DischargeDuration, 
                        Decimal.Round(_dischargerModel.ResultCapacity, 2),
                        "Батарея разряжена");
                    _dataGetter.GetWebStatus("Соединение с веб остановлено");
                }
                catch (Exception)
                {
                    _dataGetter.GetWebStatus("Ошибка подключения к веб");
                }
            }

            _portController.StopDischarging();

            _dataGetter.GetPortStatus("Порт закрыт");
            _isOnlineModeEnabled = false;
            _isNewTestCreated = false;
            _discharger.SetDischargingParams(0, 0, 0);
            _isDischargingStarted = false;
        }
        #endregion
    }
}
