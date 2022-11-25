﻿using SmartBatteryTesterDesktopApp.BL;
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
        MeasurementModel _measurementModel;
        IDataSaverFacade _dataSaver;
        IDataSaverFactory _dataSaverFactory;
        bool _isOnlineModeEnabled;

        private PortInteractor() 
        {
            _dischargerInitializer = new DischargerInitializer();
            _measurementModel = new MeasurementModel();
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
            _dischargerModel = _discharger.GetDischargingData();
            HandleDataFromUsart(data);

            if (!_discharger.IsNewDataReceived) return;

            if (_isOnlineModeEnabled) 
                await SendDataToWeb();

            if (!_dischargerModel.IsDischargingCompleted) return;

            await HandleDischargingFinish();
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
            _measurementModel.Current = Convert.ToDecimal(portConnectionParameters["TempDischargingCurrent"]);  // TODO: temp

            if (portConnectionParameters["Mode"] == "OnlineMode")
                _isOnlineModeEnabled = true;
            else if (portConnectionParameters["Mode"] == "OfflineMode")
                _isOnlineModeEnabled = false;
        }

        public void StopDischarging()
        {
            _portController.StopDischarging();

            if (!_isOnlineModeEnabled) 
                return;

            try
            {
                _dataSaver.FinishDataTransfer(_dischargerModel.DischargeDuration, _dischargerModel.ResultCapacity,
                    "Разряд батареи прерван");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async void CreateNewTest(string testName)
        {
            try
            {
                await _dataSaver.StartDataTransfer(testName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods

        private void HandleDataFromUsart(string data)
        {
            try
            {
                _discharger.Discharge(Convert.ToDecimal(data), 0, DateTime.Now);
            }
            catch (Exception)
            {
            }

            _dataGetter.GetData(data);
        }

        private async Task SendDataToWeb()
        {
            _measurementModel.Voltage = _dischargerModel.Voltage;
            //_measurementModel.Current = _dischargerModel.Current;
            _measurementModel.MeasurementDateTime = _dischargerModel.CurrentDateTime.ToString();

            try
            {
                await _dataSaver.TransmitData(_measurementModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task HandleDischargingFinish()
        {
            if (_isOnlineModeEnabled)
            {
                try
                {
                    await _dataSaver.FinishDataTransfer(_dischargerModel.DischargeDuration, _dischargerModel.ResultCapacity,
                        "Батарея разряжена");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            _portController.StopDischarging();

            _dataGetter.GetData("Порт закрыт");
        }
        #endregion
    }
}
