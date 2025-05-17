using System.ComponentModel;
using System.Diagnostics;

namespace IRL_Gui_Debugger.CustomComponents
{
    public partial class ButtonEventInput : UserControl
    {
        private string m_buttonName = "Button";
        public byte[] Param { get; } = Array.Empty<byte>();
        public ButtonEventInput()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        public string ButtonName
        {
            get => m_buttonName;
            set
            {
                m_buttonName = value;
                ButtonNameLabel.Text = m_buttonName;
            }
        }

        private void ParamsTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = ParamsTextBox.Text;

            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            int index = text.IndexOf(';');

            string param = text.Substring(0, index);
            Debug.WriteLine(param);
        }
    }
}
