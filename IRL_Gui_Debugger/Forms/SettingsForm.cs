using IRL_Gui_Debugger.Communication.GuiDebugProtocol;
using IRL_Gui_Debugger.Logging;
using IRL_Gui_Debugger.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRL_Gui_Debugger.Forms
{
    public partial class SettingsForm : Form
    {
        private AppSettings m_settings;
        public SettingsForm(AppSettings settings)
        {
            m_settings = settings;
            InitializeComponent();
            InitBaudRateComboBox();
        }

        protected override void OnLoad(EventArgs e)
        {
            LogFolderLabel.Text = m_settings.LogDirectory;
            ScreenShotFolderLabel.Text = m_settings.ScreenShotDirectory;
            BaudRateComboBox.SelectedValue = m_settings.BaudRate;

            base.OnLoad(e);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            m_settings.Save();
            Close();
        }

        private void SelectScreenShotFolderBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new();
            DialogResult result = dialog.ShowDialog();

            if (DialogResult.OK == result)
            {
                string selectedPath = dialog.SelectedPath;

                if (Directory.Exists(selectedPath))
                {
                    m_settings.ScreenShotDirectory = selectedPath;
                    ScreenShotFolderLabel.Text = selectedPath;
                }
                else
                {
                    Logger.Error("Selected Path not exists");
                }
            }
        }

        private void SelectLogFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new();
            DialogResult result = dialog.ShowDialog();

            if (DialogResult.OK == result)
            {
                string selectedPath = dialog.SelectedPath;

                if (Directory.Exists(selectedPath))
                {
                    m_settings.LogDirectory = selectedPath;
                    LogFolderLabel.Text = selectedPath;
                }
                else
                {
                    Logger.Error("Selected Path not exists");
                }
            }
        }

        private void InitBaudRateComboBox()
        {
            Type enumType = typeof(IRLBaudRate);
            var enumValuesAndDescriptions = new ArrayList();

            foreach (var e in Enum.GetValues(enumType))
            {
                enumValuesAndDescriptions.Add(new
                {
                    EnumValue = e,
                    EnumDescription = (e.GetType().GetMember(e.ToString()).FirstOrDefault()
                    .GetCustomAttributes(typeof(DescriptionAttribute), inherit: false).FirstOrDefault()
                    as DescriptionAttribute)?.Description ?? e.ToString() //defaults to enum name if no description
                });
            }

            BaudRateComboBox.DataSource = enumValuesAndDescriptions;
            BaudRateComboBox.DisplayMember = "EnumDescription";
            BaudRateComboBox.ValueMember = "EnumValue";
        }

        private void BaudRateComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {           
            if (m_settings.BaudRate != (IRLBaudRate)BaudRateComboBox.SelectedValue)
            {
                m_settings.BaudRate = (IRLBaudRate)BaudRateComboBox.SelectedValue;
            }
        }
    }
}
