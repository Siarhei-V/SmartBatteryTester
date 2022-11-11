using SmartBatteryTesterDesktopApp.BL;
using SmartBatteryTesterDesktopApp.BL.Interfaces;
using System.Windows;

namespace SmartBatteryTesterDesktopApp.UI.Temp
{
    internal class TempDataSaver : IDischargerDataSaver
    {
        public void Save(DischargerDto values)  // TODO: This is a temporary implementation
        {
            if (values.IsDischargingCompleted)
            {
                MessageBox.Show(values.DischargeDuration.ToString());
                MessageBox.Show(values.ResultCapacity.ToString());
                return;
            }
            MessageBox.Show(values.Voltage.ToString());
        }
    }
}
