using SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver;
using SmartBatteryTesterDesktopApp.PORT.Models;
using System.Net.Http.Json;

namespace SmartBatteryTesterDesktopApp.PORT.DataSaver
{
    internal class DataToWebApiSender : IDataSaver
    {
        async Task<int> IDataSaver.CreateNewTest(TestModel testModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync("https://localhost:44373/api/Measurements/AddMeasurementSet", testModel);
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var receivedTest = await httpClient.GetAsync(
                    "https://localhost:44373/api/Measurements/FindMeasurementSet?status=Батарея разряжается");
                var deserializedTest = await receivedTest.Content.ReadFromJsonAsync<TestModel>();
                return deserializedTest.Id;
            }
        }

        async Task IDataSaver.SaveData(MeasurementModel portDataModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync("https://localhost:44373/api/Measurements/AddMeasurement", portDataModel);
            }
        }

        async Task IDataSaver.FinishTest(TestModel testModel)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                await httpClient.PostAsJsonAsync("https://localhost:44373/api/Measurements/UpdateMeasurementSet", testModel);
            }
        }
    }
}
