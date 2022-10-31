namespace SmartBatteryTesterDesktopApp.BL
{
    public class Discharger
    {
        DischargerDto _dataModel;
        decimal _lowerDischargeThreshold;
        IResultSaver _resultSaver;
        ISwitchable _dischargerSwitch;

        public Discharger(IResultSaver resultSaver, ISwitchable dischargerSwitch)
        {
            _dataModel = new DischargerDto();
            _resultSaver = resultSaver;
            _dischargerSwitch = dischargerSwitch;
        }

        public void Start(decimal lowerDischargeThreshold, decimal voltageBeforeDischarging)
        {
            _dataModel.Voltage = voltageBeforeDischarging;
            _lowerDischargeThreshold = lowerDischargeThreshold;
            _resultSaver.Save();
            _dischargerSwitch.TurnOn();
        }



    }
}