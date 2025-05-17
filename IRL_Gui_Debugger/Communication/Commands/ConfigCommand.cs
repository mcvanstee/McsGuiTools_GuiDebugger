using IRL_Gui_Debugger.Communication.GuiDebugProtocol;

namespace IRL_Gui_Debugger.Communication.Commands
{
    public class ConfigCommand : IDeviceCommand
    {
        public PacketType PacketType { get; }

        public ConfigCommand()
        {
            PacketType = PacketType.Config;
        }

        public byte[] GetBytes()
        {
            return Array.Empty<byte>();
        }
    }
}
