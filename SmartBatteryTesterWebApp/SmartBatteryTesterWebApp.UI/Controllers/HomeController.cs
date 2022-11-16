using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.UI.Models;
using System.Diagnostics;

namespace SmartBatteryTesterWebApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IMeasurementService _measurementService;
        MeasurementViewModel _testModel;

        public HomeController(ILogger<HomeController> logger, IMeasurementService measurementService)
        {
            _logger = logger;
            _measurementService = measurementService;
            _testModel = new MeasurementViewModel();
        }

        public IActionResult Index()
        {
            List<MeasurementSetDTO> measurementSetDTOs = _measurementService.GetMeasurementSet();
            var mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSetDTO, MeasurementSetViewModel>()).CreateMapper();
            var measurements = mapper.Map<List<MeasurementSetDTO>, List<MeasurementSetViewModel>>(measurementSetDTOs);


            return View(measurements);
        }

        [HttpGet]
        public IActionResult TestMeasurementName() => View();

        [HttpPost]
        public IActionResult TestMeasurementName(string measurementName)
        {
            MeasurementSetDTO measurementSetDTO = new MeasurementSetDTO() { MeasurementName = measurementName};
            _measurementService.MakeMeasurementSet(measurementSetDTO);
            return RedirectToAction("Index");
        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}