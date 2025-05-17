namespace IRL_Gui_Debugger.Communication.Commands
{
    public enum GuiEvent : byte
    {
        None = 0,
        EnterPress,
        EnterPressed,
        EnterReleased,
        LeftPress,
        LeftPressed,
        LeftReleased,
        UpPress,
        UpPressed,
        UpReleased,
        RightPress,
        RightPressed,
        RightReleased,
        DownPress,
        DownPressed,
        DownReleased,
        TouchOnPressed,
        TouchPressed,
        TouchReleased,
        DateTimeChanged,
        TimeChanged,
        DateChanged,
        NavigateToHome,
    }
}
