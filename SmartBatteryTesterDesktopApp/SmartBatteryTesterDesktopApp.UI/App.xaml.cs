using Ninject;
using SmartBatteryTesterDesktopApp.UI;
using SmartBatteryTesterDesktopApp.UI.Models;
using SmartBatteryTesterDesktopApp.UI.ViewModels;
using SmartBatteryTesterDesktopApp.UI.Views;
using SmartBatteryTesterDesktopApp.USART;
using SmartBatteryTesterDesktopApp.Views;
using System.Windows;

namespace SmartBatteryTesterDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal IDisplayRootRegistry _displayRootRegistry;
        internal MainWindowVM _mainWindowWM;

        IKernel _kernel;
        public IKernel Kernel => _kernel;

        public App()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IDisplayRootRegistry>().To<DisplayRootRegistry>().InSingletonScope();
            _displayRootRegistry = _kernel.Get<IDisplayRootRegistry>();
            _displayRootRegistry.RegisterWindowType<MainWindowVM, MainWindow>();
            _displayRootRegistry.RegisterWindowType<NewMeasurementModalWindowVM, NewMeasurementModalWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new UsartInitializer();


            _kernel.Bind<DischargingParameters>().ToSelf().InSingletonScope();

            _mainWindowWM = _kernel.Get<MainWindowVM>();

            await _displayRootRegistry.ShowModalPresentation(_mainWindowWM);
        }
    }
}
