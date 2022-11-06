using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    public class DischargerFacade : IDischargerFacade
    {
        private IDischarger? _discharger;
        private IResultsCalculator? _resultsCalculator;
        private DischargerDto? _dischargerDto;

        public void InitializeDischarger(IDischargerDataSaver dataSaver, IDischargerController dischargerController)
        {
            _dischargerDto = new DischargerDto();
            _resultsCalculator = new ResultsCalculator(_dischargerDto);
            _discharger = new Discharger(dataSaver, _resultsCalculator, _dischargerDto, dischargerController);
        }

        public void StartDischarging(decimal lowerDischargeThreshold, decimal voltageBeforeDischarging, decimal valuesChangeDiscreteness)
        {
            _discharger.Start(lowerDischargeThreshold, voltageBeforeDischarging, valuesChangeDiscreteness);
        }

        public void Discharge(decimal voltage, decimal current)
        {
            _discharger.Discharge(voltage, current);
        }
    }
}
