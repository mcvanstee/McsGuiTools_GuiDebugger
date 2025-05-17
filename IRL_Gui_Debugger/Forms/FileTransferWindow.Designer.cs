namespace IRL_Gui_Debugger.Forms
{
    partial class FileTransferWindow
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
            panel1 = new Panel();
            panel3 = new Panel();
            BytesWrittenLabel = new Label();
            FileTransferProgressBar = new ProgressBar();
            panel2 = new Panel();
            CancelButton = new Button();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(417, 249);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(BytesWrittenLabel);
            panel3.Controls.Add(FileTransferProgressBar);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(417, 199);
            panel3.TabIndex = 1;
            // 
            // BytesWrittenLabel
            // 
            BytesWrittenLabel.AutoSize = true;
            BytesWrittenLabel.Location = new Point(86, 118);
            BytesWrittenLabel.Name = "BytesWrittenLabel";
            BytesWrittenLabel.Size = new Size(0, 15);
            BytesWrittenLabel.TabIndex = 1;
            // 
            // FileTransferProgressBar
            // 
            FileTransferProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FileTransferProgressBar.Location = new Point(86, 92);
            FileTransferProgressBar.Name = "FileTransferProgressBar";
            FileTransferProgressBar.Size = new Size(234, 23);
            FileTransferProgressBar.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(CancelButton);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 199);
            panel2.Name = "panel2";
            panel2.Size = new Size(417, 50);
            panel2.TabIndex = 0;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(12, 15);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 0;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // FileTransferWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(417, 249);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FileTransferWindow";
            Text = "File Transfer";
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Label BytesWrittenLabel;
        private ProgressBar FileTransferProgressBar;
        private Button CancelButton;
    }
}