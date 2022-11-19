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
        public void AddMeasurementSet(MeasurementSetDTO measurementSetDTO)
        {
            _measurementService.MakeMeasurementSet(measurementSetDTO);
        }

        [HttpPost]
        public void AddMeasurement(MeasurementDTO measurementDTO)
        {
            _measurementService.MakeMeasurement(measurementDTO);
        }

        [HttpPost]
        public void UpdateMeasurementSet(MeasurementSetDTO measurementSetDTO)
        {
            _measurementService.UpdateMeasurementSet(measurementSetDTO);
        }
    }
}