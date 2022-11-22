using SmartBatteryTesterDesktopApp.UI.Commands.Base;
using SmartBatteryTesterDesktopApp.UI.Views;
using System;
using System.Windows;

namespace SmartBatteryTesterDesktopApp.UI.Commands
{
    internal class CreateNewMeasurementModalWindowCommand : Command
    {
        private NewMeasurementModalWindow _window;


        public override bool CanExecute(object parameter) => _window == null;

        public override void Execute(object parameter)
        {
            var window = new NewMeasurementModalWindow()
            {
                Owner = Application.Current.MainWindow
            };

            _window = window;
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
