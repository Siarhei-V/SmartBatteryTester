using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver
{
    internal interface IDataSaver
    {
        internal Task CreateNewTest(TestModel testModel);
        internal Task SaveData(MeasurementModel portDataModel);
    }
}
