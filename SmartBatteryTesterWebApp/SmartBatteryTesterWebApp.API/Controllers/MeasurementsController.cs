using Microsoft.AspNetCore.Mvc;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;

namespace SmartBatteryTesterWebApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MeasurementsController : ControllerBase
    {
        IMeasurementInputService _measurementService;

        public MeasurementsController(IMeasurementInputService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpPost]
        public async Task AddMeasurementSetAsync(MeasurementSetDTO measurementSetDTO)
        {
            await _measurementService.MakeMeasurementSetAsync(measurementSetDTO);
        }

        [HttpPost]
        public async Task AddMeasurementAsync(MeasurementDTO measurementDTO)
        {
            await _measurementService.MakeMeasurementAsync(measurementDTO);
        }

        [HttpPost]
        public async Task UpdateMeasurementSetAsync(MeasurementSetDTO measurementSetDTO)
        {
            await _measurementService.UpdateMeasurementSetAsync(measurementSetDTO);
        }
    }
}