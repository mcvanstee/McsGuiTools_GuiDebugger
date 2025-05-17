using IRL_Gui_Debugger.Communication.GuiDebugProtocol;

namespace IRL_Gui_Debugger.Communication.Commands
{
    public class EventCommand : IDeviceCommand
    {
        public PacketType PacketType { get; }
        public byte EventValue { get; } 
        public byte[] EventArgs { get; set; } = Array.Empty<byte>();

        public EventCommand(byte guiEvent)
        {
            PacketType = PacketType.Event;
            EventValue = guiEvent;
        }

        public EventCommand(GuiEvent guiEvent)
        {
            PacketType = PacketType.Event;
            EventValue = (byte)guiEvent;
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[2 + EventArgs.Length];
            bytes[0] = EventValue;
            bytes[1] = (byte)EventArgs.Length;
            Array.Copy(EventArgs, 0, bytes, 2, EventArgs.Length);

            return bytes;
        }
    }
}
