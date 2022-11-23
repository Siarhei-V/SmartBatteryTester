using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver
{
    internal interface IDataSaverFacade
    {
        internal Task<int> CreateNewTest(string testName);
        internal Task SaveData(MeasurementModel portDataModel);
        internal Task FinishTest();
    }
}
