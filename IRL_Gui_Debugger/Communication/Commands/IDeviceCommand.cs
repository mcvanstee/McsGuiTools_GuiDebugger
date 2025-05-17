using IRL_Gui_Debugger.Communication.GuiDebugProtocol;

namespace IRL_Gui_Debugger.Communication.Commands
{
    public interface IDeviceCommand
    {
        public PacketType PacketType { get; }
        public byte[] GetBytes();
    }
}
