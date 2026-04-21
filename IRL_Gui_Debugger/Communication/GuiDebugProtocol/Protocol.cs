namespace IRL_Gui_Debugger.Communication.GuiDebugProtocol
{
    // Packet structure
    // [0] [1] [2]      [3]           [4]        [5] ... [PayloadLength + 4] [CRC] [CRC] [CRC] [CRC]  
    // 'I' 'R' 'L'   PacketType   payload length    

    public enum PacketType : byte
    {
        Request = 0,
        RequestResponse,
        Config,
        Event,
        ScreenUpdate,
        LogMessage,
        CustomButtonSetup,
        SyncRtcTime,
        Error,
    }

    public enum GraphicsInstructionType : byte
    {
        ImageInstruction = 1,
        FillInstruction = 2,
    }

    public static class Protocol
    {
        public const int PacketLength = 64;
        public const int PacketTypeIndex = 3;
        public const int PayloadLengthIndex = 4;
        public const int StartPayloadIndex = 6;

        public const int HeaderLength = 6;
        public const int CrcLength = 4;

        public const int DataPacketLength = 512; // For File write/read
        public const int DataPacketPayloadLength = DataPacketLength - 4; // For File write/read
    }

    public enum RequestType : byte
    {
        WriteFileToDevice = 0,
        ReadFileFromDevice,
    }

    public enum RequestResponseType : byte
    {
        OK = 0,
        Data,
        Error,
    }
}
