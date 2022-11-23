using SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver;
using SmartBatteryTesterDesktopApp.PORT.Models;
using System.Net.Http.Json;

namespace SmartBatteryTesterDesktopApp.PORT.DataSaver
{
    internal class DataToWebApiSender : IDataSaver
    {
        HttpClient _httpClient;

        public DataToWebApiSender()
        {
            _httpClient = new HttpClient();
        }

        async Task IDataSaver.CreateNewTest(TestModel testModel)
        {
            using (_httpClient)
            {
                await _httpClient.PostAsJsonAsync("https://localhost:44373/api/Measurements/AddMeasurementSet", testModel);
            }
        }

        async Task IDataSaver.SaveData(MeasurementModel portDataModel)
        {
            using (_httpClient)
            {
                await _httpClient.PostAsJsonAsync("https://localhost:44373/api/Measurements/AddMeasurement", portDataModel);
            }
        }
    }
}
