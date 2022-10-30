using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartBatteryTesterDesktopApp.ViewModels
{
    internal class MainWindowVM
    {




        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
