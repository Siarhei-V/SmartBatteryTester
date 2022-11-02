using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    internal class ResultsCalculatorFactory : IResultsCalculatorFactory
    {
        IResultsCalculator IResultsCalculatorFactory.MakeResultsCalculator()
        {
            return new ResultsCalculator();
        }
    }
}
