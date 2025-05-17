using Gui_Debug_Tool.DisplaySimulator;
using IRL_Gui_Debugger.Communication.Commands;
using IRL_Gui_Debugger.Forms;
using System.Diagnostics;

namespace IRL_Gui_Debugger.Communication
{
    public class CommunicationController
    {
        private enum CommunicationStatus
        {
            Idle,
            WaitingForConfigResponse
        }

        private static bool s_stop = false;
        public static bool IsConnectedResponse { get; set; } = false;

        private readonly Stopwatch m_stopWatch = new();
        private readonly DisplayGraphics m_displayGraphics;
        private CommunicationStatus m_status = CommunicationStatus.Idle;

        public CommunicationController(DisplayGraphics displayGraphics) 
        {
            m_displayGraphics = displayGraphics;
        }

        public void StartCommunicationController()
        {
            s_stop = false;
            PacketHandler.CommandQueue.Clear();

            while (!s_stop)
            {
                PacketHandler.ProcessPackets(m_displayGraphics);

                if (!MainWindow.DisplayConfigured && m_status == CommunicationStatus.Idle)
                {
                    PacketHandler.CommandQueue.Enqueue(new ConfigCommand());
                    m_status = CommunicationStatus.WaitingForConfigResponse;
                    m_stopWatch.Restart();
                }
                else if (!MainWindow.DisplayConfigured && m_status == CommunicationStatus.WaitingForConfigResponse)
                {
                    if (m_stopWatch.ElapsedMilliseconds > 1000)
                    {
                        m_status = CommunicationStatus.Idle;
                        m_stopWatch.Stop();
                    }
                }
                else if (MainWindow.DisplayConfigured && m_status == CommunicationStatus.WaitingForConfigResponse)
                {
                    m_status = CommunicationStatus.Idle;
                    m_stopWatch.Stop();
                }
                else
                {
                }
                

            }
        }

        public static void Stop()
        {
            s_stop = true;
        }
    }
}
