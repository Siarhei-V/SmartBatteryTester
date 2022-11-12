using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using System.Windows;

namespace SmartBatteryTesterDesktopApp.UI.Temp
{
    internal class TempDataSaver : ITempDataSaver
    {
        public void SaveData(string str)
        {
            MessageBox.Show(str);
        }
    }
}
