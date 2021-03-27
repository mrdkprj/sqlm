using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.Preference.Setting
{
    [SettingsGroupName("InputSetting")]
    internal class InputSetting : SettingBase
    {
        FontConverter _fontConverter = new FontConverter();

        static InputSetting _instance = new InputSetting();
        static InputSetting() { }
        private InputSetting() { }

        internal static InputSetting Instance
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

            if (prop.PropertyType.Equals(typeof(Font)))
                return _fontConverter.ConvertFromString(prop.DefaultValue.ToString());

            return Convert.ChangeType(prop.DefaultValue, prop.PropertyType);
        }

        [UserScopedSetting()]
        [DefaultSettingValue("ＭＳ ゴシック, 10pt")]
        public Font Font
        {
            get
            {
                return (Font)this["Font"];
            }
            internal set
            {
                this["Font"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool UseAssistant
        {
            get
            {
                return (bool)this["UseAssistant"];
            }
            internal set
            {
                this["UseAssistant"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool ShowTableNameSupport
        {
            get
            {
                return (bool)this["ShowTableNameSupport"];
            }
            internal set
            {
                this["ShowTableNameSupport"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool ShowColumnNameSupport
        {
            get
            {
                return (bool)this["ShowColumnNameSupport"];
            }
            internal set
            {
                this["ShowColumnNameSupport"] = value;
            }
        }
    }
}
