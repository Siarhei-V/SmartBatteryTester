using Ninject;
using SmartBatteryTesterDesktopApp.UI.Commands.Base;
using SmartBatteryTesterDesktopApp.UI.Models;
using SmartBatteryTesterDesktopApp.UI.ViewModels;
using SmartBatteryTesterDesktopApp.UI.Views;
using System;
using System.Windows;

namespace SmartBatteryTesterDesktopApp.UI.Commands
{
    public class NewMeasurementModalWindowCommand : Command
    {
        IKernel _kernel = App.Kernel;
        NewMeasurementModalWindow _window;

        public override bool CanExecute(object parameter) => _window == null;

        public override void Execute(object parameter)
        {
            var window = new NewMeasurementModalWindow()
            {
                Owner = Application.Current.MainWindow
            };
            _window = window;
            window.DataContext = new NewMeasurementModalWindowVM(_kernel.Get<DischargingParameters>());
            window.Closed += OnWindowClosed;
            window.ShowDialog();
        }

        private void OnWindowClosed(object? sender, EventArgs e)
        {
            ((Window) sender).Closed -= OnWindowClosed;
            _window = null;
        }
    }
}
