using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.Preference.Setting
{
    [SettingsGroupName("FileSetting")]
    internal class FileSetting : SettingBase
    {
        private static FileSetting _instance = new FileSetting();
        static FileSetting() { }
        private FileSetting() { }

        internal static FileSetting Instance
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
        public bool WriteLock
        {
            get
            {
                return (bool)this["WriteLock"];
            }
            internal set
            {
                this["WriteLock"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool ReadLock
        {
            get
            {
                return (bool)this["ReadLock"];
            }
            internal set
            {
                this["ReadLock"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("65001")]
        public int IOEncodingCodePage
        {
            get
            {
                return (int)this["IOEncodingCodePage"];
            }
            internal set
            {
                this["IOEncodingCodePage"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Comma")]
        public FieldSeparatorType CsvFieldSeparatorType
        {
            get
            {
                return (FieldSeparatorType)this["CsvFieldSeparatorType"];
            }
            internal set
            {
                this["CsvFieldSeparatorType"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool EncloseCsvFields
        {
            get
            {
                return (bool)this["EncloseCsvFields"];
            }
            internal set
            {
                this["EncloseCsvFields"] = value;
            }
        }
    }
}
