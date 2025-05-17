using System.Text;

namespace IRL_Gui_Debugger.Communication.GuiDebugProtocol
{
    public class ButtonSetup
    {
        public int Id { get; private set; }
        public int GuiEvent { get; private set; }
        public string Description { get; private set; }

        public ButtonSetup(int id, int guiEvent, string description)
        {
            Id = id;
            GuiEvent = guiEvent;
            Description = description;
        }

        public static ButtonSetup GetButtonSetup(CommunicationPacket packet)
        {
            byte[] data = packet.Data;
            int payloadIndex = Protocol.StartPayloadIndex;

            int id = data[payloadIndex];
            int guiEvent = data[payloadIndex + 1];

            int stringLength = 0;
            for (int i = payloadIndex + 2; i < data.Length; i++)
            {
                if (data[i] == 0)
                {
                    break;
                }
                else
                {
                    stringLength++;
                }
            }

            byte[] descriptionBytes = new byte[stringLength];
            Array.Copy(data, payloadIndex + 2, descriptionBytes, 0, descriptionBytes.Length);
            string description = Encoding.UTF8.GetString(descriptionBytes);

            return new ButtonSetup(id, guiEvent, description);
        }
    }
}
