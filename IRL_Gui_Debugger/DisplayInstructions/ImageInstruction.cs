namespace Gui_Debug_Tool.DisplayInstructions
{
    public class ImageInstruction : DisplayInstruction
    {
        public uint DataOffset { get; set; }
        public uint DataSize { get; set; }

        public ImageInstruction(Point point, Size size, uint dataOffset, uint dataSize) : base(point, size)
        {
            DataOffset = dataOffset;
            DataSize = dataSize;
        }

        public static ImageInstruction GetInstruction(byte[] instructionBytes)
        {
            Point point = GetPoint(instructionBytes);
            Size size = GetSize(instructionBytes);
            uint dataOffset = BitConverter.ToUInt32(instructionBytes, 1);
            uint dataSize = BitConverter.ToUInt32(instructionBytes, 5);

            return new ImageInstruction(point, size, dataOffset, dataSize);
        }
    }
}
