using Gui_Debug_Tool.Communication;
using IRL_Gui_Debugger.Communication.Commands;
using IRL_Gui_Debugger.Communication.GuiDebugProtocol;
using IRL_Gui_Debugger.Logging;

namespace IRL_Gui_Debugger.Communication
{
    public class FileWriteHandler
    {
        private enum FileWriteState
        {
            Idle,
            WaitForStartOK,
            WriteData,
            Done,
        }

        private byte[] m_fileBytes = Array.Empty<byte>();
        private int m_fileIndex = 0;
        private int m_remainingData = 0;
        private uint m_fileCrc = 0;
        private FileWriteState m_fileWriteState = FileWriteState.Idle;

        public FileWriteHandler() { }

        public int FileBytesRemaining => m_fileBytes.Length - m_fileIndex;
        public int TotalFileBytes => m_fileBytes.Length;

        public bool OpenFile(string filePath)
        {
            bool result = true;

            try
            {
                BinaryReader binaryReader = new BinaryReader(File.Open(filePath, FileMode.Open));
                m_fileBytes = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
                binaryReader.Close();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error opening file: {ex.Message}");
                result = false;
            }

            return result;
        }

        public void CancelFileWrite()
        {
            byte[] data = new byte[Protocol.DataPacketLength];
            Array.Copy(BitConverter.GetBytes(0), 0, data, 0, 4);

            DeviceCommunication.OutPacketQueue.Enqueue(new CommunicationPacket(data));

            CloseFile();
        }

        private void CloseFile()
        {
            m_fileWriteState = FileWriteState.Idle;
            m_fileBytes = Array.Empty<byte>();
        }

        public bool StartFileWriteCommand(string fileName)
        {
            if (m_fileBytes.Length == 0)
            {
                Logger.Error("No file open");
            }
            else
            {
                m_fileWriteState = FileWriteState.WaitForStartOK;
                PacketHandler.CommandQueue.Enqueue(new WriteFileRequest((uint)m_fileBytes.Length, fileName));
            }

            return m_fileBytes.Length > 0;
        }

        public void RequestResponseReceived(CommunicationPacket packet)
        {
            RequestResponseType responseType = CommunicationPacket.GetRequestResponseType(packet);

            switch (m_fileWriteState)
            {
                case FileWriteState.WaitForStartOK:
                    if (responseType == RequestResponseType.OK)
                    {
                        m_fileWriteState = FileWriteState.WriteData;
                        WriteData(true);
                    }
                    else
                    {
                        Logger.Error("Error starting file write");
                        CloseFile();
                    }
                    break;
                case FileWriteState.WriteData:
                    if (responseType == RequestResponseType.OK)
                    {
                        WriteData(false);
                    }
                    else
                    {
                        Logger.Error("Error writing data");
                        CloseFile();
                    }
                    break;
                case FileWriteState.Done:
                    FinishFileWrite(packet);
                    break;
                default:
                    Logger.AddMessageToCommunicationLog("Unexpected response");
                    break;
            }
        }

        private void WriteData(bool start)
        {
            if (start)
            {
                m_fileIndex = 0;
                m_fileCrc = 0xFFFFFFFFU;
                m_remainingData = m_fileBytes.Length;
            }

            int payloadLength;

            if (m_remainingData < Protocol.DataPacketPayloadLength)
            {
                payloadLength = m_remainingData;
                m_remainingData = 0;
            }
            else
            {
                payloadLength = Protocol.DataPacketPayloadLength;
                m_remainingData -= Protocol.DataPacketPayloadLength;
            }

            byte[] payload = new byte[payloadLength];
            Array.Copy(m_fileBytes, m_fileIndex, payload, 0, payloadLength);
            m_fileIndex += payloadLength;
            m_fileCrc = CRC32.GetCrc32Accumulate(m_fileCrc, ref payload, payloadLength);

            byte[] data = new byte[Protocol.DataPacketLength];
            byte[] payloadLengthBytes = BitConverter.GetBytes(payloadLength);
            Array.Copy(payloadLengthBytes, 0, data, 0, 4);
            Array.Copy(payload, 0, data, 4, payloadLength);

            DeviceCommunication.OutPacketQueue.Enqueue(new CommunicationPacket(data));

            if (m_remainingData == 0)
            {
                m_fileWriteState = FileWriteState.Done;
            }  
        }

        private void FinishFileWrite(CommunicationPacket packet)
        {
            RequestResponseType responseType = CommunicationPacket.GetRequestResponseType(packet);
            int payloadLength = packet.Data[Protocol.PayloadLengthIndex];

            if ((responseType == RequestResponseType.Data) && (payloadLength >= 5))
            {
                uint crcFromDevice = BitConverter.ToUInt32(packet.Data, Protocol.StartPayloadIndex + 1);

                if (crcFromDevice == m_fileCrc)
                {
                    Logger.Message("File write successful");
                }
                else
                {
                    Logger.Error("CRC mismatch");
                }
            }
            else
            {
                Logger.Error("Error finishing file write");
            }

            CloseFile();
        }
    }
}
