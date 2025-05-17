namespace IRL_Gui_Debugger.CustomComponents
{
    partial class ButtonEventInput
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Button1Params = new Label();
            ParamsTextBox = new TextBox();
            ButtonNameLabel = new Label();
            SuspendLayout();
            // 
            // Button1Params
            // 
            Button1Params.AutoSize = true;
            Button1Params.Location = new Point(4, 29);
            Button1Params.Name = "Button1Params";
            Button1Params.Size = new Size(69, 15);
            Button1Params.TabIndex = 5;
            Button1Params.Text = "Parameters:";
            // 
            // ParamsTextBox
            // 
            ParamsTextBox.Location = new Point(75, 25);
            ParamsTextBox.Name = "ParamsTextBox";
            ParamsTextBox.Size = new Size(127, 23);
            ParamsTextBox.TabIndex = 4;
            ParamsTextBox.TextChanged += ParamsTextBox_TextChanged;
            // 
            // ButtonNameLabel
            // 
            ButtonNameLabel.AutoSize = true;
            ButtonNameLabel.Location = new Point(4, 7);
            ButtonNameLabel.Name = "ButtonNameLabel";
            ButtonNameLabel.Size = new Size(52, 15);
            ButtonNameLabel.TabIndex = 3;
            ButtonNameLabel.Text = "Button 1";
            // 
            // ButtonEventInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Button1Params);
            Controls.Add(ParamsTextBox);
            Controls.Add(ButtonNameLabel);
            Name = "ButtonEventInput";
            Size = new Size(208, 54);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Button1Params;
        private TextBox ParamsTextBox;
        private Label ButtonNameLabel;
    }
}
