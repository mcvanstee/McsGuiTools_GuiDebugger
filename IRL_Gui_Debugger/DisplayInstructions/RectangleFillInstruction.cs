namespace Gui_Debug_Tool.DisplayInstructions
{
    public class RectangleFillInstruction : DisplayInstruction
    {
        public Color Color { get; set; }
        public byte Radius { get; private set; }

        public RectangleFillInstruction(Point point, Size size, Color fillCollor, byte radius) : base(point, size)
        { 
            Color = fillCollor;
            Radius = radius;
        }
    }
}
