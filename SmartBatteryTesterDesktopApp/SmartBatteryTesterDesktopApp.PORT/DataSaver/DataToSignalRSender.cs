using Microsoft.AspNetCore.SignalR.Client;
using SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver;
using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT.DataSaver
{
    internal class DataToSignalRSender : IDataSaver
    {
        TestModel _model;
        HubConnection _hubConnection;

        async Task IDataSaver.StartDataTransfer(TestModel testModel)
        {
            _model = testModel;

            _hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:44357/measurementsHub").Build();

            try
            {
                await _hubConnection.StartAsync();
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                await _hubConnection.InvokeAsync("Send", $"Начат новый тест {_model.MeasurementName}", new MeasurementBaseModel());
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task IDataSaver.TransmitData(MeasurementModel measurementModel)
        {
            try
            {
                var baseMeasurement = new MeasurementBaseModel()
                {
                    Voltage = measurementModel.Voltage,
                    Current = measurementModel.Current,
                    MeasurementDateTime = measurementModel.MeasurementDateTime,
                };
                await _hubConnection.InvokeAsync("Send", "Получены новые данные", baseMeasurement);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task IDataSaver.FinishDataTransfer()
        {
            throw new NotImplementedException();
        }
    }
}
