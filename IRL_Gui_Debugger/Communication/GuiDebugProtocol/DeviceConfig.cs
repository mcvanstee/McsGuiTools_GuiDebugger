using System.Text;

namespace IRL_Gui_Debugger.Communication.GuiDebugProtocol
{
    public class DeviceConfig
    {
        public ushort Version { get; set; }
        public ushort DisplayWidth { get; set; }
        public ushort DisplayHeight { get; set; }
        public bool UseKeyNav { get; set; }
        public bool UseTouch { get; set; }
        public int CustomButtonsDefined { get; set; }
        public string Description { get; set; } = string.Empty;

        public static DeviceConfig GetDeviceConfigFromPacket(CommunicationPacket packet)
        {
            byte[] data = packet.Data;
            int payloadIndex = Protocol.StartPayloadIndex;
            int payloadLength = data[Protocol.PayloadLengthIndex];

            DeviceConfig deviceConfig = new();

            if (payloadLength >= 20)
            {
                deviceConfig.Version = BitConverter.ToUInt16(data, payloadIndex);
                deviceConfig.DisplayWidth = BitConverter.ToUInt16(data, payloadIndex + 2);
                deviceConfig.DisplayHeight = BitConverter.ToUInt16(data, payloadIndex + 4);
                deviceConfig.UseKeyNav = BitConverter.ToBoolean(data, payloadIndex + 6);
                deviceConfig.UseTouch = BitConverter.ToBoolean(data, payloadIndex + 7);
                deviceConfig.CustomButtonsDefined = data[payloadIndex + 8];

                byte[] descriptionBytes = new byte[128];
                Array.Copy(data, payloadIndex + 9, descriptionBytes, 0, descriptionBytes.Length);
                deviceConfig.Description = Encoding.ASCII.GetString(descriptionBytes);
            }

            return deviceConfig;
        }
    }
}
