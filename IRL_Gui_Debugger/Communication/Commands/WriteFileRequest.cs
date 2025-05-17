using IRL_Gui_Debugger.Communication.GuiDebugProtocol;
using System.Text;

namespace IRL_Gui_Debugger.Communication.Commands
{
    public class WriteFileRequest : IDeviceCommand
    {
        public PacketType PacketType => PacketType.Request;
        public RequestType RequestType => RequestType.WriteFileToDevice;

        public uint FileSize { get; }
        public string FileName { get; }

        public WriteFileRequest(uint fileSize, string fileName)
        {
            FileSize = fileSize;
            FileName = fileName;
        }

        public byte[] GetBytes()
        {
            byte[] fileNameBytes = Encoding.ASCII.GetBytes(FileName);
            int payloadLength = 1 + sizeof(uint) + fileNameBytes.Length;

            byte[] bytes = new byte[payloadLength];
            bytes[0] = (byte)RequestType.WriteFileToDevice;
            Array.Copy(BitConverter.GetBytes(FileSize), 0, bytes, 1, sizeof(uint));
            Array.Copy(fileNameBytes, 0, bytes, 5, fileNameBytes.Length);

            return bytes;
        }
    }
}
