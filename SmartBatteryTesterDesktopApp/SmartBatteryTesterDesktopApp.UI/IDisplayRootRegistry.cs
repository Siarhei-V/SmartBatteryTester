using System.Threading.Tasks;

namespace SmartBatteryTesterDesktopApp.UI
{
    internal interface IDisplayRootRegistry
    {
        internal void RegisterWindowType<VM, Window>();
        internal Task ShowModalPresentation(object vm);
    }
}
