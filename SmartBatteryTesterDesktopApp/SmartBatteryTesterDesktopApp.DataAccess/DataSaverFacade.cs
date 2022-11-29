using AutoMapper;
using SmartBatteryTesterDesktopApp.DataAccess.Interfaces;
using SmartBatteryTesterDesktopApp.DataAccess.Models;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.DataAccess
{
    internal class DataSaverFacade : IDataSaver
    {
        IDataSenderFactory _dataSaverFactory;
        IDataSender _dataToWebApiSender;
        IDataSender _dataToSignalRSender;
        TestDataModel _testModel;

        internal DataSaverFacade(IDataSenderFactory dataSenderFactory)
        {
            _dataSaverFactory = dataSenderFactory;
            _dataToWebApiSender = _dataSaverFactory.MakeDataToWebApiSender();
            _dataToSignalRSender = _dataSaverFactory.MakeDataToSignalRSender();
            _testModel = new TestDataModel();
        }

        public async Task StartDataTransfer(string testName)
        {
            _testModel.MeasurementName = testName;
            _testModel.MeasurementStatus = "Батарея разряжается";
            await _dataToWebApiSender.StartDataTransfer(_testModel);
            await _dataToSignalRSender.StartDataTransfer(_testModel);
        }

        public async Task TransmitData(MeasurementModel portDataModel)
        {
            IMapper mapper = new MapperConfiguration(m => m.CreateMap<MeasurementModel, MeasurementDataModel>()).CreateMapper();
            var measurementDataModel = mapper.Map<MeasurementModel, MeasurementDataModel>(portDataModel);

            await _dataToWebApiSender.TransmitData(measurementDataModel);
            await _dataToSignalRSender.TransmitData(measurementDataModel);
        }

        public async Task FinishDataTransfer(TimeSpan dischargingDuration, decimal resultCapacity, string status)
        {
            _testModel.MeasurementStatus = status;
            _testModel.DischargeDuration = dischargingDuration;
            _testModel.ResultCapacity = resultCapacity;
            await _dataToWebApiSender.FinishDataTransfer();
            await _dataToSignalRSender.FinishDataTransfer();
        }
    }
}