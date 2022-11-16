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
        public void AddMeasurementSet(string measurementName)
        {
            MeasurementSetDTO measurementSetDTO = new MeasurementSetDTO() { MeasurementName = measurementName};
            _measurementService.MakeMeasurementSet(measurementSetDTO);
        }
    }
}