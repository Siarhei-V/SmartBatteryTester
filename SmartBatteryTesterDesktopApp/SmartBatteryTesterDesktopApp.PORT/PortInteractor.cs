using SmartBatteryTesterDesktopApp.BL;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortInteractor : IUiInteractorInputPort, IUsartInteractorInputPort
    {
        static PortInteractor? _portInteractor;
        IPortController? _portController;
        IDataGetter _dataGetter;
        IDischargerInitializer _dischargerInitializer;
        IDischarger _discharger;
        DischargerDto _dischargerDto;

        // TODO: remove this
        ITempDataSaver _tempDataSaver;


        private PortInteractor() 
        {
            _dischargerInitializer = new DischargerInitializer();
            _discharger = _dischargerInitializer.InitializeDischarger();
        }

        // TODO: remove this prop
        public ITempDataSaver TempDataSaver
        {
            set => _tempDataSaver = value;
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

        public void SendUsartData(string data)
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
                _dischargerDto = _discharger.GetDischargingData();

                if (_dischargerDto.IsDischargingCompleted)
                {
                    _tempDataSaver.SaveData($"Discharging was finished\n" +
                        $"Discharging duration: {_dischargerDto.DischargeDuration}\n" +
                        $"Total capacity: {_dischargerDto.ResultCapacity}");
                    _portController.StopDischarging();
                    _dataGetter.GetData("Порт закрыт");
                    return;
                }
                else
                {
                    _tempDataSaver.SaveData(_dischargerDto.Voltage.ToString());
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
    }
}
