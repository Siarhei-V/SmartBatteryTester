using Ninject;
using SmartBatteryTesterDesktopApp.UI.Models;
using SmartBatteryTesterDesktopApp.USART;
using SmartBatteryTesterDesktopApp.UI.ViewModels;
using SmartBatteryTesterDesktopApp.UI.Views;
using System.Windows;
using SmartBatteryTesterDesktopApp.UI.Commands;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.PORT;

namespace SmartBatteryTesterDesktopApp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static IKernel _kernel;
        public static IKernel Kernel => _kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            new UsartInitializer();
            _kernel = new StandardKernel();

            _kernel.Bind<DischargingParameters>().ToSelf().InSingletonScope();

            var _portInteractor = PortInteractor.Instance;
            _kernel.Bind<IUiInteractorInputPort>().ToConstant(_portInteractor);

            var appVM = _kernel.Get<MainWindowVM>();

            MainWindow = new MainWindow();
            MainWindow.DataContext = appVM;
            MainWindow.Show();
        }
    }
}
