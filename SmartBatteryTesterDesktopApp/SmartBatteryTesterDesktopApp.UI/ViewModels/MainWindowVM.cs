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
        ComPortConnectionParameters _connectionParameters;
        DischargingParameters _dischargingParameters;
        IPortDataHandler _portDataHandler;
        Dictionary<string, string> _startParameters;

        public MainWindowVM(ComPortConnectionParameters parameters, DischargingParameters dischargingParameters,
            IPortDataHandler portDataHandler)
        {
            _connectionParameters = parameters;
            _dischargingParameters = dischargingParameters;
            _portDataHandler = portDataHandler;
        }

        #region Parameter Lists
        public List<string> PortNameListVM
        {
            get => _connectionParameters.PortNamesList;
        }

        public List<string> BaudRateListVM
        {
            get => _connectionParameters.BaudRatesList;
        }

        public List<string> DataBitsListVM
        {
            get => _connectionParameters.DataBitsList;
        }

        public List<string> ParityListVM
        {
            get => _connectionParameters.ParityList;
        }

        public List<string> StopBitsListVM
        {
            get => _connectionParameters.StopBitsList;
        }

        public List<string> ValuesChangeDiscretennesListVM
        {
            get => _dischargingParameters.ValuesChangeDiscretennesList;
        }
        #endregion

        #region Selected Parameters
        public string SelectedPortNameVM
        {
            set => _connectionParameters.SelectedPortName = value;
        }

        public string SelectedBaudRateVM
        {
            set => _connectionParameters.SelectedBaudRate = value;
        }

        public string SelectedDataBitsVM
        {
            set => _connectionParameters.SelectedDataBits = value;
        }

        public string SelectedParityVM
        {
            set => _connectionParameters.SelectedParity = value;
        }

        public string SelectedStopBitsVM
        {
            set => _connectionParameters.SelectedStopBits = value;
        }

        public string ValuesChangeDiscretennesVM
        {
            set => _dischargingParameters.ValuesChangeDiscretennes = value;
        }

        public string LowerVoltageThresholdVM
        {
            set => _dischargingParameters.LowerVoltageThreshold = value;
        }
        #endregion

        #region Commands
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
        #endregion

        private void CreateParameterDictionary()
        {
            _startParameters = new Dictionary<string, string>()
            {
                ["PortName"]    = _connectionParameters.SelectedPortName,
                ["BaudRate"]    = _connectionParameters.SelectedBaudRate,
                ["DataBits"]    = _connectionParameters.SelectedDataBits,
                ["Parity"]      = _connectionParameters.SelectedParity,
                ["StopBits"]    = _connectionParameters.SelectedStopBits,

                ["LowDischargeVoltage"] = _dischargingParameters.LowerVoltageThreshold,
                ["ValuesChangeDiscreteness"] = _dischargingParameters.ValuesChangeDiscretennes
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
