using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.Preference.Setting
{
    [SettingsGroupName("OutputSetting")]
    internal class OutputSetting : SettingBase
    {
        FontConverter _fontConverter = new FontConverter();

        static OutputSetting _instance = new OutputSetting();
        static OutputSetting() { }
        private OutputSetting() { }

        internal static OutputSetting Instance
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
        [DefaultSettingValue("false")]
        public bool DisplaySpaceCharacter
        {
            get
            {
                return (bool)this["DisplaySpaceCharacter"];
            }
            internal set
            {
                this["DisplaySpaceCharacter"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool DisplayRowNumber
        {
            get
            {
                return (bool)this["DisplayRowNumber"];
            }
            internal set
            {
                this["DisplayRowNumber"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Tab")]
        public FieldSeparatorType CopyDataSeparatorType
        {
            get
            {
                return (FieldSeparatorType)this["CopyDataSeparatorType"];
            }
            internal set
            {
                this["CopyDataSeparatorType"] = value;
            }
        }
    }
}
