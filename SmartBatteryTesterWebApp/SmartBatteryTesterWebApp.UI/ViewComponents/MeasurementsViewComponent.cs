using Microsoft.AspNetCore.Mvc;
using SmartBatteryTesterWebApp.UI.Models.Chart;

namespace SmartBatteryTesterWebApp.UI.ViewComponents
{
    public class MeasurementsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ChartJsData chartJs)
        {
            return View("MeasurementsChart", chartJs);
        }
    }
}
