using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;
using ScintillaNET;

namespace MasudaManager.Utility.Preference.Setting
{
    [SettingsGroupName("EditorSetting")]
    internal class EditorSetting : SettingBase
    {
        private static EditorSetting _instance = new EditorSetting();
        static EditorSetting() { }
        private EditorSetting() { }

        internal static EditorSetting Instance
        {
            get { return _instance; }
        }

        public override void RestoreDefault()
        {
            foreach (SettingsProperty prop in this.Properties)
            {
                this.PropertyValues[prop.Name].PropertyValue = GetDefaultValue(prop);
            }
        }

        object GetDefaultValue(SettingsProperty prop)
        {
            if (prop.PropertyType.IsEnum)
                return Enum.Parse(prop.PropertyType, prop.DefaultValue.ToString());

            return Convert.ChangeType(prop.DefaultValue, prop.PropertyType);
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool ShowLineNumber
        {
            get
            {
                return (bool)this["ShowLineNumber"];
            }
            set
            {
                this["ShowLineNumber"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool ShowSelectionMargin
        {
            get
            {
                return (bool)this["ShowSelectionMargin"];
            }
            set
            {
                this["ShowSelectionMargin"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool EnableWordwrap
        {
            get
            {
                return (bool)this["EnableWordwrap"];
            }
            set
            {
                this["EnableWordwrap"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Char")]
        public WrapMode WordwrapMode
        {
            get
            {
                return (WrapMode)this["WordwrapMode"];
            }
            set
            {
                this["WordwrapMode"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool EnableBraceHighlight
        {
            get
            {
                return (bool)this["EnableBraceHighlight"];
            }
            set
            {
                this["EnableBraceHighlight"] = value;
            }
        }
    }
}
