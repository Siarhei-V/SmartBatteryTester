using SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver;
using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT.DataSaver
{
    internal class DataSaverFacade : IDataSaverFacade
    {
        IDataSaverFactory _dataSaverFactory;
        IDataSaver _dataToWebApiSender;
        IDataSaver _dataToSignalRSender;
        TestModel _testModel;

        public DataSaverFacade(IDataSaverFactory dataSaverFactory)
        {
            _dataSaverFactory = dataSaverFactory;
            _dataToWebApiSender = _dataSaverFactory.MakeDataToWebApiSender();
            _dataToSignalRSender = _dataSaverFactory.MakeDataToSignalRSender();
            _testModel = new TestModel();
        }

        async Task<int> IDataSaverFacade.CreateNewTest(string testName)
        {
            _testModel.MeasurementName = testName;
            _testModel.MeasurementStatus = "Батарея разряжается";
            _testModel.Id = await _dataToWebApiSender.CreateNewTest(_testModel);
            return _testModel.Id;
        }

        async Task IDataSaverFacade.SaveData(MeasurementModel portDataModel)
        {
            await _dataToWebApiSender.SaveData(portDataModel);
            _dataToSignalRSender?.SaveData(portDataModel);
        }

        async Task IDataSaverFacade.FinishTest()
        {
            _testModel.MeasurementStatus = "Батарея разряжена";
            await _dataToWebApiSender.FinishTest(_testModel);
        }
    }
}
