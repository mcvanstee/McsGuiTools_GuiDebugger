namespace Gui_Debug_Tool.DisplayInstructions
{
    public class RectangleFillBorderInstruction : DisplayInstruction
    {
        public Color FillColor { get; private set; }
        public Color BorderColor { get; private set; }
        public byte BorderThickness { get; private set; }
        public byte Radius { get; private set; }

        public RectangleFillBorderInstruction(
            Point point, Size size, Color fillCollor, byte thickness, Color borderColor, byte radius) : base(point, size)
        {
            FillColor = fillCollor;
            BorderThickness = thickness;
            BorderColor = borderColor;
            Radius = radius;
        }
    }
}
