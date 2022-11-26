using SmartBatteryTesterDesktopApp.PORT;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.UI.Commands;
using SmartBatteryTesterDesktopApp.UI.Infrastructure;
using SmartBatteryTesterDesktopApp.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartBatteryTesterDesktopApp.UI.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        ComPortConnectionParameters _connectionParameters;
        DischargingParameters _dischargingParameters;
        IUiInteractorInputPort _portInteractor;
        IDataGetter _dataGetter;
        Dictionary<string, string> _startParameters;
        bool _isOnlineModeEnabled = true;

        public MainWindowVM(IUiInteractorInputPort portInteractor, ComPortConnectionParameters connectionparameters, 
            DischargingParameters dischargingParameters)
        {
            _connectionParameters = connectionparameters;
            _dischargingParameters = dischargingParameters;

            _dataGetter = new PortDataGetter(dischargingParameters);    // TODO: inject this using ninject?

            _portInteractor = portInteractor;
            _portInteractor.DataSender = _dataGetter;

            _dataGetter.DataChanged += HandleDataChangedEvent;
        }

        #region Parameters
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

        public string SelectedDischargingCurrent
        {
            get => _dischargingParameters.Current;
            set
            {
                _dischargingParameters.Current = value;
                OnPropertyChanged();
            }
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

        public string PortConnectionStatusMessageVM
        {
            get => _dischargingParameters.PortConnectionStatus;
            set
            {
                _dischargingParameters.PortConnectionStatus = value;
                OnPropertyChanged();
            }
        }

        public string WebConnectionStatusMessageVM
        {
            get => _dischargingParameters.WebConnectionStatus;
            set
            {
                _dischargingParameters.WebConnectionStatus = value;
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
                        PortConnectionStatusMessageVM = "Попытка подключения";

                        try
                        {
                            _portInteractor.SetDischargingParams(_dischargingParameters.LowerVoltageThreshold,
                                _dischargingParameters.ValuesChangeDiscretennes, 
                                _dischargingParameters.Current);
                            _portInteractor.ConnectToPort(_startParameters);
                        }
                        catch (Exception e)
                        {
                            PortConnectionStatusMessageVM = e.Message;
                        }
                    }));
            }
        }

        private RelayCommand _startDischargingCommand;
        public RelayCommand StartDischargingCommand
        {
            get
            {
                return _startDischargingCommand ??
                    (_startDischargingCommand = new RelayCommand(obj =>
                    {
                        _portInteractor.StartDischarging(_isOnlineModeEnabled);
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
                        PortConnectionStatusMessageVM = "Порт закрыт";
                    }));
            }
        }

        private RelayCommand _disconnectWebCommand;
        public RelayCommand DisconnectWebCommand
        {
            get
            {
                return _disconnectWebCommand ??
                    (_disconnectWebCommand = new RelayCommand(obj =>
                    {
                        _portInteractor.DisconnectWeb();
                    }));
            }
        }

        private RelayCommand _switchOfflineModeCommand;
        public RelayCommand SwitchOfflineModeCommand
        {
            get
            {
                return _switchOfflineModeCommand ??
                    (_switchOfflineModeCommand = new RelayCommand(obj =>
                    {
                        _isOnlineModeEnabled = _isOnlineModeEnabled ? false : true;
                    }));
            }
        }
        #endregion

        #region Private Methods
        void HandleDataChangedEvent(object? sender, EventArgs args)
        {
            WebConnectionStatusMessageVM = _dischargingParameters.WebConnectionStatus;
        }

        private void CreateParameterDictionary()
        {
            _startParameters = new Dictionary<string, string>()
            {
                ["PortName"] = _connectionParameters.SelectedPortName,
                ["BaudRate"] = _connectionParameters.SelectedBaudRate,
                ["DataBits"] = _connectionParameters.SelectedDataBits,
                ["Parity"] = _connectionParameters.SelectedParity,
                ["StopBits"] = _connectionParameters.SelectedStopBits,

                ["TempDischargingCurrent"] = _dischargingParameters.Current,    // TODO: this is a temporary parameter
                ["Mode"] = _isOnlineModeEnabled ? "OnlineMode" : "OfflineMode"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        #endregion

    }
}
