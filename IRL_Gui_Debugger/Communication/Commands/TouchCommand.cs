namespace IRL_Gui_Debugger.Communication.Commands
{
    public static class TouchCommand
    {
        public static IDeviceCommand TouchOnPressed(Point point)
        {
            return TouchOnPressed(point.X, point.Y);
        }

        public static IDeviceCommand TouchOnPressed(int x, int y)
        {
            return GetTouchCommand(x, y, GuiEvent.TouchOnPressed);
        }

        public static IDeviceCommand TouchPressed(Point point)
        {
            return TouchPressed(point.X, point.Y);
        }

        public static IDeviceCommand TouchPressed(int x, int y)
        {
            return GetTouchCommand(x, y, GuiEvent.TouchOnPressed);
        }

        public static IDeviceCommand TouchOnReleased(Point point)
        {
            return TouchOnReleased(point.X, point.Y);
        }

        public static IDeviceCommand TouchOnReleased(int x, int y)
        {
            return GetTouchCommand(x, y, GuiEvent.TouchReleased);
        }

        private static EventCommand GetTouchCommand(int x, int y, GuiEvent guiEvent)
        {
            EventCommand command = new(guiEvent);
            ushort xPos = (ushort)x;
            ushort yPos = (ushort)y;

            command.EventArgs = new byte[4];
            Array.Copy(BitConverter.GetBytes(xPos), 0, command.EventArgs, 0, 2);
            Array.Copy(BitConverter.GetBytes(yPos), 0, command.EventArgs, 2, 2);

            return command;
        }
    }
}
