using SmartBatteryTesterDesktopApp.BL;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using System.Windows;

namespace SmartBatteryTesterDesktopApp.UI.Temp
{
    internal class TempDataSaver : IDischargerDataSaver
    {
        public void Save(DischargerDto values)  // TODO: This is a temporary implementation
        {
            MessageBox.Show(values.Voltage.ToString());
        }
    }
}
