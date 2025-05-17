namespace IRL_Gui_Debugger.Communication.Commands
{
    public class SetRtcCommand
    {
        public static EventCommand GetCommand()
        {
            DateTime dateTime = DateTime.Now;

            EventCommand command = new(GuiEvent.DateTimeChanged);
            command.EventArgs = new byte[8];

            command.EventArgs[0] = (byte)(dateTime.Year - 2000);
            command.EventArgs[1] = (byte)dateTime.Month;
            command.EventArgs[2] = (byte)dateTime.Day;
            command.EventArgs[3] = (byte)dateTime.Hour;
            command.EventArgs[4] = (byte)dateTime.Minute;
            command.EventArgs[5] = (byte)dateTime.Second;
            command.EventArgs[6] = (byte)dateTime.DayOfWeek;

            return command;
        }
    }
}
