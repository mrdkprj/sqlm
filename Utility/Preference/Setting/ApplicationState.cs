using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Drawing;

namespace MasudaManager.Utility.Preference.Setting
{
    [SettingsGroupName("ApplicationState")]
    internal class ApplicationState : ApplicationSettingsBase
    {
        static ApplicationState _instance = new ApplicationState();
        static ApplicationState() { }
        private ApplicationState() { }

        internal static ApplicationState Instance
        {
            get { return _instance; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("null, null, null, false, null")]
        public ConnectionData LastDbConnection
        {
            get
            {
                return (ConnectionData)this["LastDbConnection"];
            }
            set
            {
                this["LastDbConnection"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("100, 100, 949, 608")]
        public Rectangle LastClientSize
        {
            get
            {
                return (Rectangle)this["LastClientSize"];
            }
            set
            {
                this["LastClientSize"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Normal")]
        public System.Windows.Forms.FormWindowState LastWindowState
        {
            get
            {
                return (System.Windows.Forms.FormWindowState)this["LastWindowState"];
            }
            set
            {
                this["LastWindowState"] = value;
            }
        }
    }
}
