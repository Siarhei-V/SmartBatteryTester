using Microsoft.AspNetCore.SignalR.Client;
using SmartBatteryTesterDesktopApp.DataAccess.Interfaces;
using SmartBatteryTesterDesktopApp.DataAccess.Models;

namespace SmartBatteryTesterDesktopApp.DataAccess
{
    internal class DataToSignalRSender : IDataSender
    {
        TestDataModel _model;
        HubConnection _hubConnection;

        async Task IDataSender.StartDataTransfer(TestDataModel testModel)
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
                await _hubConnection.InvokeAsync("Send", $"Начат новый тест \"{_model.MeasurementName}\"", new MeasurementBaseDataModel());
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task IDataSender.TransmitData(MeasurementDataModel measurementModel)
        {
            try
            {
                var baseMeasurement = new MeasurementBaseDataModel()
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

        async Task IDataSender.FinishDataTransfer()
        {
            string resultMessage = string.Empty;
            var resDuration = _model.DischargeDuration.ToString(@"hh\:mm\:ss");

            switch (_model.MeasurementStatus)
            {
                case "Батарея разряжена":
                    resultMessage = $"Тест \"{_model.MeasurementName}\" окончен<br>" +
                        $"Длительность теста: {resDuration}<br>" +
                        $"Измеренная емкость: {_model.ResultCapacity} Ач";
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
                await _hubConnection.InvokeAsync("Send", resultMessage, new MeasurementBaseDataModel());
                await _hubConnection.StopAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
