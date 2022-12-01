using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.UI.Infrastructure;
using SmartBatteryTesterWebApp.UI.Models;
using SmartBatteryTesterWebApp.UI.Models.Chart;

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
            ChartJsData chartData;

            var measurementSetDTO = _measurementService.FindMeasurementSet("Батарея разряжается");
            mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSetDTO, MeasurementSetViewModel>()).CreateMapper();
            var measurementSet = mapper.Map<MeasurementSetDTO, MeasurementSetViewModel>(measurementSetDTO);

            if (measurementSet == null) 
            {
                chartData = _chartCreator.GetLineChartData(new List<MeasurementViewModel>());
            }
            else
            {
                List<MeasurementDTO> measurementDTOs = _measurementService.GetMeasurement(measurementSet.Id);
                mapper = new MapperConfiguration(m => m.CreateMap<MeasurementDTO, MeasurementViewModel>()).CreateMapper();
                var measurements = mapper.Map<List<MeasurementDTO>, List<MeasurementViewModel>>(measurementDTOs);
                chartData = _chartCreator.GetLineChartData(measurements);
            }

            return View(chartData);
        }
    }
}
