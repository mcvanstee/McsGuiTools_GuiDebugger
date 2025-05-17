namespace IRL_Gui_Debugger.Communication.GuiDebugProtocol
{
    public enum PacketError
    {
        None,
        UnKnown,
        SyncByte,
        Crc,
        PacketLength,
    }

    public class CommunicationPacket
    {
        public byte[] Data { get; } = Array.Empty<byte>();

        public PacketError Error { get; }

        public CommunicationPacket(PacketError error)
        {
            Error = error;
        }

        public CommunicationPacket(byte[] data)
        {
            Error = PacketError.None;
            Data = data;
        }

        public static CommunicationPacket CreatePacket(byte[] payload, PacketType packetType)
        {
            int packetDataLength = payload.Length + Protocol.HeaderLength;
            byte[] data = new byte[Protocol.PacketLength];
            data[0] = (byte)'I';
            data[1] = (byte)'R';
            data[2] = (byte)'L';
            data[3] = (byte)packetType;
            data[4] = (byte)payload.Length;

            Array.Copy(payload, 0, data, Protocol.StartPayloadIndex, payload.Length);
            uint crc = CRC32.GetCrc32(ref data, packetDataLength);
            Array.Copy(BitConverter.GetBytes(crc), 0, data, packetDataLength, sizeof(uint));

            return new CommunicationPacket(data);
        }

        public static RequestResponseType GetRequestResponseType(CommunicationPacket packet)
        {
            return (RequestResponseType)packet.Data[Protocol.StartPayloadIndex];
        }
    }
}
