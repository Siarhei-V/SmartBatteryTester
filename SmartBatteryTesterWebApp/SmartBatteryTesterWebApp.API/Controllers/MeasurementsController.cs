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
        public void AddMeasurementSet(MeasurementSetModel measurementSet)
        {
            var measurementSetDTO = MapMeasurementSetModelToMeasurementSetDTO(measurementSet);
            _measurementInputService.MakeMeasurementSet(measurementSetDTO);
        }

        [HttpPost]
        public void AddMeasurement(MeasurementModel measurement)
        {
            IMapper mapper = new MapperConfiguration(m => m.CreateMap<MeasurementModel, MeasurementDTO>()).CreateMapper();
            var measurementDTO = mapper.Map<MeasurementModel, MeasurementDTO>(measurement);
            _measurementInputService.MakeMeasurement(measurementDTO);
        }

        [HttpPost]
        public void UpdateMeasurementSet(MeasurementSetModel measurementSet)
        {
            var measurementSetDTO = MapMeasurementSetModelToMeasurementSetDTO(measurementSet);
            _measurementInputService.UpdateMeasurementSet(measurementSetDTO);
        }

        [HttpGet]
        public MeasurementSetModel FindMeasurementSet(string status)
        {
            var measurementSet = _measurementOutputService.FindMeasurementSet(status);
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