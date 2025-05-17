namespace IRL_Gui_Debugger.Forms
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            statusStrip1 = new StatusStrip();
            ToolStripProgressLabel = new ToolStripStatusLabel();
            MenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            ToolPanel = new Panel();
            ScreenshotButton = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            panel14 = new Panel();
            panelWithBorder7 = new IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder();
            DisplayPanel = new Panel();
            DisplayDimensionsLabel = new Label();
            panel16 = new Panel();
            Button14 = new Button();
            Button13 = new Button();
            Button12 = new Button();
            Button11 = new Button();
            Button10 = new Button();
            Button9 = new Button();
            panel17 = new Panel();
            Button7 = new Button();
            Button5 = new Button();
            Button6 = new Button();
            Button8 = new Button();
            panel15 = new Panel();
            Button3 = new Button();
            Button1 = new Button();
            Button2 = new Button();
            Button4 = new Button();
            splitter3 = new Splitter();
            splitter2 = new Splitter();
            splitter1 = new Splitter();
            panel6 = new Panel();
            panelWithBorder2 = new IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder();
            panel7 = new Panel();
            panel9 = new Panel();
            OutputConsole = new RichTextBox();
            OutputPanel = new Panel();
            SaveLogButton = new Button();
            LockConsoleButton = new Button();
            SelectConsolOutputCB = new ComboBox();
            ToggleWordWrapButton = new Button();
            ClearOutputButton = new Button();
            label1 = new Label();
            panel5 = new Panel();
            panel10 = new Panel();
            panelWithBorder5 = new IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder();
            EventInput14 = new CustomComponents.ButtonEventInput();
            EventInput13 = new CustomComponents.ButtonEventInput();
            EventInput12 = new CustomComponents.ButtonEventInput();
            EventInput11 = new CustomComponents.ButtonEventInput();
            EventInput10 = new CustomComponents.ButtonEventInput();
            EventInput9 = new CustomComponents.ButtonEventInput();
            EventInput8 = new CustomComponents.ButtonEventInput();
            EventInput7 = new CustomComponents.ButtonEventInput();
            EventInput6 = new CustomComponents.ButtonEventInput();
            EventInput5 = new CustomComponents.ButtonEventInput();
            EventInput4 = new CustomComponents.ButtonEventInput();
            EventInput3 = new CustomComponents.ButtonEventInput();
            EventInput2 = new CustomComponents.ButtonEventInput();
            EventInput1 = new CustomComponents.ButtonEventInput();
            panelWithBorder3 = new IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder();
            NavKeyEnabledCheckBox = new CheckBox();
            TouchEnabledCheckBox = new CheckBox();
            panel4 = new Panel();
            panelWithBorder4 = new IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder();
            AutoConnectDeviceNameLabel = new Label();
            AutoConnectCheckBox = new CheckBox();
            DisconnectButton = new Button();
            label5 = new Label();
            ConnectButton = new Button();
            RefreshComportBtn = new Button();
            label4 = new Label();
            ComPortComboBox = new ComboBox();
            KeyNavPanel = new IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder();
            CaptureKeysCheckBox = new CheckBox();
            KeyNavRightBtn = new Button();
            KeyNavLeftBtn = new Button();
            KeyNavOKBtn = new Button();
            KeyNavUpBtn = new Button();
            KeyNavDownBtn = new Button();
            panelWithBorder6 = new IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder();
            GuiImageFileLabel = new Label();
            SelectGuiImageButton = new Button();
            label2 = new Label();
            panelWithBorder1 = new IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder();
            SyncDateTimeButton = new Button();
            NavigateToHomeViewButton = new Button();
            ToolTip = new ToolTip(components);
            OpenFileDialog = new OpenFileDialog();
            FolderBrowserDialog = new FolderBrowserDialog();
            sendGuiFileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            statusStrip1.SuspendLayout();
            MenuStrip.SuspendLayout();
            ToolPanel.SuspendLayout();
            panel3.SuspendLayout();
            panel14.SuspendLayout();
            panelWithBorder7.SuspendLayout();
            DisplayPanel.SuspendLayout();
            panel16.SuspendLayout();
            panel17.SuspendLayout();
            panel15.SuspendLayout();
            panel6.SuspendLayout();
            panelWithBorder2.SuspendLayout();
            panel7.SuspendLayout();
            panel9.SuspendLayout();
            OutputPanel.SuspendLayout();
            panel5.SuspendLayout();
            panel10.SuspendLayout();
            panelWithBorder5.SuspendLayout();
            panelWithBorder3.SuspendLayout();
            panel4.SuspendLayout();
            panelWithBorder4.SuspendLayout();
            KeyNavPanel.SuspendLayout();
            panelWithBorder6.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { ToolStripProgressLabel });
            statusStrip1.Location = new Point(0, 894);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1390, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // ToolStripProgressLabel
            // 
            ToolStripProgressLabel.AccessibleName = "ToolStripStatusLabel";
            ToolStripProgressLabel.Image = Properties.Resources.OnlineStatusPresenting;
            ToolStripProgressLabel.Name = "ToolStripProgressLabel";
            ToolStripProgressLabel.Size = new Size(55, 17);
            ToolStripProgressLabel.Text = "Status";
            // 
            // MenuStrip
            // 
            MenuStrip.BackColor = SystemColors.Control;
            MenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(1390, 24);
            MenuStrip.TabIndex = 1;
            MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, sendGuiFileToolStripMenuItem, toolStripSeparator1, closeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(180, 22);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(180, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(107, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // ToolPanel
            // 
            ToolPanel.Controls.Add(ScreenshotButton);
            ToolPanel.Dock = DockStyle.Top;
            ToolPanel.Location = new Point(0, 24);
            ToolPanel.Name = "ToolPanel";
            ToolPanel.Size = new Size(1390, 24);
            ToolPanel.TabIndex = 2;
            // 
            // ScreenshotButton
            // 
            ScreenshotButton.FlatStyle = FlatStyle.Flat;
            ScreenshotButton.ForeColor = SystemColors.ControlText;
            ScreenshotButton.Image = (Image)resources.GetObject("ScreenshotButton.Image");
            ScreenshotButton.ImageAlign = ContentAlignment.MiddleLeft;
            ScreenshotButton.Location = new Point(5, 1);
            ScreenshotButton.Name = "ScreenshotButton";
            ScreenshotButton.Size = new Size(92, 22);
            ScreenshotButton.TabIndex = 0;
            ScreenshotButton.Text = "Screenshot";
            ScreenshotButton.TextAlign = ContentAlignment.MiddleRight;
            ScreenshotButton.UseVisualStyleBackColor = true;
            ScreenshotButton.Click += ScreenshotButton_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 889);
            panel2.Name = "panel2";
            panel2.Size = new Size(1390, 5);
            panel2.TabIndex = 3;
            // 
            // panel3
            // 
            panel3.Controls.Add(panel14);
            panel3.Controls.Add(splitter3);
            panel3.Controls.Add(splitter2);
            panel3.Controls.Add(splitter1);
            panel3.Controls.Add(panel6);
            panel3.Controls.Add(panel5);
            panel3.Controls.Add(panel4);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 48);
            panel3.Name = "panel3";
            panel3.Size = new Size(1390, 841);
            panel3.TabIndex = 4;
            // 
            // panel14
            // 
            panel14.Controls.Add(panelWithBorder7);
            panel14.Dock = DockStyle.Fill;
            panel14.Location = new Point(224, 0);
            panel14.Name = "panel14";
            panel14.Padding = new Padding(0, 5, 0, 0);
            panel14.Size = new Size(943, 621);
            panel14.TabIndex = 6;
            // 
            // panelWithBorder7
            // 
            panelWithBorder7.BorderColor = Color.Gray;
            panelWithBorder7.Controls.Add(DisplayPanel);
            panelWithBorder7.Controls.Add(panel16);
            panelWithBorder7.Controls.Add(panel17);
            panelWithBorder7.Controls.Add(panel15);
            panelWithBorder7.Dock = DockStyle.Fill;
            panelWithBorder7.Location = new Point(0, 5);
            panelWithBorder7.Name = "panelWithBorder7";
            panelWithBorder7.Padding = new Padding(5);
            panelWithBorder7.Size = new Size(943, 616);
            panelWithBorder7.TabIndex = 0;
            // 
            // DisplayPanel
            // 
            DisplayPanel.Controls.Add(DisplayDimensionsLabel);
            DisplayPanel.Dock = DockStyle.Fill;
            DisplayPanel.Location = new Point(80, 5);
            DisplayPanel.Name = "DisplayPanel";
            DisplayPanel.Size = new Size(783, 531);
            DisplayPanel.TabIndex = 4;
            DisplayPanel.Paint += DisplayPanel_Paint;
            DisplayPanel.MouseClick += DisplayPanel_MouseClick;
            DisplayPanel.MouseDown += DisplayPanel_MouseDown;
            DisplayPanel.MouseUp += DisplayPanel_MouseUp;
            // 
            // DisplayDimensionsLabel
            // 
            DisplayDimensionsLabel.AutoSize = true;
            DisplayDimensionsLabel.Location = new Point(165, 454);
            DisplayDimensionsLabel.Name = "DisplayDimensionsLabel";
            DisplayDimensionsLabel.Size = new Size(12, 15);
            DisplayDimensionsLabel.TabIndex = 0;
            DisplayDimensionsLabel.Text = "-";
            // 
            // panel16
            // 
            panel16.Controls.Add(Button14);
            panel16.Controls.Add(Button13);
            panel16.Controls.Add(Button12);
            panel16.Controls.Add(Button11);
            panel16.Controls.Add(Button10);
            panel16.Controls.Add(Button9);
            panel16.Dock = DockStyle.Bottom;
            panel16.Location = new Point(80, 536);
            panel16.Name = "panel16";
            panel16.Size = new Size(783, 75);
            panel16.TabIndex = 3;
            // 
            // Button14
            // 
            Button14.FlatStyle = FlatStyle.Flat;
            Button14.Location = new Point(664, 0);
            Button14.Name = "Button14";
            Button14.Size = new Size(75, 75);
            Button14.TabIndex = 7;
            Button14.Text = "Button 14";
            Button14.UseVisualStyleBackColor = true;
            Button14.Click += Button14_Click;
            // 
            // Button13
            // 
            Button13.FlatStyle = FlatStyle.Flat;
            Button13.Location = new Point(544, 0);
            Button13.Name = "Button13";
            Button13.Size = new Size(75, 75);
            Button13.TabIndex = 6;
            Button13.Text = "Button 13";
            Button13.UseVisualStyleBackColor = true;
            Button13.Click += Button13_Click;
            // 
            // Button12
            // 
            Button12.FlatStyle = FlatStyle.Flat;
            Button12.Location = new Point(424, 0);
            Button12.Name = "Button12";
            Button12.Size = new Size(75, 75);
            Button12.TabIndex = 5;
            Button12.Text = "Button 12";
            Button12.UseVisualStyleBackColor = true;
            Button12.Click += Button12_Click;
            // 
            // Button11
            // 
            Button11.FlatStyle = FlatStyle.Flat;
            Button11.Location = new Point(280, 0);
            Button11.Name = "Button11";
            Button11.Size = new Size(75, 75);
            Button11.TabIndex = 4;
            Button11.Text = "Button 11";
            Button11.UseVisualStyleBackColor = true;
            Button11.Click += Button11_Click;
            // 
            // Button10
            // 
            Button10.FlatStyle = FlatStyle.Flat;
            Button10.Location = new Point(160, 0);
            Button10.Name = "Button10";
            Button10.Size = new Size(75, 75);
            Button10.TabIndex = 3;
            Button10.Text = "Button 10";
            Button10.UseVisualStyleBackColor = true;
            Button10.Click += Button10_Click;
            // 
            // Button9
            // 
            Button9.FlatStyle = FlatStyle.Flat;
            Button9.Location = new Point(40, 0);
            Button9.Name = "Button9";
            Button9.Size = new Size(75, 75);
            Button9.TabIndex = 2;
            Button9.Text = "Button 9";
            Button9.UseVisualStyleBackColor = true;
            Button9.Click += Button9_Click;
            // 
            // panel17
            // 
            panel17.Controls.Add(Button7);
            panel17.Controls.Add(Button5);
            panel17.Controls.Add(Button6);
            panel17.Controls.Add(Button8);
            panel17.Dock = DockStyle.Right;
            panel17.Location = new Point(863, 5);
            panel17.Name = "panel17";
            panel17.Size = new Size(75, 606);
            panel17.TabIndex = 2;
            // 
            // Button7
            // 
            Button7.FlatStyle = FlatStyle.Flat;
            Button7.Location = new Point(0, 288);
            Button7.Name = "Button7";
            Button7.Size = new Size(75, 75);
            Button7.TabIndex = 5;
            Button7.Text = "Button 7";
            Button7.UseVisualStyleBackColor = true;
            Button7.Click += Button7_Click;
            // 
            // Button5
            // 
            Button5.FlatStyle = FlatStyle.Flat;
            Button5.Location = new Point(0, 59);
            Button5.Name = "Button5";
            Button5.Size = new Size(75, 75);
            Button5.TabIndex = 3;
            Button5.Text = "Button 5";
            Button5.UseVisualStyleBackColor = true;
            Button5.Click += Button5_Click;
            // 
            // Button6
            // 
            Button6.FlatStyle = FlatStyle.Flat;
            Button6.Location = new Point(0, 176);
            Button6.Name = "Button6";
            Button6.Size = new Size(75, 75);
            Button6.TabIndex = 4;
            Button6.Text = "Button 6";
            Button6.UseVisualStyleBackColor = true;
            Button6.Click += Button6_Click;
            // 
            // Button8
            // 
            Button8.FlatStyle = FlatStyle.Flat;
            Button8.Location = new Point(0, 400);
            Button8.Name = "Button8";
            Button8.Size = new Size(75, 75);
            Button8.TabIndex = 1;
            Button8.Text = "Button 8";
            Button8.UseVisualStyleBackColor = true;
            Button8.Click += Button8_Click;
            // 
            // panel15
            // 
            panel15.Controls.Add(Button3);
            panel15.Controls.Add(Button1);
            panel15.Controls.Add(Button2);
            panel15.Controls.Add(Button4);
            panel15.Dock = DockStyle.Left;
            panel15.Location = new Point(5, 5);
            panel15.Name = "panel15";
            panel15.Size = new Size(75, 606);
            panel15.TabIndex = 0;
            // 
            // Button3
            // 
            Button3.FlatStyle = FlatStyle.Flat;
            Button3.Location = new Point(0, 289);
            Button3.Name = "Button3";
            Button3.Size = new Size(75, 75);
            Button3.TabIndex = 2;
            Button3.Text = "Button 3";
            Button3.UseVisualStyleBackColor = true;
            Button3.Click += Button3_Click;
            // 
            // Button1
            // 
            Button1.FlatStyle = FlatStyle.Flat;
            Button1.Location = new Point(0, 59);
            Button1.Name = "Button1";
            Button1.Size = new Size(75, 75);
            Button1.TabIndex = 0;
            Button1.Text = "Button 1";
            Button1.UseVisualStyleBackColor = true;
            Button1.Click += Button1_Click;
            // 
            // Button2
            // 
            Button2.FlatStyle = FlatStyle.Flat;
            Button2.Location = new Point(0, 176);
            Button2.Name = "Button2";
            Button2.Size = new Size(75, 75);
            Button2.TabIndex = 1;
            Button2.Text = "Button 2";
            Button2.UseVisualStyleBackColor = true;
            Button2.Click += Button2_Click;
            // 
            // Button4
            // 
            Button4.FlatStyle = FlatStyle.Flat;
            Button4.Location = new Point(0, 400);
            Button4.Name = "Button4";
            Button4.Size = new Size(75, 75);
            Button4.TabIndex = 1;
            Button4.Text = "Button 4";
            Button4.UseVisualStyleBackColor = true;
            Button4.Click += Button4_Click;
            // 
            // splitter3
            // 
            splitter3.BackColor = SystemColors.Control;
            splitter3.Dock = DockStyle.Bottom;
            splitter3.Location = new Point(224, 621);
            splitter3.Name = "splitter3";
            splitter3.Size = new Size(943, 5);
            splitter3.TabIndex = 5;
            splitter3.TabStop = false;
            // 
            // splitter2
            // 
            splitter2.BackColor = SystemColors.Control;
            splitter2.Dock = DockStyle.Right;
            splitter2.Location = new Point(1167, 0);
            splitter2.Name = "splitter2";
            splitter2.Size = new Size(5, 626);
            splitter2.TabIndex = 4;
            splitter2.TabStop = false;
            // 
            // splitter1
            // 
            splitter1.BackColor = SystemColors.Control;
            splitter1.Location = new Point(219, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(5, 626);
            splitter1.TabIndex = 3;
            splitter1.TabStop = false;
            // 
            // panel6
            // 
            panel6.Controls.Add(panelWithBorder2);
            panel6.Dock = DockStyle.Bottom;
            panel6.Location = new Point(219, 626);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(5);
            panel6.Size = new Size(953, 215);
            panel6.TabIndex = 2;
            // 
            // panelWithBorder2
            // 
            panelWithBorder2.BorderColor = Color.Gray;
            panelWithBorder2.Controls.Add(panel7);
            panelWithBorder2.Dock = DockStyle.Fill;
            panelWithBorder2.Location = new Point(5, 5);
            panelWithBorder2.Name = "panelWithBorder2";
            panelWithBorder2.Padding = new Padding(0, 1, 0, 1);
            panelWithBorder2.Size = new Size(943, 205);
            panelWithBorder2.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.Controls.Add(panel9);
            panel7.Controls.Add(OutputPanel);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(0, 1);
            panel7.Name = "panel7";
            panel7.Size = new Size(943, 203);
            panel7.TabIndex = 0;
            // 
            // panel9
            // 
            panel9.Controls.Add(OutputConsole);
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(0, 34);
            panel9.Name = "panel9";
            panel9.Size = new Size(943, 169);
            panel9.TabIndex = 1;
            // 
            // OutputConsole
            // 
            OutputConsole.Dock = DockStyle.Fill;
            OutputConsole.Location = new Point(0, 0);
            OutputConsole.Name = "OutputConsole";
            OutputConsole.ReadOnly = true;
            OutputConsole.Size = new Size(943, 169);
            OutputConsole.TabIndex = 0;
            OutputConsole.Text = "";
            // 
            // OutputPanel
            // 
            OutputPanel.Controls.Add(SaveLogButton);
            OutputPanel.Controls.Add(LockConsoleButton);
            OutputPanel.Controls.Add(SelectConsolOutputCB);
            OutputPanel.Controls.Add(ToggleWordWrapButton);
            OutputPanel.Controls.Add(ClearOutputButton);
            OutputPanel.Controls.Add(label1);
            OutputPanel.Dock = DockStyle.Top;
            OutputPanel.Location = new Point(0, 0);
            OutputPanel.Name = "OutputPanel";
            OutputPanel.Size = new Size(943, 34);
            OutputPanel.TabIndex = 0;
            // 
            // SaveLogButton
            // 
            SaveLogButton.FlatStyle = FlatStyle.Flat;
            SaveLogButton.Image = (Image)resources.GetObject("SaveLogButton.Image");
            SaveLogButton.Location = new Point(443, 7);
            SaveLogButton.Name = "SaveLogButton";
            SaveLogButton.Padding = new Padding(0, 0, 2, 2);
            SaveLogButton.Size = new Size(20, 20);
            SaveLogButton.TabIndex = 5;
            SaveLogButton.UseVisualStyleBackColor = true;
            SaveLogButton.Click += SaveLogButton_Click;
            // 
            // LockConsoleButton
            // 
            LockConsoleButton.FlatStyle = FlatStyle.Flat;
            LockConsoleButton.Image = (Image)resources.GetObject("LockConsoleButton.Image");
            LockConsoleButton.Location = new Point(417, 7);
            LockConsoleButton.Name = "LockConsoleButton";
            LockConsoleButton.Size = new Size(20, 20);
            LockConsoleButton.TabIndex = 4;
            LockConsoleButton.UseVisualStyleBackColor = true;
            LockConsoleButton.Click += LockConsoleButton_Click;
            // 
            // SelectConsolOutputCB
            // 
            SelectConsolOutputCB.DropDownStyle = ComboBoxStyle.DropDownList;
            SelectConsolOutputCB.FormattingEnabled = true;
            SelectConsolOutputCB.Items.AddRange(new object[] { "All", "Communication", "Device", "Application" });
            SelectConsolOutputCB.Location = new Point(115, 6);
            SelectConsolOutputCB.Name = "SelectConsolOutputCB";
            SelectConsolOutputCB.Size = new Size(234, 23);
            SelectConsolOutputCB.TabIndex = 3;
            SelectConsolOutputCB.SelectedIndexChanged += SelectConsolOutputCB_SelectedIndexChanged;
            // 
            // ToggleWordWrapButton
            // 
            ToggleWordWrapButton.FlatStyle = FlatStyle.Flat;
            ToggleWordWrapButton.Image = (Image)resources.GetObject("ToggleWordWrapButton.Image");
            ToggleWordWrapButton.Location = new Point(391, 7);
            ToggleWordWrapButton.Name = "ToggleWordWrapButton";
            ToggleWordWrapButton.Size = new Size(20, 20);
            ToggleWordWrapButton.TabIndex = 2;
            ToggleWordWrapButton.UseVisualStyleBackColor = true;
            ToggleWordWrapButton.Click += ToggleWordWrapButton_Click;
            // 
            // ClearOutputButton
            // 
            ClearOutputButton.FlatStyle = FlatStyle.Flat;
            ClearOutputButton.Image = (Image)resources.GetObject("ClearOutputButton.Image");
            ClearOutputButton.Location = new Point(365, 7);
            ClearOutputButton.Name = "ClearOutputButton";
            ClearOutputButton.Size = new Size(20, 20);
            ClearOutputButton.TabIndex = 1;
            ClearOutputButton.UseVisualStyleBackColor = true;
            ClearOutputButton.Click += ClearOutputButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 8);
            label1.Name = "label1";
            label1.Size = new Size(109, 15);
            label1.TabIndex = 0;
            label1.Text = "Show Output from:";
            // 
            // panel5
            // 
            panel5.Controls.Add(panel10);
            panel5.Controls.Add(panelWithBorder3);
            panel5.Dock = DockStyle.Right;
            panel5.Location = new Point(1172, 0);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(0, 5, 5, 0);
            panel5.Size = new Size(218, 841);
            panel5.TabIndex = 1;
            // 
            // panel10
            // 
            panel10.Controls.Add(panelWithBorder5);
            panel10.Dock = DockStyle.Fill;
            panel10.Location = new Point(0, 64);
            panel10.Name = "panel10";
            panel10.Padding = new Padding(0, 5, 0, 5);
            panel10.Size = new Size(213, 777);
            panel10.TabIndex = 2;
            // 
            // panelWithBorder5
            // 
            panelWithBorder5.BorderColor = Color.Gray;
            panelWithBorder5.Controls.Add(EventInput14);
            panelWithBorder5.Controls.Add(EventInput13);
            panelWithBorder5.Controls.Add(EventInput12);
            panelWithBorder5.Controls.Add(EventInput11);
            panelWithBorder5.Controls.Add(EventInput10);
            panelWithBorder5.Controls.Add(EventInput9);
            panelWithBorder5.Controls.Add(EventInput8);
            panelWithBorder5.Controls.Add(EventInput7);
            panelWithBorder5.Controls.Add(EventInput6);
            panelWithBorder5.Controls.Add(EventInput5);
            panelWithBorder5.Controls.Add(EventInput4);
            panelWithBorder5.Controls.Add(EventInput3);
            panelWithBorder5.Controls.Add(EventInput2);
            panelWithBorder5.Controls.Add(EventInput1);
            panelWithBorder5.Dock = DockStyle.Fill;
            panelWithBorder5.Location = new Point(0, 5);
            panelWithBorder5.Name = "panelWithBorder5";
            panelWithBorder5.Padding = new Padding(2);
            panelWithBorder5.Size = new Size(213, 767);
            panelWithBorder5.TabIndex = 2;
            // 
            // EventInput14
            // 
            EventInput14.ButtonName = "Button";
            EventInput14.Dock = DockStyle.Top;
            EventInput14.Location = new Point(2, 704);
            EventInput14.Name = "EventInput14";
            EventInput14.Size = new Size(209, 54);
            EventInput14.TabIndex = 19;
            // 
            // EventInput13
            // 
            EventInput13.ButtonName = "Button";
            EventInput13.Dock = DockStyle.Top;
            EventInput13.Location = new Point(2, 650);
            EventInput13.Name = "EventInput13";
            EventInput13.Size = new Size(209, 54);
            EventInput13.TabIndex = 18;
            // 
            // EventInput12
            // 
            EventInput12.ButtonName = "Button";
            EventInput12.Dock = DockStyle.Top;
            EventInput12.Location = new Point(2, 596);
            EventInput12.Name = "EventInput12";
            EventInput12.Size = new Size(209, 54);
            EventInput12.TabIndex = 17;
            // 
            // EventInput11
            // 
            EventInput11.ButtonName = "Button";
            EventInput11.Dock = DockStyle.Top;
            EventInput11.Location = new Point(2, 542);
            EventInput11.Name = "EventInput11";
            EventInput11.Size = new Size(209, 54);
            EventInput11.TabIndex = 16;
            // 
            // EventInput10
            // 
            EventInput10.ButtonName = "Button";
            EventInput10.Dock = DockStyle.Top;
            EventInput10.Location = new Point(2, 488);
            EventInput10.Name = "EventInput10";
            EventInput10.Size = new Size(209, 54);
            EventInput10.TabIndex = 15;
            // 
            // EventInput9
            // 
            EventInput9.ButtonName = "Button";
            EventInput9.Dock = DockStyle.Top;
            EventInput9.Location = new Point(2, 434);
            EventInput9.Name = "EventInput9";
            EventInput9.Size = new Size(209, 54);
            EventInput9.TabIndex = 14;
            // 
            // EventInput8
            // 
            EventInput8.ButtonName = "Button";
            EventInput8.Dock = DockStyle.Top;
            EventInput8.Location = new Point(2, 380);
            EventInput8.Name = "EventInput8";
            EventInput8.Size = new Size(209, 54);
            EventInput8.TabIndex = 13;
            // 
            // EventInput7
            // 
            EventInput7.ButtonName = "Button";
            EventInput7.Dock = DockStyle.Top;
            EventInput7.Location = new Point(2, 326);
            EventInput7.Name = "EventInput7";
            EventInput7.Size = new Size(209, 54);
            EventInput7.TabIndex = 12;
            // 
            // EventInput6
            // 
            EventInput6.ButtonName = "Button";
            EventInput6.Dock = DockStyle.Top;
            EventInput6.Location = new Point(2, 272);
            EventInput6.Name = "EventInput6";
            EventInput6.Size = new Size(209, 54);
            EventInput6.TabIndex = 11;
            // 
            // EventInput5
            // 
            EventInput5.ButtonName = "Button";
            EventInput5.Dock = DockStyle.Top;
            EventInput5.Location = new Point(2, 218);
            EventInput5.Name = "EventInput5";
            EventInput5.Size = new Size(209, 54);
            EventInput5.TabIndex = 10;
            // 
            // EventInput4
            // 
            EventInput4.ButtonName = "Button";
            EventInput4.Dock = DockStyle.Top;
            EventInput4.Location = new Point(2, 164);
            EventInput4.Name = "EventInput4";
            EventInput4.Size = new Size(209, 54);
            EventInput4.TabIndex = 9;
            // 
            // EventInput3
            // 
            EventInput3.ButtonName = "Button";
            EventInput3.Dock = DockStyle.Top;
            EventInput3.Location = new Point(2, 110);
            EventInput3.Name = "EventInput3";
            EventInput3.Size = new Size(209, 54);
            EventInput3.TabIndex = 8;
            // 
            // EventInput2
            // 
            EventInput2.ButtonName = "Button";
            EventInput2.Dock = DockStyle.Top;
            EventInput2.Location = new Point(2, 56);
            EventInput2.Name = "EventInput2";
            EventInput2.Size = new Size(209, 54);
            EventInput2.TabIndex = 7;
            // 
            // EventInput1
            // 
            EventInput1.ButtonName = "Button";
            EventInput1.Dock = DockStyle.Top;
            EventInput1.Location = new Point(2, 2);
            EventInput1.Name = "EventInput1";
            EventInput1.Size = new Size(209, 54);
            EventInput1.TabIndex = 6;
            // 
            // panelWithBorder3
            // 
            panelWithBorder3.BorderColor = Color.Gray;
            panelWithBorder3.Controls.Add(NavKeyEnabledCheckBox);
            panelWithBorder3.Controls.Add(TouchEnabledCheckBox);
            panelWithBorder3.Dock = DockStyle.Top;
            panelWithBorder3.Location = new Point(0, 5);
            panelWithBorder3.Name = "panelWithBorder3";
            panelWithBorder3.Size = new Size(213, 59);
            panelWithBorder3.TabIndex = 0;
            // 
            // NavKeyEnabledCheckBox
            // 
            NavKeyEnabledCheckBox.AutoSize = true;
            NavKeyEnabledCheckBox.Enabled = false;
            NavKeyEnabledCheckBox.Location = new Point(10, 32);
            NavKeyEnabledCheckBox.Name = "NavKeyEnabledCheckBox";
            NavKeyEnabledCheckBox.Size = new Size(151, 19);
            NavKeyEnabledCheckBox.TabIndex = 1;
            NavKeyEnabledCheckBox.Text = "Navigation Key Enabled";
            NavKeyEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // TouchEnabledCheckBox
            // 
            TouchEnabledCheckBox.AutoSize = true;
            TouchEnabledCheckBox.Enabled = false;
            TouchEnabledCheckBox.Location = new Point(10, 10);
            TouchEnabledCheckBox.Name = "TouchEnabledCheckBox";
            TouchEnabledCheckBox.Size = new Size(103, 19);
            TouchEnabledCheckBox.TabIndex = 0;
            TouchEnabledCheckBox.Text = "Touch Enabled";
            TouchEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Controls.Add(panelWithBorder4);
            panel4.Controls.Add(KeyNavPanel);
            panel4.Controls.Add(panelWithBorder6);
            panel4.Controls.Add(panelWithBorder1);
            panel4.Controls.Add(SyncDateTimeButton);
            panel4.Controls.Add(NavigateToHomeViewButton);
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(5, 5, 0, 5);
            panel4.Size = new Size(219, 841);
            panel4.TabIndex = 0;
            // 
            // panelWithBorder4
            // 
            panelWithBorder4.BorderColor = Color.Gray;
            panelWithBorder4.Controls.Add(AutoConnectDeviceNameLabel);
            panelWithBorder4.Controls.Add(AutoConnectCheckBox);
            panelWithBorder4.Controls.Add(DisconnectButton);
            panelWithBorder4.Controls.Add(label5);
            panelWithBorder4.Controls.Add(ConnectButton);
            panelWithBorder4.Controls.Add(RefreshComportBtn);
            panelWithBorder4.Controls.Add(label4);
            panelWithBorder4.Controls.Add(ComPortComboBox);
            panelWithBorder4.Dock = DockStyle.Bottom;
            panelWithBorder4.Location = new Point(5, 487);
            panelWithBorder4.Name = "panelWithBorder4";
            panelWithBorder4.Size = new Size(214, 178);
            panelWithBorder4.TabIndex = 4;
            // 
            // AutoConnectDeviceNameLabel
            // 
            AutoConnectDeviceNameLabel.AutoSize = true;
            AutoConnectDeviceNameLabel.Location = new Point(5, 152);
            AutoConnectDeviceNameLabel.Name = "AutoConnectDeviceNameLabel";
            AutoConnectDeviceNameLabel.Size = new Size(82, 15);
            AutoConnectDeviceNameLabel.TabIndex = 13;
            AutoConnectDeviceNameLabel.Text = "McsGuiDevice";
            // 
            // AutoConnectCheckBox
            // 
            AutoConnectCheckBox.AutoSize = true;
            AutoConnectCheckBox.Location = new Point(7, 128);
            AutoConnectCheckBox.Name = "AutoConnectCheckBox";
            AutoConnectCheckBox.Size = new Size(98, 19);
            AutoConnectCheckBox.TabIndex = 12;
            AutoConnectCheckBox.Text = "Auto connect";
            AutoConnectCheckBox.UseVisualStyleBackColor = true;
            // 
            // DisconnectButton
            // 
            DisconnectButton.FlatStyle = FlatStyle.Flat;
            DisconnectButton.Image = Properties.Resources.Disconnect;
            DisconnectButton.ImageAlign = ContentAlignment.MiddleLeft;
            DisconnectButton.Location = new Point(115, 88);
            DisconnectButton.Name = "DisconnectButton";
            DisconnectButton.Size = new Size(96, 24);
            DisconnectButton.TabIndex = 11;
            DisconnectButton.Text = "Disconnect";
            DisconnectButton.TextAlign = ContentAlignment.MiddleRight;
            DisconnectButton.UseVisualStyleBackColor = true;
            DisconnectButton.Click += DisconnectButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(29, 32);
            label5.Name = "label5";
            label5.Size = new Size(107, 15);
            label5.TabIndex = 10;
            label5.Text = "Refresh COM ports";
            // 
            // ConnectButton
            // 
            ConnectButton.FlatStyle = FlatStyle.Flat;
            ConnectButton.Image = Properties.Resources.Connect;
            ConnectButton.ImageAlign = ContentAlignment.MiddleLeft;
            ConnectButton.Location = new Point(4, 88);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(95, 24);
            ConnectButton.TabIndex = 9;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // RefreshComportBtn
            // 
            RefreshComportBtn.FlatStyle = FlatStyle.Flat;
            RefreshComportBtn.Image = (Image)resources.GetObject("RefreshComportBtn.Image");
            RefreshComportBtn.Location = new Point(4, 30);
            RefreshComportBtn.Name = "RefreshComportBtn";
            RefreshComportBtn.Size = new Size(20, 20);
            RefreshComportBtn.TabIndex = 8;
            RefreshComportBtn.UseVisualStyleBackColor = true;
            RefreshComportBtn.Click += RefreshComportBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(5, 6);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 6;
            label4.Text = "Connect";
            // 
            // ComPortComboBox
            // 
            ComPortComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ComPortComboBox.FormattingEnabled = true;
            ComPortComboBox.Location = new Point(3, 56);
            ComPortComboBox.Name = "ComPortComboBox";
            ComPortComboBox.Size = new Size(208, 23);
            ComPortComboBox.TabIndex = 7;
            // 
            // KeyNavPanel
            // 
            KeyNavPanel.BorderColor = Color.Gray;
            KeyNavPanel.Controls.Add(CaptureKeysCheckBox);
            KeyNavPanel.Controls.Add(KeyNavRightBtn);
            KeyNavPanel.Controls.Add(KeyNavLeftBtn);
            KeyNavPanel.Controls.Add(KeyNavOKBtn);
            KeyNavPanel.Controls.Add(KeyNavUpBtn);
            KeyNavPanel.Controls.Add(KeyNavDownBtn);
            KeyNavPanel.Dock = DockStyle.Fill;
            KeyNavPanel.Location = new Point(5, 61);
            KeyNavPanel.Name = "KeyNavPanel";
            KeyNavPanel.Size = new Size(214, 604);
            KeyNavPanel.TabIndex = 0;
            // 
            // CaptureKeysCheckBox
            // 
            CaptureKeysCheckBox.AutoSize = true;
            CaptureKeysCheckBox.Location = new Point(7, 10);
            CaptureKeysCheckBox.Name = "CaptureKeysCheckBox";
            CaptureKeysCheckBox.Size = new Size(95, 19);
            CaptureKeysCheckBox.TabIndex = 5;
            CaptureKeysCheckBox.Text = "Capture Keys";
            CaptureKeysCheckBox.UseVisualStyleBackColor = true;
            CaptureKeysCheckBox.CheckedChanged += CaptureKeysCheckBox_CheckedChanged;
            // 
            // KeyNavRightBtn
            // 
            KeyNavRightBtn.Image = Properties.Resources.IconRight;
            KeyNavRightBtn.Location = new Point(129, 77);
            KeyNavRightBtn.Name = "KeyNavRightBtn";
            KeyNavRightBtn.Size = new Size(30, 50);
            KeyNavRightBtn.TabIndex = 4;
            KeyNavRightBtn.UseVisualStyleBackColor = true;
            KeyNavRightBtn.Click += KeyNavRightBtn_Click;
            KeyNavRightBtn.MouseDown += KeyNavRightBtn_MouseDown;
            // 
            // KeyNavLeftBtn
            // 
            KeyNavLeftBtn.Image = Properties.Resources.IconLeft;
            KeyNavLeftBtn.Location = new Point(45, 77);
            KeyNavLeftBtn.Name = "KeyNavLeftBtn";
            KeyNavLeftBtn.Size = new Size(30, 50);
            KeyNavLeftBtn.TabIndex = 3;
            KeyNavLeftBtn.UseVisualStyleBackColor = true;
            KeyNavLeftBtn.Click += KeyNavLeftBtn_Click;
            KeyNavLeftBtn.MouseDown += KeyNavLeftBtn_MouseDown;
            // 
            // KeyNavOKBtn
            // 
            KeyNavOKBtn.Image = Properties.Resources.IconOK;
            KeyNavOKBtn.Location = new Point(77, 77);
            KeyNavOKBtn.Name = "KeyNavOKBtn";
            KeyNavOKBtn.Size = new Size(50, 50);
            KeyNavOKBtn.TabIndex = 2;
            KeyNavOKBtn.UseVisualStyleBackColor = true;
            KeyNavOKBtn.Click += KeyNavOKBtn_Click;
            KeyNavOKBtn.MouseDown += KeyNavOKBtn_MouseDown;
            // 
            // KeyNavUpBtn
            // 
            KeyNavUpBtn.Image = Properties.Resources.IconUp;
            KeyNavUpBtn.Location = new Point(77, 44);
            KeyNavUpBtn.Name = "KeyNavUpBtn";
            KeyNavUpBtn.Size = new Size(50, 30);
            KeyNavUpBtn.TabIndex = 1;
            KeyNavUpBtn.UseVisualStyleBackColor = true;
            KeyNavUpBtn.Click += KeyNavUpBtn_Click;
            KeyNavUpBtn.MouseDown += KeyNavUpBtn_MouseDown;
            // 
            // KeyNavDownBtn
            // 
            KeyNavDownBtn.Image = Properties.Resources.IconDown;
            KeyNavDownBtn.Location = new Point(77, 129);
            KeyNavDownBtn.Name = "KeyNavDownBtn";
            KeyNavDownBtn.Size = new Size(50, 30);
            KeyNavDownBtn.TabIndex = 0;
            KeyNavDownBtn.UseVisualStyleBackColor = true;
            KeyNavDownBtn.Click += KeyNavDownBtn_Click;
            KeyNavDownBtn.MouseDown += KeyNavDownBtn_MouseDown;
            // 
            // panelWithBorder6
            // 
            panelWithBorder6.BorderColor = Color.Gray;
            panelWithBorder6.Controls.Add(GuiImageFileLabel);
            panelWithBorder6.Controls.Add(SelectGuiImageButton);
            panelWithBorder6.Controls.Add(label2);
            panelWithBorder6.Dock = DockStyle.Bottom;
            panelWithBorder6.Location = new Point(5, 665);
            panelWithBorder6.Name = "panelWithBorder6";
            panelWithBorder6.Size = new Size(214, 92);
            panelWithBorder6.TabIndex = 1;
            // 
            // GuiImageFileLabel
            // 
            GuiImageFileLabel.AutoSize = true;
            GuiImageFileLabel.Location = new Point(7, 29);
            GuiImageFileLabel.Name = "GuiImageFileLabel";
            GuiImageFileLabel.Size = new Size(50, 15);
            GuiImageFileLabel.TabIndex = 2;
            GuiImageFileLabel.Text = "PixelFile";
            // 
            // SelectGuiImageButton
            // 
            SelectGuiImageButton.FlatStyle = FlatStyle.Flat;
            SelectGuiImageButton.Location = new Point(5, 55);
            SelectGuiImageButton.Name = "SelectGuiImageButton";
            SelectGuiImageButton.Size = new Size(75, 23);
            SelectGuiImageButton.TabIndex = 1;
            SelectGuiImageButton.Text = "Select";
            SelectGuiImageButton.UseVisualStyleBackColor = true;
            SelectGuiImageButton.Click += SelectGuiImageButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 8);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 0;
            label2.Text = "Gui File";
            // 
            // panelWithBorder1
            // 
            panelWithBorder1.BorderColor = Color.Gray;
            panelWithBorder1.Dock = DockStyle.Bottom;
            panelWithBorder1.Location = new Point(5, 757);
            panelWithBorder1.Name = "panelWithBorder1";
            panelWithBorder1.Size = new Size(214, 79);
            panelWithBorder1.TabIndex = 3;
            // 
            // SyncDateTimeButton
            // 
            SyncDateTimeButton.Dock = DockStyle.Top;
            SyncDateTimeButton.FlatStyle = FlatStyle.Flat;
            SyncDateTimeButton.Font = new Font("Segoe UI", 10F);
            SyncDateTimeButton.Image = (Image)resources.GetObject("SyncDateTimeButton.Image");
            SyncDateTimeButton.ImageAlign = ContentAlignment.MiddleLeft;
            SyncDateTimeButton.Location = new Point(5, 33);
            SyncDateTimeButton.Margin = new Padding(3, 4, 3, 3);
            SyncDateTimeButton.Name = "SyncDateTimeButton";
            SyncDateTimeButton.Size = new Size(214, 28);
            SyncDateTimeButton.TabIndex = 2;
            SyncDateTimeButton.Text = "Sync Date/Time";
            SyncDateTimeButton.UseVisualStyleBackColor = true;
            SyncDateTimeButton.Click += SyncDateTimeButton_Click;
            // 
            // NavigateToHomeViewButton
            // 
            NavigateToHomeViewButton.Dock = DockStyle.Top;
            NavigateToHomeViewButton.FlatStyle = FlatStyle.Flat;
            NavigateToHomeViewButton.Font = new Font("Segoe UI", 10F);
            NavigateToHomeViewButton.Image = (Image)resources.GetObject("NavigateToHomeViewButton.Image");
            NavigateToHomeViewButton.ImageAlign = ContentAlignment.MiddleLeft;
            NavigateToHomeViewButton.Location = new Point(5, 5);
            NavigateToHomeViewButton.Name = "NavigateToHomeViewButton";
            NavigateToHomeViewButton.Size = new Size(214, 28);
            NavigateToHomeViewButton.TabIndex = 0;
            NavigateToHomeViewButton.Text = "Home page";
            NavigateToHomeViewButton.UseVisualStyleBackColor = true;
            NavigateToHomeViewButton.Click += NavigateToHomeViewButton_Click;
            // 
            // OpenFileDialog
            // 
            OpenFileDialog.FileName = "openFileDialog1";
            // 
            // sendGuiFileToolStripMenuItem
            // 
            sendGuiFileToolStripMenuItem.Name = "sendGuiFileToolStripMenuItem";
            sendGuiFileToolStripMenuItem.Size = new Size(180, 22);
            sendGuiFileToolStripMenuItem.Text = "Send Gui File";
            sendGuiFileToolStripMenuItem.Click += SendGuiFileToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1390, 916);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(ToolPanel);
            Controls.Add(statusStrip1);
            Controls.Add(MenuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MenuStrip;
            Name = "MainWindow";
            Text = "IRL McsGui External Display";
            Load += MainWindow_Load;
            Paint += MainWindow_Paint;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            ToolPanel.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel14.ResumeLayout(false);
            panelWithBorder7.ResumeLayout(false);
            DisplayPanel.ResumeLayout(false);
            DisplayPanel.PerformLayout();
            panel16.ResumeLayout(false);
            panel17.ResumeLayout(false);
            panel15.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panelWithBorder2.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel9.ResumeLayout(false);
            OutputPanel.ResumeLayout(false);
            OutputPanel.PerformLayout();
            panel5.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panelWithBorder5.ResumeLayout(false);
            panelWithBorder3.ResumeLayout(false);
            panelWithBorder3.PerformLayout();
            panel4.ResumeLayout(false);
            panelWithBorder4.ResumeLayout(false);
            panelWithBorder4.PerformLayout();
            KeyNavPanel.ResumeLayout(false);
            KeyNavPanel.PerformLayout();
            panelWithBorder6.ResumeLayout(false);
            panelWithBorder6.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private MenuStrip MenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private Panel ToolPanel;
        private Panel panel2;
        private Panel panel3;
        private Panel panel6;
        private IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder panelWithBorder2;
        private Panel panel5;
        private IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder panelWithBorder3;
        private Panel panel4;
        private IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder KeyNavPanel;
        private Splitter splitter3;
        private Splitter splitter2;
        private Splitter splitter1;
        private RichTextBox OutputConsole;
        private Panel panel7;
        private Panel panel9;
        private Panel OutputPanel;
        private Button ClearOutputButton;
        private Label label1;
        private ToolTip ToolTip;
        private Button ToggleWordWrapButton;
        private CheckBox TouchEnabledCheckBox;
        private IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder panelWithBorder5;
        private CheckBox NavKeyEnabledCheckBox;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Panel panel10;
        private Button Button1;
        private TabPage tabPage3;
        private Button Button3;
        private Button Button2;
        private IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder panelWithBorder7;
        private Panel panel14;
        private Panel panel16;
        private Panel panel17;
        private Panel panel15;
        private Panel DisplayPanel;
        private Button Button7;
        private Button Button5;
        private Button Button6;
        private Button KeyNavRightBtn;
        private Button KeyNavLeftBtn;
        private Button KeyNavOKBtn;
        private Button KeyNavUpBtn;
        private Button KeyNavDownBtn;
        private CheckBox CaptureKeysCheckBox;
        private ComboBox SelectConsolOutputCB;
        private IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder panelWithBorder6;
        private ToolStripStatusLabel ToolStripProgressLabel;
        private Button LockConsoleButton;
        private Button SaveLogButton;
        private Button Button14;
        private Button Button13;
        private Button Button12;
        private Button Button11;
        private Button Button10;
        private Button Button9;
        private Button Button8;
        private Button Button4;
        private Label DisplayDimensionsLabel;
        private Button ScreenshotButton;
        private CustomComponents.ButtonEventInput EventInput1;
        private CustomComponents.ButtonEventInput EventInput4;
        private CustomComponents.ButtonEventInput EventInput3;
        private CustomComponents.ButtonEventInput EventInput2;
        private CustomComponents.ButtonEventInput EventInput14;
        private CustomComponents.ButtonEventInput EventInput13;
        private CustomComponents.ButtonEventInput EventInput12;
        private CustomComponents.ButtonEventInput EventInput11;
        private CustomComponents.ButtonEventInput EventInput10;
        private CustomComponents.ButtonEventInput EventInput9;
        private CustomComponents.ButtonEventInput EventInput8;
        private CustomComponents.ButtonEventInput EventInput7;
        private CustomComponents.ButtonEventInput EventInput6;
        private CustomComponents.ButtonEventInput EventInput5;
        private OpenFileDialog OpenFileDialog;
        private FolderBrowserDialog FolderBrowserDialog;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Button NavigateToHomeViewButton;
        private Button SyncDateTimeButton;
        private IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder panelWithBorder4;
        private Button DisconnectButton;
        private Label label5;
        private Button ConnectButton;
        private Button RefreshComportBtn;
        private Label label4;
        private ComboBox ComPortComboBox;
        private IRL_Image_Creator.CustomComponents.Panels.PanelWithBorder panelWithBorder1;
        private Label AutoConnectDeviceNameLabel;
        private CheckBox AutoConnectCheckBox;
        private Label GuiImageFileLabel;
        private Button SelectGuiImageButton;
        private Label label2;
        private ToolStripMenuItem sendGuiFileToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
    }
}