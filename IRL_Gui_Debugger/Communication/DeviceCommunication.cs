using IRL_Gui_Debugger.Communication.GuiDebugProtocol;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO.Ports;

namespace Gui_Debug_Tool.Communication
{
    public enum CommunicationResult
    {
        None,
        ComPortClosed,
        ReadTimeout,
        WriteTimeout,
        UnKnown,
    }

    public delegate void CommuncationErrorCallbackDelegate(CommunicationResult result);

    public class DeviceCommunication
    {
        public static ConcurrentQueue<CommunicationPacket> InPacketQueue { get; set; } = new();
        public static ConcurrentQueue<CommunicationPacket> OutPacketQueue { get; set; } = new();
        
        private static bool s_stop = false;
        private readonly SerialPort m_serialPort;
        private CommuncationErrorCallbackDelegate m_errorCallback;

        private int m_bytesToRead = 0;
        private List<byte> m_bytesRead = new();

        public DeviceCommunication(SerialPort serialPort, CommuncationErrorCallbackDelegate errorCallback)
        {
            m_serialPort = serialPort;
            m_serialPort.ReadTimeout = 500;
            m_serialPort.WriteTimeout = 500;

            m_errorCallback = errorCallback;
        }

        public void StartDeviceCommunication()
        {
            s_stop = false;
            OutPacketQueue.Clear();
            InPacketQueue.Clear();

            CommunicationResult error = CommunicationResult.None;

            while (!s_stop)
            {
                if (!m_serialPort.IsOpen)
                {
                    error = CommunicationResult.ComPortClosed;

                    break;
                }

                if (OutPacketQueue.TryDequeue(out CommunicationPacket? packet))
                {
                    if (packet.Data.Length > 0)
                    {
                        try
                        {
                            m_serialPort.DiscardOutBuffer();
                            m_serialPort.Write(packet.Data, 0, packet.Data.Length);
                            m_serialPort.BaseStream.Flush();
                        }
                        catch (TimeoutException)
                        {
                            error = CommunicationResult.WriteTimeout;

                            break;
                        }
                    }
                }

                try 
                { 
                    if (m_serialPort.BytesToRead > 0)
                    {
                        //Debug.WriteLine($"Bytes to read: {m_serialPort.BytesToRead}");

                        int byteReadResult = m_serialPort.ReadByte();

                        if (byteReadResult >= 0)
                        {
                            ProcessByteRead((byte)byteReadResult);
                        }
                    }
                }
                catch (TimeoutException)
                {
                    error = CommunicationResult.ReadTimeout;

                    break;
                }
                catch
                {
                    error = CommunicationResult.UnKnown;

                    break;
                }               
            }

            if (!s_stop)
            {
                if (error == CommunicationResult.UnKnown && !m_serialPort.IsOpen)
                {
                    error = CommunicationResult.ComPortClosed;
                }

                m_errorCallback?.Invoke(error);
            }
        }

        public static void Stop()
        {
            s_stop = true;
        }

        private void ProcessByteRead(byte byteRead)
        {
            if (m_bytesRead.Count >= Protocol.HeaderLength)
            {
                m_bytesRead.Add(byteRead);
                m_bytesToRead--;

                if (m_bytesToRead == 0)
                {
                    byte[] packet = m_bytesRead.ToArray();
                    m_bytesRead.Clear();
                    //m_serialPort.DiscardInBuffer();

                    bool crcResult = CheckCrc(ref packet);
                    if (crcResult)
                    {
                        InPacketQueue.Enqueue(new CommunicationPacket(packet));
                    }
                    else
                    {
                        InPacketQueue.Enqueue(new CommunicationPacket(PacketError.Crc));
                    }
                }
            }
            else if (m_bytesRead.Count == 0)
            {
                if (byteRead == 'I')
                {
                    m_bytesRead.Add(byteRead);
                }
                else
                {
                    //m_bytesRead.Clear();
                    //m_bytesToRead = 0;
                    //m_serialPort.DiscardInBuffer();
                    //InPacketQueue.Enqueue(new CommunicationPacket(PacketError.SyncByte));
                }
            }
            else if (m_bytesRead.Count == 1)
            {
                if (byteRead == 'R')
                {
                    m_bytesRead.Add(byteRead);
                }
                else
                {
                    InPacketQueue.Enqueue(new CommunicationPacket(PacketError.SyncByte));
                    //m_bytesRead.Clear();
                    //m_bytesToRead = 0;
                    //m_serialPort.DiscardInBuffer();
                }
            }
            else if (m_bytesRead.Count == 2)
            {
                if (byteRead == 'L')
                {
                    m_bytesRead.Add(byteRead);
                }
                else
                {
                    InPacketQueue.Enqueue(new CommunicationPacket(PacketError.SyncByte));
                    //m_bytesRead.Clear();
                    //m_bytesToRead = 0;
                    //m_serialPort.DiscardInBuffer();
                }
            }
            else if (m_bytesRead.Count == 3)
            {
                m_bytesRead.Add(byteRead);
            }
            else if (m_bytesRead.Count == 4)
            {
                m_bytesRead.Add(byteRead);
                m_bytesToRead = byteRead + Protocol.CrcLength;
            }
            else
            {
                InPacketQueue.Enqueue(new CommunicationPacket(PacketError.UnKnown));
                m_bytesToRead = 0;
                //m_bytesRead.Clear();
                //m_serialPort.DiscardInBuffer();
            }
        }

        private static bool CheckCrc(ref byte[] packet)
        {
            int length = packet[Protocol.PayloadLengthIndex] + Protocol.HeaderLength;
            uint crcCalculated = CRC32.GetCrc32(ref packet, length);
            uint crcFromPacket = BitConverter.ToUInt32(packet, length);

            return crcCalculated == crcFromPacket;
        }


        //private void ProcesDataRecevied(object sender, DataEventArgs e)
        //{
        //    if (e.CommunicationPacket.DataLength >= 4 && IsPacketValid(e.CommunicationPacket.Data))
        //    {
        //        DisplayInstructionBase? instruction = null;

        //        PacketType packetType = (PacketType)e.CommunicationPacket.Data[3];
        //        switch (packetType)
        //        {
        //            case PacketType.RectangleFill:
        //                instruction = RectangleFillDisplayInstruction.GetInstruction(e.CommunicationPacket.Data);
        //                break;
        //            case PacketType.RectangleFillBorder:
        //                break;
        //            case PacketType.RectangleBorder:
        //                instruction = RectangleBorderDisplayInstruction.GetInstruction(e.CommunicationPacket.Data);
        //                break;
        //            case PacketType.RectangleFillRadius:
        //                break;
        //            case PacketType.RectangleFillBorderRadius:
        //                break;
        //            case PacketType.RectangleBorderRadius:
        //                break;
        //            case PacketType.Image:
        //                instruction = ImageDisplayInstruction.GetInstruction(e.CommunicationPacket.Data);
        //                break;
        //            case PacketType.UpdateDisplay:
        //                OnDisplayInstructionsReceived(new EventArgs());
        //                break;
        //            default: 
        //                break;
        //        }

        //        if (instruction != null)
        //        {
        //            DisplayInstructionsQueue.Enqueue(instruction);
        //        }
        //    }
        //}

        //private void OnDisplayInstructionsReceived(EventArgs e)
        //{
        //    m_displayGraphics.ProcessDisplayInstructions(DisplayInstructionsQueue);

        //    DisplayInstructionsReceived?.Invoke(this, e);
        //}


    }
}
