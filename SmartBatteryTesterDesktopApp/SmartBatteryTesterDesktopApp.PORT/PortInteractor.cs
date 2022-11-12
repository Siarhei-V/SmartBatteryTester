﻿using SmartBatteryTesterDesktopApp.PORT.Interfaces;

namespace SmartBatteryTesterDesktopApp.PORT
{
    public class PortInteractor : IInteractorInputPort, IControllerInstanceSetter
    {
        static PortInteractor? _portInteractor;
        IPortController? _portController;

        private PortInteractor() { }

        public static PortInteractor Instance
        {
            get => _portInteractor ?? (_portInteractor = new PortInteractor());
        }

        public IPortController PortController
        {
            set => _portController = value;
        }

        public void StartDischarging(Dictionary<string, string> portConnectionParameters)
        {
            _portController.StartDischarging(portConnectionParameters);
        }

        public void StopDischarging()
        {
            _portController.StopDischarging();
        }
    }
}
