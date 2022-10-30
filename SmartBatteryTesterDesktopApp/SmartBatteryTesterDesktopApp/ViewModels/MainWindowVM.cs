using SmartBatteryTesterDesktopApp.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SmartBatteryTesterDesktopApp.ViewModels
{
    internal class MainWindowVM : INotifyPropertyChanged
    {

        private List<string> _portNameListVM = new List<string>() { "COM 1", "COM 2", "COM 3", "COM 4", "COM 5"}; // TODO: temp
        public List<string> PortNameListVM
        {
            get => _portNameListVM;

        }

        private RelayCommand _connectToComPortCommand;
        public RelayCommand ConnectToComPortCommand
        {
            get
            {
                return _connectToComPortCommand ??
                    (_connectToComPortCommand = new RelayCommand(obj =>
                    {
                        MessageBox.Show("Command Test");    // TODO: temp
                    }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
