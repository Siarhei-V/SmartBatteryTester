using Ninject;
using SmartBatteryTesterDesktopApp.UI.Models;
using SmartBatteryTesterDesktopApp.USART;
using SmartBatteryTesterDesktopApp.ViewModels;
using SmartBatteryTesterDesktopApp.Views;
using System.Windows;

namespace SmartBatteryTesterDesktopApp
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

            var appVM = _kernel.Get<MainWindowVM>();

            MainWindow = new MainWindow();
            MainWindow.DataContext = appVM;
            MainWindow.Show();
        }
    }
}
