namespace IRL_Gui_Debugger.Communication.Commands
{
    public static class NavKeyCommand
    {
        public static IDeviceCommand NavKeyLeftPress()
        {
            return new EventCommand(GuiEvent.LeftPress);
        }

        public static IDeviceCommand NavKeyRightPress()
        {
            return new EventCommand(GuiEvent.RightPress);
        }

        public static IDeviceCommand NavKeyUpPress()
        {
            return new EventCommand(GuiEvent.UpPress);
        }

        public static IDeviceCommand NavKeyDownPress()
        {
            return new EventCommand(GuiEvent.DownPress);
        }

        public static IDeviceCommand NavKeyEnterPress()
        {
            return new EventCommand(GuiEvent.EnterPress);
        }

        public static IDeviceCommand NavKeyLeftRelease()
        {
            return new EventCommand(GuiEvent.LeftReleased);
        }

        public static IDeviceCommand NavKeyRightRelease()
        {
            return new EventCommand(GuiEvent.RightReleased);
        }

        public static IDeviceCommand NavKeyUpRelease()
        {
            return new EventCommand(GuiEvent.UpReleased);
        }

        public static IDeviceCommand NavKeyDownRelease()
        {
            return new EventCommand(GuiEvent.DownReleased);
        }

        public static IDeviceCommand NavKeyEnterRelease()
        {
            return new EventCommand(GuiEvent.EnterReleased);
        }
    }
}
