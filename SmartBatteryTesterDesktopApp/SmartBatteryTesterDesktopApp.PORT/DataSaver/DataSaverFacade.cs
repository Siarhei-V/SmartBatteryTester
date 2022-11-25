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

        async Task IDataSaverFacade.StartDataTransfer(string testName)
        {
            _testModel.MeasurementName = testName;
            _testModel.MeasurementStatus = "Батарея разряжается";
            await _dataToWebApiSender.StartDataTransfer(_testModel);
            await _dataToSignalRSender.StartDataTransfer(_testModel);
        }

        async Task IDataSaverFacade.TransmitData(MeasurementModel portDataModel)
        {
            await _dataToWebApiSender.TransmitData(portDataModel);
            _dataToSignalRSender?.TransmitData(portDataModel);
        }

        async Task IDataSaverFacade.FinishDataTransfer(TimeSpan dischargingDuration, decimal resultCapacity, string status)
        {
            _testModel.MeasurementStatus = status;
            _testModel.DischargeDuration = dischargingDuration;
            _testModel.ResultCapacity = resultCapacity;
            await _dataToWebApiSender.FinishDataTransfer();
            await _dataToSignalRSender.FinishDataTransfer();
        }
    }
}
