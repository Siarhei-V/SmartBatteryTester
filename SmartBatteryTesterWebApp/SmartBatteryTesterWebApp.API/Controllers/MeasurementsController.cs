using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartBatteryTesterWebApp.API.Models;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;

namespace SmartBatteryTesterWebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MeasurementsController : ControllerBase
    {
        IMeasurementInputService _measurementService;

        public MeasurementsController(IMeasurementInputService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpPost]
        public async Task AddMeasurementSetAsync(MeasurementSetModel measurementSet)
        {
            var measurementSetDTO = MapMeasurementSetModelToMeasurementSetDTO(measurementSet);
            await _measurementService.MakeMeasurementSetAsync(measurementSetDTO);
        }

        [HttpPost]
        public async Task AddMeasurementAsync(MeasurementModel measurement)
        {
            IMapper mapper = new MapperConfiguration(m => m.CreateMap<MeasurementModel, MeasurementDTO>()).CreateMapper();
            var measurementDTO = mapper.Map<MeasurementModel, MeasurementDTO>(measurement);
            await _measurementService.MakeMeasurementAsync(measurementDTO);
        }

        [HttpPost]
        public async Task UpdateMeasurementSetAsync(MeasurementSetModel measurementSet)
        {
            var measurementSetDTO = MapMeasurementSetModelToMeasurementSetDTO(measurementSet);
            await _measurementService.UpdateMeasurementSetAsync(measurementSetDTO);
        }

        #region Private Methods
        private MeasurementSetDTO MapMeasurementSetModelToMeasurementSetDTO(MeasurementSetModel measurementSet)
        {
            IMapper mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSetModel, MeasurementSetDTO>()).CreateMapper();
            return mapper.Map<MeasurementSetModel, MeasurementSetDTO>(measurementSet);
        }
        #endregion
    }
}