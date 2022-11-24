using SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver;
using SmartBatteryTesterDesktopApp.PORT.Models;
using System.Net.Http.Json;

namespace SmartBatteryTesterDesktopApp.PORT.DataSaver
{
    internal class DataToWebApiSender : IDataSaver
    {
        int _currentTestId = 0;
        TestModel _model;

        async Task IDataSaver.StartDataTransfer(TestModel testModel)
        {
            _model = testModel;
            using (HttpClient httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync("https://localhost:44373/api/Measurements/AddMeasurementSet", testModel);
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var receivedTest = await httpClient.GetAsync(
                    "https://localhost:44373/api/Measurements/FindMeasurementSet?status=Батарея разряжается");
                _currentTestId = (await receivedTest.Content.ReadFromJsonAsync<TestModel>()).Id;
            }
        }

        async Task IDataSaver.TransmitData(MeasurementModel measurementModel)
        {
            measurementModel.MeasurementSetId = _currentTestId;
            using (HttpClient httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync("https://localhost:44373/api/Measurements/AddMeasurement", measurementModel);
            }
        }

        async Task IDataSaver.FinishDataTransfer()
        {
            _model.Id = _currentTestId;
            using (HttpClient httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync("https://localhost:44373/api/Measurements/UpdateMeasurementSet", _model);
            }
        }
    }
}
