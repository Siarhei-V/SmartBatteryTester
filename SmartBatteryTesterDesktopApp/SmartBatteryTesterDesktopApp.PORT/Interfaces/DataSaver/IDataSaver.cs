using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver
{
    internal interface IDataSaver
    {
        internal Task<int> CreateNewTest(TestModel testModel);
        internal Task SaveData(MeasurementModel portDataModel);
        internal Task FinishTest(TestModel testModel);
    }
}
