using Ninject;
using SmartBatteryTesterDesktopApp.PORT;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.UI.Temp;
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
        IPortDataHandler? _portDataHandler;

        protected override void OnStartup(StartupEventArgs e)
        {
            new UsartInitializer();
            _kernel = new StandardKernel();

            _portDataHandler = PortDataHandler.Instance;
            _portDataHandler.InitializeDataHandler(new TempDataSaver());
            _kernel.Bind<IPortDataHandler>().ToConstant(_portDataHandler);

            var appVM = _kernel.Get<MainWindowVM>();

            MainWindow = new MainWindow();
            MainWindow.DataContext = appVM;
            MainWindow.Show();
        }
    }
}
