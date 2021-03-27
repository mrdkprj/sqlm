using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.Preference.Setting
{
    [SettingsGroupName("ListSetting")]
    internal class ListSetting : SettingBase
    {
        FontConverter _fontConverter = new FontConverter();

        static ListSetting _instance = new ListSetting();
        static ListSetting() { }
        private ListSetting() { }

        internal static ListSetting Instance
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
        public bool InsertObjectName
        {
            get
            {
                return (bool)this["InsertObjectName"];
            }
            internal set
            {
                this["InsertObjectName"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool InsertPropertyValue
        {
            get
            {
                return (bool)this["InsertPropertyValue"];
            }
            internal set
            {
                this["InsertPropertyValue"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool EncloseObjectName
        {
            get
            {
                return (bool)this["EncloseObjectName"];
            }
            internal set
            {
                this["EncloseObjectName"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool EnclosePropertyValue
        {
            get
            {
                return (bool)this["EnclosePropertyValue"];
            }
            internal set
            {
                this["EnclosePropertyValue"] = value;
            }
        }
    }
}
