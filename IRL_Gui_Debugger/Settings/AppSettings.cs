using IRL_Gui_Debugger.Communication.GuiDebugProtocol;
using System.Configuration;

namespace IRL_Gui_Debugger.Settings
{
    public class AppSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string ScreenShotDirectory
        {
            get
            {
                return (string)this[nameof(ScreenShotDirectory)];
            }
            set
            {
                this[nameof(ScreenShotDirectory)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string LogDirectory
        {
            get
            {
                return (string)this[nameof(LogDirectory)];
            }
            set
            {
                this[nameof(LogDirectory)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string GuiImageDirectory
        {
            get
            {
                return (string)this[nameof(GuiImageDirectory)];
            }
            set
            {
                this[nameof(GuiImageDirectory)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue(nameof(IRLBaudRate.BaudRate921600))]
        public IRLBaudRate BaudRate
        {
            get
            {
                return (IRLBaudRate)this[nameof(BaudRate)];
            }
            set
            {
                this[nameof(BaudRate)] = value;
            }
        }
    }
}
