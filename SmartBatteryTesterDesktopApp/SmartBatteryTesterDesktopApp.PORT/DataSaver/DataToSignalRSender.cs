﻿using SmartBatteryTesterDesktopApp.PORT.Interfaces.DataSaver;
using SmartBatteryTesterDesktopApp.PORT.Models;

namespace SmartBatteryTesterDesktopApp.PORT.DataSaver
{
    internal class DataToSignalRSender : IDataSaver
    {
        async Task IDataSaver.CreateNewTest(TestModel testModel)
        {
            throw new NotImplementedException();
        }

        async Task IDataSaver.SaveData(MeasurementModel portDataModel)
        {
            throw new NotImplementedException();
        }
    }
}
