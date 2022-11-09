using Ninject;
using SmartBatteryTesterDesktopApp.PORT;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.UI.Infrastructure;
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
        INotifyDataChanged _dataChangedNotifier;

        protected override void OnStartup(StartupEventArgs e)
        {
            new UsartInitializer();
            _dataChangedNotifier = new DataChangedNotifier();
            _kernel = new StandardKernel();

            _portDataHandler = PortDataHandler.Instance;
            _portDataHandler.InitializeDataHandler(new TempDataSaver());
            _portDataHandler.NotifyDataChanged = _dataChangedNotifier;
            _kernel.Bind<IPortDataHandler>().ToConstant(_portDataHandler);
            _kernel.Bind<INotifyDataChanged>().ToConstant(_dataChangedNotifier);

            var appVM = _kernel.Get<MainWindowVM>();

            MainWindow = new MainWindow();
            MainWindow.DataContext = appVM;
            MainWindow.Show();
        }
    }
}
