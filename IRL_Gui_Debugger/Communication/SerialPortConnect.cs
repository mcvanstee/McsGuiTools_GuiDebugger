using IRL_Gui_Debugger.Communication.GuiDebugProtocol;
using System.Diagnostics;
using System.IO.Ports;
using System.Management;

namespace IRL_Gui_Debugger.Communication
{
    public class SerialPortConnect
    {
        public static SerialPort OpenNewSerialPort(string portName, IRLBaudRate baudRate)
        {
            SerialPort serialPort;

            if (!string.IsNullOrEmpty(portName))
            {
                serialPort = new(portName)
                {
                    BaudRate = (int)baudRate,
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None
                };

                try
                {
                    serialPort.Open();
                    serialPort.DiscardInBuffer();
                    serialPort.DiscardOutBuffer();
                }
                catch { }
            }
            else
            {
                serialPort = new();
            }

            return serialPort;
        }

        public async static Task<string[]> GetAvailablePorts()
        {
            List<string> comPortDescriptions = new();
            List<string> comPortNames = new();
            string[] portNames = Array.Empty<string>();

            await Task.Run(() =>
            {
                string[] portNames = SerialPort.GetPortNames();

                using ManagementObjectSearcher searcher = new("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'");
                
                try
                {
                    var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());
                    comPortDescriptions = portNames.Select(n => n + " - " + ports.FirstOrDefault(s => s.Contains(n))?.Replace(" (" + n + ")", "")).ToList();
                    comPortNames = portNames.Select(n => ports.FirstOrDefault(s => s.Contains(n))?.Replace(" (" + n + ")", "")).ToList();
                }
                catch { }                
            });

            if (comPortDescriptions.Count > 0)
            {
                return comPortDescriptions.ToArray();
            }
            else
            {
                return portNames;
            }
        }

        public static void AutoConnect(string portName)
        {

        }
    }
}
