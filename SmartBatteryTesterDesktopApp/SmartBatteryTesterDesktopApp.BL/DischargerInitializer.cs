using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    public class DischargerInitializer : IDischargerInitializer
    {
        IDischarger? _discharger;
        IResultsCalculator? _resultsCalculator;
        DischargerDto? _dischargerDto;

        public IDischarger InitializeDischarger()
        {
            _dischargerDto = new DischargerDto();
            _resultsCalculator = new ResultsCalculator(_dischargerDto);
            _discharger = new Discharger(_dischargerDto);
            _discharger.ResultsCalculator = _resultsCalculator;

            return _discharger;
        }
    }
}
