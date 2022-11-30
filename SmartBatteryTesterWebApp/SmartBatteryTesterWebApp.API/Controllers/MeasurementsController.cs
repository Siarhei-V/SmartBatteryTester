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
        IMeasurementInputService _measurementInputService;
        IMeasurementOutputService _measurementOutputService;

        public MeasurementsController(IMeasurementInputService measurementService, IMeasurementOutputService measurementOutputService)
        {
            _measurementInputService = measurementService;
            _measurementOutputService = measurementOutputService;
        }

        [HttpPost]
        public async Task AddMeasurementSetAsync(MeasurementSetModel measurementSet)
        {
            var measurementSetDTO = MapMeasurementSetModelToMeasurementSetDTO(measurementSet);
            await _measurementInputService.MakeMeasurementSetAsync(measurementSetDTO);
        }

        [HttpPost]
        public async Task AddMeasurementAsync(MeasurementModel measurement)
        {
            IMapper mapper = new MapperConfiguration(m => m.CreateMap<MeasurementModel, MeasurementDTO>()).CreateMapper();
            var measurementDTO = mapper.Map<MeasurementModel, MeasurementDTO>(measurement);
            await _measurementInputService.MakeMeasurementAsync(measurementDTO);
        }

        [HttpPost]
        public async Task UpdateMeasurementSetAsync(MeasurementSetModel measurementSet)
        {
            var measurementSetDTO = MapMeasurementSetModelToMeasurementSetDTO(measurementSet);
            await _measurementInputService.UpdateMeasurementSetAsync(measurementSetDTO);
        }

        [HttpGet]
        public async Task<MeasurementSetModel> FindMeasurementSetAsync(string status)
        {
            var measurementSet = await _measurementOutputService.FindMeasurementSetAsync(status);
            var mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSetDTO, MeasurementSetModel>()).CreateMapper();
            var result = mapper.Map<MeasurementSetDTO, MeasurementSetModel>(measurementSet);
            return result;
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