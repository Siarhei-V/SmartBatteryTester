using Ninject;
using SmartBatteryTesterDesktopApp.ViewModels;
using System.Windows;

namespace SmartBatteryTesterDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static IKernel kernel;
        public static IKernel Kernel => kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            kernel = new StandardKernel();

 

            var appVM = kernel.Get<MainWindowVM>();

            MainWindow = new MainWindow();
            MainWindow.DataContext = appVM;
            MainWindow.Show();
        }
    }
}
