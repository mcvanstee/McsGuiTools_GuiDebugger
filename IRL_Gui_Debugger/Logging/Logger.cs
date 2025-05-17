using IRL_Gui_Debugger.Forms;
using System.Diagnostics;
using System.Globalization;

namespace IRL_Gui_Debugger.Logging
{
    public enum LogType
    {
        All,
        Communication,
        Device,
        Application
    }

    public static class Logger
    {
        private static string m_appLogConsole = string.Empty;
        private static string m_communicationConsole = string.Empty;
        private static string m_deviceLogConsole = string.Empty;
        private static string m_combinedLog = string.Empty;

        public static void ClearLog()
        {
            m_appLogConsole = string.Empty;
            m_communicationConsole = string.Empty;
            m_deviceLogConsole = string.Empty;
            m_combinedLog = string.Empty;
        }

        public static string GetLog(LogType logType)
        {
            return logType switch
            {
                LogType.All => m_combinedLog,
                LogType.Communication => m_communicationConsole,
                LogType.Device => m_deviceLogConsole,
                LogType.Application => m_appLogConsole,
                _ => string.Empty,
            };
        }

        public static void Save(LogType logType, string directory)
        {
            string fullFilePath = $"{directory}\\McsGui_Log_{logType.ToString()}.txt";
            using StreamWriter streamWriter = new(fullFilePath);
            streamWriter.Write(GetLog(logType));
            streamWriter.Close();
        }

        public static void Error(string message)
        {
            AddMessageToLog($"ERROR {message}", LogType.Application);
        }

        public static void Message(string message)
        {
            AddMessageToLog(message, LogType.Application);
        }

        public static void AddMessageToDeviceLog(string message)
        {
            AddMessageToLog(message, LogType.Device);
        }

        public static void AddMessageToCommunicationLog(string message)
        {
            AddMessageToLog(message, LogType.Communication);
        }

        private static void AddMessageToLog(string message, LogType logType)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);
            string logLine = $"{timestamp}    {message}\r\n";

            m_combinedLog += logLine;

            switch (logType)
            {
                case LogType.Communication:
                    m_communicationConsole += logLine;
                    break;
                case LogType.Application: 
                    m_appLogConsole += logLine;
                    break;
                case LogType.Device:
                    m_deviceLogConsole += logLine;
                    break;
                default:
                    break;
            }

            MainWindow.Instance.Invoke((MethodInvoker)delegate
            {
                MainWindow.Instance.AddMessageToOutputConsol(logLine, logType);
            });
        }
    }
}
