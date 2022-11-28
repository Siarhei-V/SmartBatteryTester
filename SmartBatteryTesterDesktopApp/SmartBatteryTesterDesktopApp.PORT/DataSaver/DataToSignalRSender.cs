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

            _hubConnection = new HubConnectionBuilder().WithUrl("http://192.168.0.101/measurementsHub").Build();

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
                await _hubConnection.InvokeAsync("Send", $"Начат новый тест \"{_model.MeasurementName}\"", new MeasurementBaseModel());
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
                await _hubConnection.InvokeAsync("Send", "Батарея разряжается", baseMeasurement);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task IDataSaver.FinishDataTransfer()
        {
            string resultMessage = string.Empty;
            var resDuration = _model.DischargeDuration.ToString(@"hh\:mm\:ss");

            switch (_model.MeasurementStatus)
            {
                case "Батарея разряжена":
                    resultMessage = $"Тест \"{_model.MeasurementName}\" окончен<br>" +
                        $"Длительность теста: {resDuration}<br>" +
                        $"Измеренная емкость: {Decimal.Round(_model.ResultCapacity, 2)} Ач";
                    break;

                case "Разряд батареи прерван":
                    resultMessage = $"Тест \"{_model.MeasurementName}\" прерван";
                    break;
                case "Разряд батареи не был запущен":
                    resultMessage = _model.MeasurementStatus;
                    break;
                default:
                    break;
            }

            try
            {
                await _hubConnection.InvokeAsync("Send", resultMessage, new MeasurementBaseModel());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
