using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    public class DischargerInitializer : IDischargerInitializer
    {
        IDischarger? _discharger;
        IResultsCalculator? _resultsCalculator;
        DischargerModel? _dischargerModel;

        public IDischarger InitializeDischarger()
        {
            _dischargerModel = new DischargerModel();
            _resultsCalculator = new ResultsCalculator(_dischargerModel);
            _discharger = new Discharger(_dischargerModel);
            _discharger.ResultsCalculator = _resultsCalculator;

            return _discharger;
        }
    }
}
