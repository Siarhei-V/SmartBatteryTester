using Ninject;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.UI.Commands.Base;
using SmartBatteryTesterDesktopApp.UI.Models;
using System.Windows;

namespace SmartBatteryTesterDesktopApp.UI.Commands
{
    internal class NewTestCommand : Command
    {
        readonly IKernel _kernel = App.Kernel;
        readonly IUiInteractorInputPort _portInteractor;
        readonly DischargingParameters _dischargingParameters;

        public NewTestCommand()
        {
            _portInteractor = _kernel.Get<IUiInteractorInputPort>();
            _dischargingParameters = _kernel.Get<DischargingParameters>();
        }

        internal bool DialogResult { get; set; }

        public override bool CanExecute(object parameter) => parameter is Window;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            ((Window)parameter).DialogResult = DialogResult;
            _portInteractor.CreateNewTest(_dischargingParameters.TestName);
        }
    }
}
