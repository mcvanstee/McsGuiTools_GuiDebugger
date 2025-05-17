using Gui_Debug_Tool.DisplayInstructions;

namespace IRL_Gui_Debugger.DisplayInstructions
{
    public class EmptyInstruction : DisplayInstruction
    {
        public EmptyInstruction() : base(new Point(), new Size())
        { 
        }
    }
}
