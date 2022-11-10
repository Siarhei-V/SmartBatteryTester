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
        IPortInteractor? _portInteractor;
        INotifyDataChanged _dataChangedNotifier;

        protected override void OnStartup(StartupEventArgs e)
        {
            new UsartInitializer();
            _dataChangedNotifier = new DataChangedNotifier();
            _kernel = new StandardKernel();

            _portInteractor = PortInteractor.Instance;
            _portInteractor.InitializeDataHandler(new TempDataSaver());
            _portInteractor.NotifyDataChanged = _dataChangedNotifier;
            _kernel.Bind<IPortInteractor>().ToConstant(_portInteractor);
            _kernel.Bind<INotifyDataChanged>().ToConstant(_dataChangedNotifier);

            var appVM = _kernel.Get<MainWindowVM>();

            MainWindow = new MainWindow();
            MainWindow.DataContext = appVM;
            MainWindow.Show();
        }
    }
}
