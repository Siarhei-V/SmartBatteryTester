using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SmartBatteryTesterDesktopApp.ViewModels
{
    internal class MainWindowVM : INotifyPropertyChanged
    {

        private List<string> _portNameListVM = new List<string>() { "COM 1", "COM 2", "COM 3", "COM 4", "COM 5"};
        public List<string> PortNameListVM
        {
            get => _portNameListVM;

        }

        


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
