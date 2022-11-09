﻿using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.USART.Interfaces;
using System.IO.Ports;

namespace SmartBatteryTesterDesktopApp.USART
{
    internal class UsartDataHanlder : IUsartDataHandler
    {
        IPortDataTransmitter _portDataTransmitter;
        IPortDataHandler _portDataHandler;
        IUsartDataConverter _dataConverter;
        string _dataFromUsart;
        bool _isFirstDataReceived = true;
        SerialPort _port;

        internal UsartDataHanlder(IPortDataTransmitter portDataTransmitter, IPortDataHandler portDataHandler,
            IUsartDataConverter usartDataConverter, SerialPort serialPort)
        {
            _port = serialPort;
            _dataConverter = usartDataConverter;

            _portDataTransmitter = portDataTransmitter;
            _port.DataReceived += HandleDataFromUsart;

            _portDataHandler = portDataHandler;
            _portDataHandler.PortTransmitter = _portDataTransmitter;
        }

        #region Private Methods
        private void HandleDataFromUsart(object? sender, EventArgs e)
        {
            string lowerVoltageThreshold;
            string valuesChangeDiscretennes;

            if (_isFirstDataReceived)
            {
                _dataFromUsart = _dataConverter.ConvertDataFromUsart(sender);

                lowerVoltageThreshold = _portDataTransmitter.Parameters["LowDischargeVoltage"];
                valuesChangeDiscretennes = _portDataTransmitter.Parameters["ValuesChangeDiscreteness"];

                _portDataHandler.HandleStartValues(lowerVoltageThreshold, _dataFromUsart, valuesChangeDiscretennes);
                _isFirstDataReceived = false;
            }

            try
            {
                _dataFromUsart = _dataConverter.ConvertDataFromUsart(sender);
                _portDataHandler.HandleIntermediateValues(_dataFromUsart, "10", DateTime.Now); 
            }
            catch (Exception)
            {
                // TODO: handle exceptions
            }
        }
        #endregion
    }
}
