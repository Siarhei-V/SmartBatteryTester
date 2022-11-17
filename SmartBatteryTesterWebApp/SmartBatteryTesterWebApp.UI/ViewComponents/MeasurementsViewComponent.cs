using Microsoft.AspNetCore.Mvc;
using SmartBatteryTesterWebApp.UI.Models;

namespace SmartBatteryTesterWebApp.UI.ViewComponents
{
    public class MeasurementsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<MeasurementViewModel> measurementViewModel)
        {
            return View("MeasurementsChart", measurementViewModel);
        }
    }
}
