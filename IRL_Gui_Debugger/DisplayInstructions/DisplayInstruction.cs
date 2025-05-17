using IRL_Gui_Debugger.DisplayInstructions;
using IRL_Gui_Debugger.Logging;

namespace Gui_Debug_Tool.DisplayInstructions
{
    public abstract class DisplayInstruction
    {
        public Point Point { get; private set; }
        public Size Size { get; private set; }

        protected DisplayInstruction(Point point, Size size) 
        {
            Point = point;
            Size = size;
        }

        protected static Color ConvertColor(uint color)
        {
            byte r = (byte)((color & 0xFF0000) >> 16);
            byte g = (byte)((color & 0xFF00) >> 8);
            byte b = (byte)((color & 0xFF) >> 0);

            return Color.FromArgb(r, g, b);
        }

        protected static Point GetPoint(byte[] instructionBytes)
        {
            ushort x = BitConverter.ToUInt16(instructionBytes, 12);
            ushort y = BitConverter.ToUInt16(instructionBytes, 14);

            return new Point(x, y);
        }

        protected static Size GetSize(byte[] instructionBytes)
        {
            ushort width = BitConverter.ToUInt16(instructionBytes, 16);
            ushort height = BitConverter.ToUInt16(instructionBytes, 18);

            return new Size(width, height); ;
        }

        public static DisplayInstruction GetRectangleInstruction(byte[] instructionBytes)
        {
            Point point = GetPoint(instructionBytes);
            Size size = GetSize(instructionBytes);
            uint fillColor = BitConverter.ToUInt32(instructionBytes, 1);
            uint borderColor = BitConverter.ToUInt32(instructionBytes, 5);
            bool fillBackground = BitConverter.ToBoolean(instructionBytes, 9);
            byte borderThickness = instructionBytes[10];
            byte radius = instructionBytes[11];

            bool hasBorder = borderThickness > 0;

            if (fillBackground && !hasBorder)
            {
                return new RectangleFillInstruction(point, size, ConvertColor(fillColor), radius);
            }
            else if (fillBackground && hasBorder)
            {
                return new RectangleFillBorderInstruction(
                    point, size, ConvertColor(fillColor), borderThickness, ConvertColor(borderColor), radius);
            }
            else if (!fillBackground && hasBorder)
            {
                return new RectangleBorderInstruction(point, size, borderThickness, ConvertColor(borderColor), radius);
            }
            else
            {
                Logger.Error("Can not draw Fill instrcution");

                return new EmptyInstruction();
            }
        }
    }
}
