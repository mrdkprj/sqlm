using System;
using System.Configuration;
using System.Drawing;

namespace MasudaManager.Utility.Preference.Setting
{
    [SettingsGroupName("TabSetting")]
    internal class TabSetting : SettingBase
    {
         FontConverter _fontConverter = new FontConverter();

        static TabSetting _instance = new TabSetting();
        static TabSetting() { }
        private TabSetting() { }

        internal static TabSetting Instance
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
        [DefaultSettingValue("MS UI Gothic, 9pt")]
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
        public bool EnableToolStripText
        {
            get
            {
                return (bool)this["EnableToolStripText"];
            }
            internal set
            {
                this["EnableToolStripText"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool ShowInputTextOnToolStrip
        {
            get
            {
                return (bool)this["ShowInputTextOnToolStrip"];
            }
            internal set
            {
                this["ShowInputTextOnToolStrip"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool ShowFilePathOnToolStrip
        {
            get
            {
                return (bool)this["ShowFilePathOnToolStrip"];
            }
            internal set
            {
                this["ShowFilePathOnToolStrip"] = value;
            }
        }  
    }
}
