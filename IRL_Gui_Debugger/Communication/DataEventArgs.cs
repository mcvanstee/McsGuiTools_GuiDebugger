using IRL_Gui_Debugger.Communication.GuiDebugProtocol;

namespace Gui_Debug_Tool.Communication
{
    public class DataEventArgs : EventArgs
    {
        //public byte[] Data { get; }
        public CommunicationPacket CommunicationPacket { get; }

        public DataEventArgs(CommunicationPacket communicationPacket)
        {
            CommunicationPacket = communicationPacket;
        }

        //public DataEventArgs(byte[] data)
        //{
        //    Data = data;
        //}
    }
}

