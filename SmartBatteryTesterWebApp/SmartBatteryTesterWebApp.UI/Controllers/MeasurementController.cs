using Microsoft.AspNetCore.Mvc;

namespace SmartBatteryTesterWebApp.UI.Controllers
{
    public class MeasurementController : Controller
    {
        public IActionResult Index() => View();
    }
}
