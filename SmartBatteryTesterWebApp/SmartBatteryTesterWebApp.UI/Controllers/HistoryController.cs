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
        IMeasurementChartDataCreator _chartCreator;

        public HistoryController(IMeasurementOutputService measurementService, IMeasurementChartDataCreator chartCreator)
        {
            _measurementService = measurementService;
            _chartCreator = chartCreator;
        }

        public async Task<IActionResult> IndexAsync(int measurementSetNumber)
        {
            IMapper mapper;
            List<MeasurementSetDTO> measurementSetDTOs = await _measurementService.GetMeasurementSetAsync();
            mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSetDTO, MeasurementSetViewModel>()).CreateMapper();
            var measurementSets = mapper.Map<List<MeasurementSetDTO>, List<MeasurementSetViewModel>>(measurementSetDTOs);
            ViewBag.MeasurementSetList = new SelectList(measurementSets, "Id", "MeasurementName");

            if (measurementSetNumber == 0)
            {
                ViewBag.MeasurementResults = null;
            }
            else
            {
                ViewBag.MeasurementResults = measurementSets.Where(m => m.Id == measurementSetNumber).FirstOrDefault();
            }

            List<MeasurementDTO> measurementDTOs = await _measurementService.GetMeasurementAsync(measurementSetNumber);
            mapper = new MapperConfiguration(m => m.CreateMap<MeasurementDTO, MeasurementViewModel>()).CreateMapper();
            var measurements = mapper.Map<List<MeasurementDTO>, List<MeasurementViewModel>>(measurementDTOs);

            var chartData = _chartCreator.GetLineChartData(measurements);

            return View(chartData);
        }
    }
}