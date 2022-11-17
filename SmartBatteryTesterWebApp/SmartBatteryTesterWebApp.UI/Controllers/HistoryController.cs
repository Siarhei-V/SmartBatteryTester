using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.UI.Models;

namespace SmartBatteryTesterWebApp.UI.Controllers
{
    public class HistoryController : Controller
    {
        IMeasurementOutputService _measurementService;

        public HistoryController(IMeasurementOutputService measurementService)
        {
            _measurementService = measurementService;
        }

        public IActionResult Index(int measurementSetNumber)
        {
            IMapper mapper;

            List<MeasurementSetDTO> measurementSetDTOs = _measurementService.GetMeasurementSet();
            mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSetDTO, MeasurementSetViewModel>()).CreateMapper();
            var measurementSets = mapper.Map<List<MeasurementSetDTO>, List<MeasurementSetViewModel>>(measurementSetDTOs);
            ViewBag.MeasurementSetList = new SelectList(measurementSets, "Id", "MeasurementName");

            List<MeasurementDTO> measurementDTOs = _measurementService.GetMeasurement(measurementSetNumber);
            mapper = new MapperConfiguration(m => m.CreateMap<MeasurementDTO, MeasurementViewModel>()).CreateMapper();
            var measurements = mapper.Map<List<MeasurementDTO>, List<MeasurementViewModel>>(measurementDTOs);
            ViewBag.Measurements = measurements;
            return View();
        }
    }
}