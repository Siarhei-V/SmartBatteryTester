using SmartBatteryTesterDesktopApp.BL.Interfaces;

namespace SmartBatteryTesterDesktopApp.BL
{
    internal class CapacityCalculatorFactory : ICapacityCalculatorFactory
    {
        ICapacityCalculator ICapacityCalculatorFactory.MakeCapacityCalculator()
        {
            return new CapacityCalculator();
        }
    }
}
