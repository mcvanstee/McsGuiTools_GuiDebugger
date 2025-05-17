using System.ComponentModel;

namespace IRL_Gui_Debugger.Communication.GuiDebugProtocol
{
    public enum IRLBaudRate : int
    {
        [Description("9600")]
        BaudRate9600 = 9600,
        [Description("19200")]
        BaudRate19200 = 19200,
        [Description("38400")]
        BaudRate38400 = 38400,
        [Description("57600")]
        BaudRate57600 = 57600,
        [Description("115200")]
        BaudRate115200 = 115200,
        [Description("230400")]
        BaudRate230400 = 230400,
        [Description("460800")]
        BaudRate460800 = 460800,
        [Description("921600")]
        BaudRate921600 = 921600
    }
}
