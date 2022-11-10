using SmartBatteryTesterDesktopApp.Commands;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.UI.Infrastructure;
using SmartBatteryTesterDesktopApp.UI.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

namespace SmartBatteryTesterDesktopApp.ViewModels
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        ComPortConnectionParameters _connectionParameters;
        DischargingParameters _dischargingParameters;
        IPortInteractor _portInteractor;
        Dictionary<string, string> _startParameters;
        INotifyDataChanged _onDataChanged;

        public MainWindowVM(ComPortConnectionParameters parameters, DischargingParameters dischargingParameters,
            IPortInteractor portInteractor, INotifyDataChanged onDataChanged)
        {
            _connectionParameters = parameters;
            _dischargingParameters = dischargingParameters;
            _portInteractor = portInteractor;
            _onDataChanged = onDataChanged;
            _onDataChanged.DataChanged += (s, e) =>
            {
                try
                {
                    VoltageVM = ((MeasurementEventArgs)e).Voltage;
                    if (((MeasurementEventArgs)e).IsDischargingStarted)
                    {
                        ConnectionStatusMessageVM = "Связь установлена";
                    }
                    else
                    {
                        ConnectionStatusMessageVM = "Порт закрыт";
                    }
                }
                catch (System.Exception ex)
                {
                    ConnectionStatusMessageVM = "Ошибка связи";
                    MessageBox.Show(ex.Message);
                }
            };
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

        public string VoltageVM
        {
            get => _dischargingParameters.Voltage;
            set
            {
                _dischargingParameters.Voltage = value;
                OnPropertyChanged();
            }
        }

        public string ConnectionStatusMessageVM
        {
            get => _connectionParameters.ConnectionStatus;
            set
            {
                _connectionParameters.ConnectionStatus = value;
                OnPropertyChanged();
            }
        }

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

                        try
                        {
                            _portInteractor.StartDischarging(_startParameters);
                        }
                        catch (System.Exception e)
                        {
                            MessageBox.Show($"Ахтунг: {e.Message}");
                        }
                    }));
            }
        }

        private RelayCommand _disconnectCommand;
        public RelayCommand DisconnectCommand
        {
            get
            {
                return _disconnectCommand ??
                    (_disconnectCommand = new RelayCommand(obj =>
                    {
                        VoltageVM = string.Empty;
                        _portInteractor.StopDischarging();
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
