using SmartBatteryTesterDesktopApp.Commands;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.UI.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartBatteryTesterDesktopApp.ViewModels
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        ComPortConnectionParameters _parameters;
        IPortDataHandler _portDataHandler;
        Dictionary<string, string> _startParameters;

        public MainWindowVM(ComPortConnectionParameters parameters, IPortDataHandler portDataHandler)
        {
            _parameters = parameters;
            _portDataHandler = portDataHandler;
        }

        #region Parameter Lists
        public List<string> PortNameListVM
        {
            get => _parameters.PortNamesList;
        }

        public List<string> BaudRateListVM
        {
            get => _parameters.BaudRatesList;
        }

        public List<string> DataBitsListVM
        {
            get => _parameters.DataBitsList;
        }

        public List<string> ParityListVM
        {
            get => _parameters.ParityList;
        }

        public List<string> StopBitsListVM
        {
            get => _parameters.StopBitsList;
        }
        #endregion

        #region Selected Parameters
        public string SelectedPortNameVM
        {
            set => _parameters.SelectedPortName = value;
        }

        public string SelectedBaudRateVM
        {
            set => _parameters.SelectedBaudRate = value;
        }

        public string SelectedDataBitsVM
        {
            set => _parameters.SelectedDataBits = value;
        }

        public string SelectedParityVM
        {
            set => _parameters.SelectedParity = value;
        }

        public string SelectedStopBitsVM
        {
            set => _parameters.SelectedStopBits = value;
        }
        #endregion

        private RelayCommand _connectToComPortCommand;
        public RelayCommand ConnectToComPortCommand
        {
            get
            {
                return _connectToComPortCommand ??
                    (_connectToComPortCommand = new RelayCommand(obj =>
                    {
                        CreateParameterDictionary();
                        _portDataHandler.StartDischarging(_startParameters);
                    }));
            }
        }

        private void CreateParameterDictionary()
        {
            _startParameters = new Dictionary<string, string>()
            {
                ["PortName"]    = _parameters.SelectedPortName,
                ["BaudRate"]    = _parameters.SelectedBaudRate,
                ["DataBits"]    = _parameters.SelectedDataBits,
                ["Parity"]      = _parameters.SelectedParity,
                ["StopBits"]    = _parameters.SelectedStopBits,

                ["LowDischargeVoltage"] = "3",
                ["ValuesChangeDiscreteness"] = "0,5"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
