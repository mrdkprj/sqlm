using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.Preference.Setting
{
    [SettingsGroupName("EditorColorSetting")]
    internal class EditorColorSetting : SettingBase
    {
        ColorConverter _colorConverter = new ColorConverter();

        static EditorColorSetting _instance = new EditorColorSetting();
        static EditorColorSetting() { }
        private EditorColorSetting() { }

        internal static EditorColorSetting Instance
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

            if (prop.PropertyType.Equals(typeof(Color)))
                return _colorConverter.ConvertFromString(prop.DefaultValue.ToString());

            return Convert.ChangeType(prop.DefaultValue, prop.PropertyType);
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Gray")]
        public Color CommentColor
        {
            get
            {
                return (Color)this["CommentColor"];
            }
            set
            {
                this["CommentColor"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Olive")]
        public Color CharColor
        {
            get
            {
                return (Color)this["CharColor"];
            }
            set
            {
                this["CharColor"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Black")]
        public Color NumberColor
        {
            get
            {
                return (Color)this["NumberColor"];
            }
            set
            {
                this["NumberColor"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Black")]
        public Color OperatorColor
        {
            get
            {
                return (Color)this["OperatorColor"];
            }
            set
            {
                this["OperatorColor"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Black")]
        public Color StringColor
        {
            get
            {
                return (Color)this["StringColor"];
            }
            set
            {
                this["StringColor"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Blue")]
        public Color KeywordColor
        {
            get
            {
                return (Color)this["KeywordColor"];
            }
            set
            {
                this["KeywordColor"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("BlueViolet")]
        public Color MatchingBracketForeColor
        {
            get
            {
                return (Color)this["MatchingBracketForeColor"];
            }
            set
            {
                this["MatchingBracketForeColor"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("LightGray")]
        public Color MatchingBracketBackColor
        {
            get
            {
                return (Color)this["MatchingBracketBackColor"];
            }
            set
            {
                this["MatchingBracketBackColor"] = value;
            }
        }
    }
}
