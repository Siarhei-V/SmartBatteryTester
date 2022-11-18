using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.UI.Infrastructure;
using SmartBatteryTesterWebApp.UI.Models;

namespace SmartBatteryTesterWebApp.UI.Controllers
{
    public class MeasurementController : Controller
    {
        IMeasurementOutputService _measurementService;
        IMeasurementChartDataCreator _chartCreator;

        public MeasurementController(IMeasurementOutputService measurementService, IMeasurementChartDataCreator chartCreator)
        {
            _measurementService = measurementService;
            _chartCreator = chartCreator;
        }

        public IActionResult Index()
        {
            IMapper mapper;

            List<MeasurementDTO> measurementDTOs = _measurementService.GetMeasurement(3);
            mapper = new MapperConfiguration(m => m.CreateMap<MeasurementDTO, MeasurementViewModel>()).CreateMapper();
            var measurements = mapper.Map<List<MeasurementDTO>, List<MeasurementViewModel>>(measurementDTOs);

            var chartData = _chartCreator.GetLineChartData(measurements);

            return View(chartData);
        }
    }
}
