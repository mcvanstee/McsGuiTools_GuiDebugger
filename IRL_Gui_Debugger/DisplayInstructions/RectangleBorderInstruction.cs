namespace Gui_Debug_Tool.DisplayInstructions
{
    public class RectangleBorderInstruction : DisplayInstruction
    {
        public byte BorderThickness { get; private set; }
        public Color BorderColor { get; private set; }
        public byte Radius { get; private set; }

        public RectangleBorderInstruction(Point point, Size size, byte thickness, Color borderColor, byte radius) : base(point, size)
        {
            BorderThickness = thickness;
            BorderColor = borderColor;
            Radius = radius;
        }
    }
}
