using IRL_Gui_Debugger.Communication.GuiDebugProtocol;

namespace IRL_Gui_Debugger.Communication.Commands
{
    public class ButtonSetupCommand : IDeviceCommand
    {
        public PacketType PacketType { get; }
        public int Index { get; }   

        public ButtonSetupCommand(int index)
        {
            PacketType = PacketType.CustomButtonSetup;
            Index = index;
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[1];
            bytes[0] = (byte)Index;
            
            return bytes;
        }
    }
}
