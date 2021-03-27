using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.Preference.Setting
{
    [SettingsGroupName("SqlSetting")]
    internal class SqlSetting : SettingBase
    {
        static SqlSetting _instance = new SqlSetting();
        static SqlSetting() { }
        private SqlSetting() { }

        internal static SqlSetting Instance
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
        [DefaultSettingValue("true")]
        public bool DisplayProgress
        {
            get
            {
                return (bool)this["DisplayProgress"];
            }
            internal set
            {
                this["DisplayProgress"] = value;
            }
        }
        
        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool AllowAutoCommit
        {
            get
            {
                return (bool)this["AllowAutoCommit"];
            }
            internal set
            {
                this["AllowAutoCommit"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool RunAfterSemicolon
        {
            get
            {
                return (bool)this["RunAfterSemicolon"];
            }
            internal set
            {
                this["RunAfterSemicolon"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool ContinueAfterError
        {
            get
            {
                return (bool)this["ContinueAfterError"];
            }
            internal set
            {
                this["ContinueAfterError"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("30")]
        public int CommandTimeout
        {
            get
            {
                return (int)this["CommandTimeout"];
            }
            internal set
            {
                this["CommandTimeout"] = value;
            }
        }
    }
}
