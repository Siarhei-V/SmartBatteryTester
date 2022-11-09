using System.Collections.Generic;

namespace SmartBatteryTesterDesktopApp.UI.Models
{
    internal class ComPortConnectionParameters
    {
        #region Parameters
        public List<string> PortNamesList { get; } = new List<string>() {   "COM1",
                                                                            "COM2",
                                                                            "COM3",
                                                                            "COM4",
                                                                            "COM5" };


        public List<string> BaudRatesList { get; } = new List<string>() {   "2400",
                                                                            "4800",
                                                                            "9600",
                                                                            "14400",
                                                                            "19200",
                                                                            "28800",
                                                                            "38400",
                                                                            "57600",
                                                                            "76800",
                                                                            "115200",
                                                                            "230400",
                                                                            "250000" };

        public List<string> ParityList { get; } = new List<string>() {  "None",
                                                                        "Odd",
                                                                        "Even",
                                                                        "Mark",
                                                                        "Space" };

        public List<string> DataBitsList { get; } = new List<string>() {    "5",
                                                                            "6",
                                                                            "7",
                                                                            "8",
                                                                            "9" };

        public List<string> StopBitsList { get; } = new List<string>() {    "None",
                                                                            "One",
                                                                            "Two",
                                                                            "OnePointFive"};
        #endregion

        #region SelectedParameters
        public string? SelectedPortName { get; set; }
        public string? SelectedBaudRate { get; set; }
        public string? SelectedParity { get; set; }
        public string? SelectedDataBits { get; set; }
        public string? SelectedStopBits { get; set; }
        #endregion
    }
}
