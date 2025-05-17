namespace IRL_Gui_Debugger.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            SelectScreenShotFolderBtn = new Button();
            SelectLogFolderButton = new Button();
            label1 = new Label();
            ScreenShotFolderLabel = new Label();
            label3 = new Label();
            LogFolderLabel = new Label();
            CloseButton = new Button();
            FolderBrowserDialog = new FolderBrowserDialog();
            BaudRateComboBox = new ComboBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // SelectScreenShotFolderBtn
            // 
            SelectScreenShotFolderBtn.FlatStyle = FlatStyle.Flat;
            SelectScreenShotFolderBtn.Location = new Point(14, 40);
            SelectScreenShotFolderBtn.Name = "SelectScreenShotFolderBtn";
            SelectScreenShotFolderBtn.Size = new Size(75, 23);
            SelectScreenShotFolderBtn.TabIndex = 0;
            SelectScreenShotFolderBtn.Text = "Select";
            SelectScreenShotFolderBtn.UseVisualStyleBackColor = true;
            SelectScreenShotFolderBtn.Click += SelectScreenShotFolderBtn_Click;
            // 
            // SelectLogFolderButton
            // 
            SelectLogFolderButton.FlatStyle = FlatStyle.Flat;
            SelectLogFolderButton.Location = new Point(14, 99);
            SelectLogFolderButton.Name = "SelectLogFolderButton";
            SelectLogFolderButton.Size = new Size(75, 23);
            SelectLogFolderButton.TabIndex = 1;
            SelectLogFolderButton.Text = "Select";
            SelectLogFolderButton.UseVisualStyleBackColor = true;
            SelectLogFolderButton.Click += SelectLogFolderButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 22);
            label1.Name = "label1";
            label1.Size = new Size(101, 15);
            label1.TabIndex = 2;
            label1.Text = "Screenshot Folder";
            // 
            // ScreenShotFolderLabel
            // 
            ScreenShotFolderLabel.AutoSize = true;
            ScreenShotFolderLabel.Location = new Point(93, 44);
            ScreenShotFolderLabel.Name = "ScreenShotFolderLabel";
            ScreenShotFolderLabel.Size = new Size(37, 15);
            ScreenShotFolderLabel.TabIndex = 3;
            ScreenShotFolderLabel.Text = "C://...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 81);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 4;
            label3.Text = "Log Folder";
            // 
            // LogFolderLabel
            // 
            LogFolderLabel.AutoSize = true;
            LogFolderLabel.Location = new Point(92, 103);
            LogFolderLabel.Name = "LogFolderLabel";
            LogFolderLabel.Size = new Size(34, 15);
            LogFolderLabel.TabIndex = 5;
            LogFolderLabel.Text = "C://..";
            // 
            // CloseButton
            // 
            CloseButton.FlatStyle = FlatStyle.Flat;
            CloseButton.Location = new Point(323, 253);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(75, 23);
            CloseButton.TabIndex = 6;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // BaudRateComboBox
            // 
            BaudRateComboBox.FormattingEnabled = true;
            BaudRateComboBox.Location = new Point(14, 150);
            BaudRateComboBox.Name = "BaudRateComboBox";
            BaudRateComboBox.Size = new Size(121, 23);
            BaudRateComboBox.TabIndex = 7;
            BaudRateComboBox.SelectionChangeCommitted += BaudRateComboBox_SelectionChangeCommitted;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 132);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 8;
            label2.Text = "Baudrate";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 288);
            Controls.Add(label2);
            Controls.Add(BaudRateComboBox);
            Controls.Add(CloseButton);
            Controls.Add(LogFolderLabel);
            Controls.Add(label3);
            Controls.Add(ScreenShotFolderLabel);
            Controls.Add(label1);
            Controls.Add(SelectLogFolderButton);
            Controls.Add(SelectScreenShotFolderBtn);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingsForm";
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SelectScreenShotFolderBtn;
        private Button SelectLogFolderButton;
        private Label label1;
        private Label ScreenShotFolderLabel;
        private Label label3;
        private Label LogFolderLabel;
        private Button CloseButton;
        private FolderBrowserDialog FolderBrowserDialog;
        private ComboBox BaudRateComboBox;
        private Label label2;
    }
}