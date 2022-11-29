using SmartBatteryTesterDesktopApp.DataAccess.Interfaces;
using SmartBatteryTesterDesktopApp.DataAccess.Models;
using System.Net.Http.Json;

namespace SmartBatteryTesterDesktopApp.DataAccess
{
    internal class DataToWebApiSender : IDataSender
    {
        int _currentTestId = 0;
        TestDataModel _model;
        string _ipAddress;
        readonly string _ipFilePath = @"Settings\WebApiIpAddress.txt";

        public DataToWebApiSender()
        {
            GetIpAddress();
        }

        async Task IDataSender.StartDataTransfer(TestDataModel testModel)
        {
            _model = testModel;
            using (HttpClient httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync(_ipAddress + "api/Measurements/AddMeasurementSet", testModel);
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var receivedTest = await httpClient.GetAsync(
                    _ipAddress + "api/Measurements/FindMeasurementSet?status=Батарея разряжается");
                _currentTestId = (await receivedTest.Content.ReadFromJsonAsync<TestDataModel>()).Id;
            }
        }

        async Task IDataSender.TransmitData(MeasurementDataModel measurementModel)
        {
            measurementModel.MeasurementSetId = _currentTestId;
            using (HttpClient httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync(_ipAddress + "api/Measurements/AddMeasurement", measurementModel);
            }
        }

        async Task IDataSender.FinishDataTransfer()
        {
            _model.Id = _currentTestId;

            using (HttpClient httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync(_ipAddress + "api/Measurements/UpdateMeasurementSet", _model);
            }
            _model.Id = 0;
        }

        #region Private Methods

        #endregion
        private void GetIpAddress()
        {
            _ipAddress = File.ReadAllText(_ipFilePath);
        }
    }
}
