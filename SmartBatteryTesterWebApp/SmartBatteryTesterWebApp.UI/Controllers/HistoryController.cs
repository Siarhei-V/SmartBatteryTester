using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.UI.Infrastructure;
using SmartBatteryTesterWebApp.UI.Models;

namespace SmartBatteryTesterWebApp.UI.Controllers
{
    public class HistoryController : Controller
    {
        IMeasurementOutputService _measurementService;
        IMeasurementChartCreator _chartCreator;

        public HistoryController(IMeasurementOutputService measurementService, IMeasurementChartCreator chartCreator)
        {
            _measurementService = measurementService;
            _chartCreator = chartCreator;
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

            var chartData = _chartCreator.GetLineChartData(measurements);

            return View(chartData);
        }
    }
}