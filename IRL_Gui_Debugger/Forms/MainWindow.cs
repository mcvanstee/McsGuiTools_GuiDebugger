using Gui_Debug_Tool.Communication;
using Gui_Debug_Tool.DisplaySimulator;
using IRL_Gui_Debugger.Communication;
using IRL_Gui_Debugger.Communication.Commands;
using IRL_Gui_Debugger.Communication.GuiDebugProtocol;
using IRL_Gui_Debugger.CustomComponents;
using IRL_Gui_Debugger.Logging;
using IRL_Gui_Debugger.Properties;
using IRL_Gui_Debugger.Settings;
using IRL_Gui_Debugger.Utils;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Reflection;

namespace IRL_Gui_Debugger.Forms
{
    public partial class MainWindow : Form
    {
        public enum ConnectionState
        {
            Disconnected,
            Connected,
            Configured,
        }

        // Singelton
        //
        private static MainWindow? instance = null;
        private readonly Color IRLColor = Color.FromArgb(33, 149, 242);

        private AppSettings m_settings;
        private bool m_lockConsole = true;
        private static SerialPort m_serialPort = new();
        private Thread m_communicationThread;
        private Thread m_controllerThread;
        private ConnectionState m_connectionState = ConnectionState.Disconnected;
        private readonly CommuncationErrorCallbackDelegate communcationErrorCallback = new(CommunicationError);
        private DeviceConfig m_deviceConfig = new();
        private readonly DisplayGraphics m_displayGraphics = new();
        private Bitmap m_currentDisplayBitmap = new(1, 1, PixelFormat.Format16bppRgb565);
        private List<ButtonSetup> m_buttonSetups = new();
        private List<Button> m_customButtons = new();
        private List<ButtonEventInput> m_buttonEventInputs = new();
        private readonly FileWriteHandler m_fileWriteHandler = new();

        private KeyMessageFilter m_keyMessageFilter = new();

        private FileTransferWindow m_fileTransferWindow = new(0);

        public static bool DisplayConfigured { get; private set; } = false;

        public static MainWindow Instance
        {
            get
            {
                instance ??= new();

                return instance;
            }
        }

        private MainWindow()
        {
            InitializeComponent();
            //KeyPreview = true;

            ToolTip.SetToolTip(ClearOutputButton, "Clear all");
            ToolTip.SetToolTip(ToggleWordWrapButton, "Toggle word wrap");
            ToolTip.SetToolTip(LockConsoleButton, "Lock console");
            ToolTip.SetToolTip(SaveLogButton, "Save selected Log file");

            ToolTip.SetToolTip(RefreshComportBtn, "Update Serial ports");
            ToolTip.SetToolTip(ConnectButton, "Open selected port, and send config request");
            ToolTip.SetToolTip(DisconnectButton, "Close current serial port");
            //ToolTip.SetToolTip(AutoConnectCheckBox, "Check to auto ")

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer, true);

            MenuStrip.BackColor = IRLColor;

            typeof(Panel).InvokeMember("DoubleBuffered",
                        BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                        null, DisplayPanel, new object[] { true });

            m_keyMessageFilter.NavigationKeyPressed += NavigationKeyPressed;
            m_keyMessageFilter.NavigationKeyReleased += NavigationKeyReleased;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            SetConnectionState(ConnectionState.Disconnected);
            SelectConsolOutputCB.SelectedIndex = 0;

            AddCustomButtonsToList();
            SetCustomButtonsVisability();

            AddButtonEventInputs();
            SetButtonEventInputsVisability();

            LoadSettings();

            Stopwatch stopwatch = new();
            stopwatch.Start();
            GetAvailablePorts();
            stopwatch.Stop();
            Debug.WriteLine("Load: " + stopwatch.ElapsedMilliseconds);

            OpenGuiImageFile(m_settings.GuiImageDirectory);
            Application.AddMessageFilter(m_keyMessageFilter);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DeviceCommunication.Stop();
            CommunicationController.Stop();

            m_serialPort.Close();
            m_communicationThread?.Join();
            m_controllerThread?.Join();

            m_settings.Save();

            base.OnClosing(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                                                     // 
                return handleParam;
            }
        }

        private void ClearOutputButton_Click(object sender, EventArgs e)
        {
            Logger.ClearLog();
            OutputConsole.Clear();
        }

        public void DeviceConfigRecieved(DeviceConfig deviceConfig)
        {
            m_deviceConfig = deviceConfig;
            DisplayConfigured = true;
            SetConnectionIcon(ConnectionState.Configured, deviceConfig.Description);
            m_displayGraphics.CreateDisplayBitmap(deviceConfig.DisplayWidth, deviceConfig.DisplayHeight);

            NavKeyEnabledCheckBox.Checked = deviceConfig.UseKeyNav;
            TouchEnabledCheckBox.Checked = deviceConfig.UseTouch;

            SetDisplayDimensionLabelPosition();

            if (deviceConfig.CustomButtonsDefined > 0)
            {
                m_buttonSetups.Clear();
                PacketHandler.CommandQueue.Enqueue(new ButtonSetupCommand(0));
            }

            DisplayPanel.Invalidate();

            PacketHandler.CommandQueue.Enqueue(SetRtcCommand.GetCommand());
        }

        public void ButtonSetupReceived(ButtonSetup buttonSetup)
        {
            m_buttonSetups.Add(buttonSetup);
            ConfigureButton(buttonSetup);

            if (m_buttonSetups.Count < m_deviceConfig.CustomButtonsDefined)
            {
                PacketHandler.CommandQueue.Enqueue(new ButtonSetupCommand(m_buttonSetups.Count));
            }
        }

        public void RequestResponseReceived(CommunicationPacket packet)
        {
            m_fileWriteHandler.RequestResponseReceived(packet);
            m_fileTransferWindow.UpdateProgress(m_fileWriteHandler.FileBytesRemaining);

            if (m_fileTransferWindow.CancelTransfer)
            {
                m_fileWriteHandler.CancelFileWrite();
                m_fileTransferWindow.Close();
            }

            if (m_fileWriteHandler.FileBytesRemaining == 0)
            {
                m_fileTransferWindow.Close();
            }
        }

        private void ConfigureButton(ButtonSetup buttonSetup)
        {
            string description = buttonSetup.Description;
            if (string.IsNullOrEmpty(description))
            {
                description = $"Button {buttonSetup.Id}";
            }
            for (int i = 0; i < m_customButtons.Count; i++)
            {
                if (i == buttonSetup.Id - 1)
                {
                    m_customButtons[i].Visible = true;
                    m_customButtons[i].Text = description;

                    ButtonEventInput input = m_buttonEventInputs[i];
                    input.Visible = true;
                    input.ButtonName = description;
                }
            }
        }

        private void SetDisplayDimensionLabelPosition()
        {
            Point displayPosition = GetDisplayPosition();
            int xDisplay = displayPosition.X;
            int yDisplay = displayPosition.Y;

            Point newLabelLocation = new(xDisplay - 10, yDisplay + m_deviceConfig.DisplayHeight + 8);
            DisplayDimensionsLabel.Location = newLabelLocation;

            DisplayDimensionsLabel.Text = $"{m_deviceConfig.DisplayWidth}x{m_deviceConfig.DisplayHeight}";
        }

        private void AddCustomButtonsToList()
        {
            m_customButtons.Add(Button1);
            m_customButtons.Add(Button2);
            m_customButtons.Add(Button3);
            m_customButtons.Add(Button4);
            m_customButtons.Add(Button5);
            m_customButtons.Add(Button6);
            m_customButtons.Add(Button7);
            m_customButtons.Add(Button8);
            m_customButtons.Add(Button9);
            m_customButtons.Add(Button10);
            m_customButtons.Add(Button11);
            m_customButtons.Add(Button12);
            m_customButtons.Add(Button13);
            m_customButtons.Add(Button14);
        }

        private void AddButtonEventInputs()
        {
            m_buttonEventInputs.Add(EventInput1);
            m_buttonEventInputs.Add(EventInput2);
            m_buttonEventInputs.Add(EventInput3);
            m_buttonEventInputs.Add(EventInput4);
            m_buttonEventInputs.Add(EventInput5);
            m_buttonEventInputs.Add(EventInput6);
            m_buttonEventInputs.Add(EventInput7);
            m_buttonEventInputs.Add(EventInput8);
            m_buttonEventInputs.Add(EventInput9);
            m_buttonEventInputs.Add(EventInput10);
            m_buttonEventInputs.Add(EventInput11);
            m_buttonEventInputs.Add(EventInput12);
            m_buttonEventInputs.Add(EventInput12);
            m_buttonEventInputs.Add(EventInput13);
            m_buttonEventInputs.Add(EventInput14);
        }

        private void SetButtonEventInputsVisability()
        {
            foreach (ButtonEventInput eventInput in m_buttonEventInputs)
            {
                eventInput.Visible = false;
            }
        }

        private void SetCustomButtonsVisability()
        {
            foreach (Button button in m_customButtons)
            {
                button.Visible = false;
            }
        }

        private void NavigateToHomeViewButton_Click(object sender, EventArgs e)
        {
            EventCommand eventCommand = new(GuiEvent.NavigateToHome);
            PacketHandler.CommandQueue.Enqueue(eventCommand);
        }

        private void SyncDateTimeButton_Click(object sender, EventArgs e)
        {
            EventCommand eventCommand = SetRtcCommand.GetCommand();
            PacketHandler.CommandQueue.Enqueue(eventCommand);
        }

        // Navigation Keys Panel
        //
        private void CaptureKeysCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_keyMessageFilter.CaptureKeyEvents = CaptureKeysCheckBox.Checked;
        }

        private void KeyNavUpBtn_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyUpRelease());
        }

        private void KeyNavUpBtn_MouseDown(object sender, MouseEventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyUpPress());
        }

        private void KeyNavLeftBtn_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyLeftRelease());
        }

        private void KeyNavLeftBtn_MouseDown(object sender, MouseEventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyLeftPress());
        }

        private void KeyNavOKBtn_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyEnterRelease());
        }

        private void KeyNavOKBtn_MouseDown(object sender, MouseEventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyEnterPress());
        }

        private void KeyNavRightBtn_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyRightRelease());
        }

        private void KeyNavRightBtn_MouseDown(object sender, MouseEventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyRightPress());
        }

        private void KeyNavDownBtn_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyDownRelease());
        }

        private void KeyNavDownBtn_MouseDown(object sender, MouseEventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyDownPress());
        }

        private void NavigationKeyPressed(object sender, CaptureKeyEventArgs e)
        {
            if (e.Key == Keys.Left)
            {
                PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyLeftPress());
            }
            else if (e.Key == Keys.Up)
            {
                PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyUpPress());
            }
            else if (e.Key == Keys.Right)
            {
                PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyRightPress());
            }
            else if (e.Key == Keys.Down)
            {
                PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyDownPress());
            }
            else if (e.Key == Keys.Space)
            {
                PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyEnterPress());
            }
            else
            {
            }
        }

        private void NavigationKeyReleased(object sender, CaptureKeyEventArgs e)
        {
            if (e.Key == Keys.Left)
            {
                PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyLeftRelease());
            }
            else if (e.Key == Keys.Up)
            {
                PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyUpRelease());
            }
            else if (e.Key == Keys.Right)
            {
                PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyRightRelease());
            }
            else if (e.Key == Keys.Down)
            {
                PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyDownRelease());
            }
            else if (e.Key == Keys.Space)
            {
                PacketHandler.CommandQueue.Enqueue(NavKeyCommand.NavKeyEnterRelease());
            }
            else
            {
            }
        }


        // Output Console 
        //
        private void SelectConsolOutputCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = SelectConsolOutputCB.SelectedIndex;
            OutputConsole.Clear();

            switch (selectedIndex)
            {
                case 0:
                    OutputConsole.Text = Logger.GetLog(LogType.All);
                    break;
                case 1:
                    OutputConsole.Text = Logger.GetLog(LogType.Communication);
                    break;
                case 2:
                    OutputConsole.Text = Logger.GetLog(LogType.Device);
                    break;
                case 3:
                    OutputConsole.Text = Logger.GetLog(LogType.Application);
                    break;
            }
        }

        private void LockConsoleButton_Click(object sender, EventArgs e)
        {
            if (m_lockConsole)
            {
                m_lockConsole = false;
                LockConsoleButton.BackColor = SystemColors.ActiveBorder;
            }
            else
            {
                m_lockConsole = true;
                LockConsoleButton.BackColor = SystemColors.Control;

                OutputConsole.SelectionStart = OutputConsole.Text.Length;
                OutputConsole.ScrollToCaret();
            }
        }

        private void ToggleWordWrapButton_Click(object sender, EventArgs e)
        {
            OutputConsole.WordWrap = !OutputConsole.WordWrap;
        }

        private void SaveLogButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = SelectConsolOutputCB.SelectedIndex;
            LogType logType = (LogType)selectedIndex;

            string dir = m_settings.LogDirectory;

            if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
            {
                FolderBrowserDialog dialog = new();
                DialogResult result = dialog.ShowDialog();

                if (DialogResult.OK == result)
                {
                    dir = dialog.SelectedPath;
                    m_settings.LogDirectory = dir;
                }
            }

            Logger.Save(logType, dir);
        }

        public void AddMessageToOutputConsol(string logLine, LogType logType)
        {
            int selectedIndex = SelectConsolOutputCB.SelectedIndex;

            if (selectedIndex == (int)LogType.All || selectedIndex == (int)logType)
            {
                OutputConsole.AppendText(logLine);
            }

            if (m_lockConsole)
            {
                OutputConsole.SelectionStart = OutputConsole.Text.Length;
                OutputConsole.ScrollToCaret();
            }
        }


        // Connection
        //
        private static int DropDownWidth(ComboBox myCombo)
        {
            int maxWidth = 0;
            int temp;
            Label label1 = new();

            foreach (var obj in myCombo.Items)
            {
                label1.Text = obj.ToString();
                temp = label1.PreferredWidth;

                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }

            label1.Dispose();
            return maxWidth;
        }

        public void SetConnectionState(ConnectionState connectionState)
        {
            m_connectionState = connectionState;
            SetConnectionIcon(connectionState, "");
        }

        public void SetConnectionIcon(ConnectionState connectionState, string deviceDescription)
        {
            m_connectionState = connectionState;

            if (string.IsNullOrEmpty(deviceDescription))
            {
                deviceDescription = "McsGui Device";
            }

            if (connectionState == ConnectionState.Configured)
            {
                ToolStripProgressLabel.Image = Resources.OnlineStatusAvailable;
                ToolStripProgressLabel.Text = deviceDescription;
            }
            else if (connectionState == ConnectionState.Connected)
            {
                ToolStripProgressLabel.Image = Resources.ConnectionWarning;
            }
            else
            {
                ToolStripProgressLabel.Image = Resources.OnlineStatusPresenting;
            }
        }

        private void RefreshComportBtn_Click(object sender, EventArgs e)
        {
            GetAvailablePorts();
        }

        private async void GetAvailablePorts()
        {
            string[] availablePorts = await SerialPortConnect.GetAvailablePorts();
            SetComPortComboBoxItems(availablePorts);
        }

        public void SetComPortComboBoxItems(string[] items)
        {
            ComPortComboBox.Items.Clear();
            ComPortComboBox.Items.AddRange(items);

            if (ComPortComboBox.Items.Count > 0)
            {
                ComPortComboBox.SelectedIndex = ComPortComboBox.Items.Count - 1;
            }

            if (ComPortComboBox.Items.Count > 0)
            {
                ComPortComboBox.DropDownWidth = DropDownWidth(ComPortComboBox);
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (m_serialPort.IsOpen)
            {
                return;
            }

            string portName = GetSelectedPortName();
            m_serialPort = SerialPortConnect.OpenNewSerialPort(portName, m_settings.BaudRate);

            if (m_serialPort.IsOpen)
            {
                StartCommunication();
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            StopCommunication();
        }

        private void StartCommunication()
        {
            SetConnectionState(ConnectionState.Connected);

            DeviceCommunication deviceCommunication = new(m_serialPort, communcationErrorCallback);
            m_communicationThread = new(new ThreadStart(deviceCommunication.StartDeviceCommunication));
            m_communicationThread.Name = "Communication thread";

            CommunicationController controller = new(m_displayGraphics);
            m_controllerThread = new(new ThreadStart(controller.StartCommunicationController));
            m_controllerThread.Name = "Communication controller thread";

            m_communicationThread.Start();
            m_controllerThread.Start();
        }

        private void StopCommunication()
        {
            DeviceCommunication.Stop();
            CommunicationController.Stop();
            DisplayConfigured = false;
            SetConnectionState(ConnectionState.Disconnected);
            m_serialPort.Close();
        }

        private static void CommunicationError(CommunicationResult communicationResult)
        {
            Debug.WriteLine($"Communication Error {communicationResult}");

            switch (communicationResult)
            {
                case CommunicationResult.ComPortClosed:
                    Logger.AddMessageToCommunicationLog("Serialport closed");
                    break;
                case CommunicationResult.ReadTimeout:
                    Logger.AddMessageToCommunicationLog("Serialport read timeout");
                    break;
                case CommunicationResult.WriteTimeout:
                    Logger.AddMessageToCommunicationLog("Serialport write timeout");
                    break;
                case CommunicationResult.UnKnown:
                    break;
                default:
                    break;
            }

            MainWindow.Instance.Invoke((MethodInvoker)delegate
            {
                MainWindow.Instance.StopCommunication();
            });
        }

        private string GetSelectedPortName()
        {
            int selectedIndex = ComPortComboBox.SelectedIndex;

            if (selectedIndex < 0)
            {
                return string.Empty;
            }

            string portName = ComPortComboBox.Items[selectedIndex].ToString();

            return portName.Substring(0, 4);
        }


        // Paint DisplayPanel
        //
        private void DisplayPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawDisplayBorder(e.Graphics);
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            //DrawDisplayBorder(e.Graphics);
        }

        private void DrawDisplayBorder(Graphics graphics)
        {
            if (!DisplayConfigured)
            {
                return;
            }

            // TODO checks on max width..
            Point displayPosition = GetDisplayPosition();
            int x = displayPosition.X;
            int y = displayPosition.Y;

            Rectangle rectangle = new(x - 5, y - 5, m_deviceConfig.DisplayWidth + 10, m_deviceConfig.DisplayHeight + 10);
            Pen pen = new(Color.Gray, 6);

            graphics.DrawRectangle(pen, rectangle);

            if (DisplayGraphics.GraphicsCache.TryDequeue(out Image? result))
            {
                Bitmap bitmap = (Bitmap)result;

                if (bitmap != null &&
                    (bitmap.Width != 0 || bitmap.Height != 0))
                {
                    m_currentDisplayBitmap = bitmap;
                }
            }

            if (m_currentDisplayBitmap != null && (m_currentDisplayBitmap.Width != 0 || m_currentDisplayBitmap.Height != 0))
            {
                graphics.DrawImage(m_currentDisplayBitmap, x, y);
            }

            SetDisplayDimensionLabelPosition();
            DisplayPanel.Invalidate();
        }


        // Custom butons
        //
        private void Button1_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(0));
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(1));
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(2));
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(3));
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(4));
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(5));
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(6));
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(7));
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(8));
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(9));
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(10));
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(11));
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(12));
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            PacketHandler.CommandQueue.Enqueue(GetCustomButtonCommand(13));
        }

        private EventCommand GetCustomButtonCommand(int index)
        {
            byte[] eventParams = m_buttonEventInputs[index].Param;
            EventCommand eventCommand = new((byte)m_buttonSetups[index].GuiEvent);
            eventCommand.EventArgs = eventParams;

            return eventCommand;
        }


        // Touch
        //
        private void DisplayPanel_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void DisplayPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (!DisplayConfigured)
            {
                return;
            }

            Point mousePosition = GetMousePosition(e);

            if (CheckMousePositionInDisplay(mousePosition))
            {
                PacketHandler.CommandQueue.Enqueue(TouchCommand.TouchOnPressed(GetMousePosition(e)));
            }
        }

        private void DisplayPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (!DisplayConfigured)
            {
                return;
            }

            Point mousePosition = GetMousePosition(e);

            if (CheckMousePositionInDisplay(mousePosition))
            {
                PacketHandler.CommandQueue.Enqueue(TouchCommand.TouchOnReleased(GetMousePosition(e)));
            }
        }

        private Point GetMousePosition(MouseEventArgs e)
        {
            Point displayPosition = GetDisplayPosition();

            int x = e.Location.X - displayPosition.X;
            int y = e.Location.Y - displayPosition.Y;

            return new Point(x, y);
        }

        private bool CheckMousePositionInDisplay(Point mousePosition)
        {
            return mousePosition.X >= 0 &&
                   mousePosition.Y >= 0 &&
                   mousePosition.X < m_deviceConfig.DisplayWidth &&
                   mousePosition.Y < m_deviceConfig.DisplayHeight;
        }


        private void ScreenshotButton_Click(object sender, EventArgs e)
        {
            if (!DisplayConfigured)
            {
                return;
            }

            using Bitmap bitmap = (Bitmap)m_currentDisplayBitmap.Clone();

            string dir = m_settings.ScreenShotDirectory;

            if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
            {
                FolderBrowserDialog dialog = new();
                DialogResult result = dialog.ShowDialog();

                if (DialogResult.OK == result)
                {
                    dir = dialog.SelectedPath;
                    m_settings.ScreenShotDirectory = dir;
                }
            }

            try
            {
                string fileName = FileUtils.CreateUniqeFileName(dir, "McsGui_Screenshot", ".bmp");
                bitmap.Save(fileName, ImageFormat.Bmp);
            }
            catch
            {
                Logger.Error("Saving Screenshot");
            }
        }

        private void LoadSettings()
        {
            m_settings = new();

        }


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new(m_settings);
            settingsForm.StartPosition = FormStartPosition.CenterParent;
            settingsForm.ShowDialog();
        }

        private void SendGuiFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((m_connectionState != ConnectionState.Configured) || !m_serialPort.IsOpen)
            {
                MessageBox.Show("Device not connected!");

                return;
            }

            OpenFileDialog dialog = new();
            DialogResult dialogResult = dialog.ShowDialog();

            if (DialogResult.OK == dialogResult)
            {
                string selectedFile = dialog.FileName;

                if (File.Exists(selectedFile))
                {
                    FileInfo fileInfo = new FileInfo(selectedFile);
                    string fileName = fileInfo.Name;

                    bool fileOpen = m_fileWriteHandler.OpenFile(selectedFile);

                    if (fileOpen) 
                    {
                        bool bytesInFile = m_fileWriteHandler.StartFileWriteCommand(fileName);

                        if (bytesInFile)
                        {
                            m_fileTransferWindow = new(m_fileWriteHandler.TotalFileBytes);
                            m_fileTransferWindow.StartPosition = FormStartPosition.CenterParent;                          
                            m_fileTransferWindow.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Empty file");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error opening file");
                    }
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        // Gui Image
        //
        private void SelectGuiImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new();
            DialogResult dialogResult = dialog.ShowDialog();

            if (DialogResult.OK == dialogResult)
            {
                string selectedFile = dialog.FileName;

                if (File.Exists(selectedFile))
                {
                    m_settings.GuiImageDirectory = selectedFile;
                    OpenGuiImageFile(selectedFile);
                }
            }
        }

        private void OpenGuiImageFile(string filePath)
        {
            bool fileOpen = m_displayGraphics.OpenImageFile(filePath);
            if (fileOpen)
            {
                int index = filePath.LastIndexOf("\\");
                string fileName = filePath;
                fileName = fileName.Substring(index + 1, fileName.Length - index - 1);
                GuiImageFileLabel.Text = fileName;
                GuiImageFileLabel.ForeColor = SystemColors.ControlText;
            }
            else
            {
                GuiImageFileLabel.Text = "File not open!";
                GuiImageFileLabel.ForeColor = Color.Red;
            }
        }

        private Point GetDisplayPosition()
        {
            int xDisplay = (DisplayPanel.Width - m_deviceConfig.DisplayWidth) / 2;
            int yDisplay = (DisplayPanel.Height - m_deviceConfig.DisplayHeight) / 2;

            return new Point(xDisplay, yDisplay);
        }
    }
}
