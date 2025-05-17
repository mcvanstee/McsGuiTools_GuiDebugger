using Gui_Debug_Tool.Communication;
using Gui_Debug_Tool.DisplayInstructions;
using Gui_Debug_Tool.DisplaySimulator;
using IRL_Gui_Debugger.Communication.Commands;
using IRL_Gui_Debugger.Communication.GuiDebugProtocol;
using IRL_Gui_Debugger.DisplayInstructions;
using IRL_Gui_Debugger.Forms;
using IRL_Gui_Debugger.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace IRL_Gui_Debugger.Communication
{
    public static class PacketHandler
    {
        public static ConcurrentQueue<IDeviceCommand> CommandQueue { get; } = new ConcurrentQueue<IDeviceCommand>();
        private static bool s_waitingForResponse = false;
        private static List<RequestType> s_requestWaitingList = new();
    
        public static void ProcessPackets(DisplayGraphics displayGraphics)
        {
            if (DeviceCommunication.InPacketQueue.TryDequeue(out CommunicationPacket? packet))
            {
                if (packet.Error == PacketError.None)
                {
                    HandlePacketReceived(packet, displayGraphics);
                }
                else
                {
                    HandlePacketError(packet);
                }
            }

            if (CommandQueue.TryDequeue(out IDeviceCommand? command))
            {
                SendCommand(command);
            }
        }

        private static void HandlePacketReceived(CommunicationPacket packet, DisplayGraphics displayGraphics)
        {
            PacketType packetType = (PacketType)packet.Data[Protocol.PacketTypeIndex];

            if (packetType == PacketType.ScreenUpdate)
            {
                HandleScreenUpdatePacketReceived(packet, displayGraphics);
            }
            else if (packetType == PacketType.LogMessage)
            {
                LogMessageReceived(packet);
            }
            else if (packetType == PacketType.Config)
            {
                ConfigDataReceived(packet);
                Logger.AddMessageToCommunicationLog("Configuration received");
            }
            else if (packetType == PacketType.CustomButtonSetup)
            {
                CustomButtonSetupReceived(packet);
            }
            else if (packetType == PacketType.SyncRtcTime)
            {
                CommandQueue.Enqueue(SetRtcCommand.GetCommand());
                Logger.AddMessageToCommunicationLog("Sync RTC request");
                Thread.Sleep(500);
            }
            else if (packetType == PacketType.RequestResponse)
            {
                s_waitingForResponse = false;
                MainWindow.Instance.Invoke((MethodInvoker)delegate
                {
                    MainWindow.Instance.RequestResponseReceived(packet);
                });
            }
            else
            {
                Logger.AddMessageToCommunicationLog("ERROR - Packet type");
            }
        }

        private static void HandlePacketError(CommunicationPacket packet)
        {
            switch (packet.Error)
            {
                case PacketError.UnKnown:
                    Logger.AddMessageToCommunicationLog("ERROR Communication Unknown ");
                    break;
                case PacketError.SyncByte:
                    Logger.AddMessageToCommunicationLog("ERROR Communication Sybc bytes");
                    break;
                case PacketError.Crc:
                    Logger.AddMessageToCommunicationLog("ERROR Communication CRC");
                    break;
                case PacketError.PacketLength:
                    Logger.AddMessageToCommunicationLog("ERROR Communication Packet length");
                    break;
                case PacketError.None:
                default:
                    break;
            }
        }

        private static void HandleScreenUpdatePacketReceived(CommunicationPacket packet, DisplayGraphics displayGraphics)
        {
            int dataToRead = packet.Data[Protocol.PayloadLengthIndex];
            Queue<DisplayInstruction> instructionsQueue = new();

            for (int i = Protocol.StartPayloadIndex; i < dataToRead; i += Protocol.InstructionLength)
            {
                byte[] instructionBytes = new byte[Protocol.InstructionLength];
                Array.Copy(packet.Data, i, instructionBytes, 0, Protocol.InstructionLength);

                if ((GraphicsInstructionType)packet.Data[i] == GraphicsInstructionType.FillInstruction)
                {
                    DisplayInstruction instruction = DisplayInstruction.GetRectangleInstruction(instructionBytes);
                    if (instruction.GetType() != typeof(EmptyInstruction))
                    {
                        instructionsQueue.Enqueue(instruction);
                    }
                }
                else if ((GraphicsInstructionType)packet.Data[i] == GraphicsInstructionType.ImageInstruction)
                {
                    DisplayInstruction instruction = ImageInstruction.GetInstruction(instructionBytes);                   
                    instructionsQueue.Enqueue(instruction);
                }
                else
                {
                    Debug.WriteLine("Grapics Instruction error");
                }
            }

            bool screenUpdate = (instructionsQueue.Count > 0);

            displayGraphics.ProcessDisplayInstructions(instructionsQueue);

            if (screenUpdate)
            {
                displayGraphics.QueueBitmapToCache();
            }
        }

        private static void ConfigDataReceived(CommunicationPacket packet)
        {
            DeviceConfig deviceConfig = DeviceConfig.GetDeviceConfigFromPacket(packet);

            MainWindow.Instance.Invoke((MethodInvoker)delegate 
            {
                MainWindow.Instance.DeviceConfigRecieved(deviceConfig);
            });
        }

        private static void CustomButtonSetupReceived(CommunicationPacket packet)
        {
            ButtonSetup buttonSetup = ButtonSetup.GetButtonSetup(packet);

            MainWindow.Instance.Invoke((MethodInvoker)delegate
            {
                MainWindow.Instance.ButtonSetupReceived(buttonSetup);
            });
        }

        private static void LogMessageReceived(CommunicationPacket packet)
        {
            int payloadLength = packet.Data[Protocol.PayloadLengthIndex];
            byte[] messageBytes = new byte[payloadLength];
            Array.Copy(packet.Data, Protocol.StartPayloadIndex, messageBytes, 0, payloadLength);

            string message = Encoding.ASCII.GetString(messageBytes);
            message = message.Replace("\n", "");
            Logger.AddMessageToDeviceLog(message);
        }

        private static void SendCommand(IDeviceCommand command)
        { 
            if (command.PacketType == PacketType.Request)
            {
                s_waitingForResponse = true;
            }

            byte[] payload = command.GetBytes();
            DeviceCommunication.OutPacketQueue.Enqueue(CommunicationPacket.CreatePacket(payload, command.PacketType));
        }
    }
}
