using Gui_Debug_Tool.DisplayInstructions;

namespace IRL_Gui_Debugger.DisplayInstructions
{
    public enum ImageType : byte
    {
        Bitmap = 0,
        Font
    }

    public class OptimizedImageInstruction : DisplayInstruction
    {
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public byte DataLocationId { get; set; }
        public ImageType Type { get; set; } = ImageType.Bitmap;
        public uint BmpKey { get; set; }
        public byte Character { get; set; }
        public byte FontId { get; set; }
        public byte[] Properties { get; set; } = [];

        public OptimizedImageInstruction(Point point, Size size) : base(point, size)
        {
        }

        public static OptimizedImageInstruction GetInstruction(
            byte[] instructionBytes, bool useBitmapColor, int propertiesLength)
        {
            Point point = GetPoint(instructionBytes);
            Size size = GetSize(instructionBytes);

            OptimizedImageInstruction instruction = new(point, size);

            int index = 5;

            if (useBitmapColor)
            {
                instruction.ForeColor = ConvertColor(BitConverter.ToUInt32(instructionBytes, index));
                index += 4;
                instruction.BackColor = ConvertColor(BitConverter.ToUInt32(instructionBytes, index));
                index += 4;
            }

            instruction.DataLocationId = instructionBytes[index++];
            instruction.Type = (ImageType)instructionBytes[index++];

            if (instruction.Type == ImageType.Bitmap)
            {
                instruction.BmpKey = BitConverter.ToUInt32(instructionBytes, index);
                index += 4;
            }
            else
            {
                instruction.FontId = instructionBytes[index++];
                instruction.Character = instructionBytes[index++];
                index += 2; // Skip 2 bytes for Bitmap key which is not used for Font type
            }

            if (propertiesLength > 0)
            {
                instruction.Properties = new byte[propertiesLength];

                for (int i = 0; i < propertiesLength; i++)
                {
                    instruction.Properties[i] = instructionBytes[index++];
                }
            }

            return instruction;
        }
    }
}
