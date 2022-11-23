using System.Collections.Generic;

namespace SmartBatteryTesterDesktopApp.UI.Models
{
    public class ComPortConnectionParameters
    {
        #region Parameters
        internal List<string> PortNamesList { get; } = new List<string>() {   "COM1",
                                                                            "COM2",
                                                                            "COM3",
                                                                            "COM4",
                                                                            "COM5" };


        internal List<string> BaudRatesList { get; } = new List<string>() {   "2400",
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

        internal List<string> ParityList { get; } = new List<string>() {  "None",
                                                                        "Odd",
                                                                        "Even",
                                                                        "Mark",
                                                                        "Space" };

        internal List<string> DataBitsList { get; } = new List<string>() {    "5",
                                                                            "6",
                                                                            "7",
                                                                            "8",
                                                                            "9" };

        internal List<string> StopBitsList { get; } = new List<string>() {    "None",
                                                                            "One",
                                                                            "Two",
                                                                            "OnePointFive"};
        #endregion

        #region SelectedParameters
        internal string? SelectedPortName { get; set; }
        internal string? SelectedBaudRate { get; set; }
        internal string? SelectedParity { get; set; }
        internal string? SelectedDataBits { get; set; }
        internal string? SelectedStopBits { get; set; }
        #endregion
        internal string? ConnectionStatus { get; set; } = "Готов";
    }
}
