using System.Configuration;
using System.IO;
using System.Text;
using System.Drawing;
using System;
using MasudaManager.Utility.Preference.Setting;
using System.Windows.Forms;

namespace MasudaManager.Utility.Preference
{
    internal class UserPreference
    {
        static ApplicationSettings _settingInstance = ApplicationSettings.Instance;
        static ApplicationState _stateInstance = ApplicationState.Instance;

        static UserPreference _instance = new UserPreference();
        static UserPreference() { }
        private UserPreference() { }

        internal static UserPreference Proxy { get { return _instance; } }
        internal static ApplicationSettings Setting { get { return _settingInstance; } }
        internal static ApplicationState State { get { return _stateInstance; } }
        
        internal bool InputViewSettingChanged { get; set; }
        internal bool EditorSettingChanged { get; set; }
        internal bool OutputViewSettingChanged { get; set; }
        internal bool ListViewSettingChanged { get; set; }
        internal bool TabSettingChanged { get; set; }

        public void RestoreDefault()
        {
            _settingInstance.RestoreDefault();
        }

        public void ReloadSetting()
        {
            _settingInstance.Reload();
        }

        public void SaveSetting()
        {
            _settingInstance.Save();
        }

        public class Reflector
        {
            readonly static string _encloseFormat = "{0}{1}{2}";

            public static string CopyDataSeparator
            {
                get { return GetSeparator(_settingInstance.Output.CopyDataSeparatorType); }
            }

            public static string CsvFieldSeparetor
            {
                get { return GetSeparator(_settingInstance.File.CsvFieldSeparatorType); }
            }

            public static string GetEnclosedObjectName(string objectName)
            {
                return String.Format(_encloseFormat, GetEnclosure(_settingInstance.List.EncloseObjectName), objectName, GetEnclosure(_settingInstance.List.EncloseObjectName));
            }

            public static string GetEnclosedPropertyValue(string columnName)
            {
                return String.Format(_encloseFormat, GetEnclosure(_settingInstance.List.EnclosePropertyValue), columnName, GetEnclosure(_settingInstance.List.EnclosePropertyValue));
            }

            //public static string GetEnclosedCsvField(string filedValue)
            //{
            //    return String.Format(_encloseFormat, GetEnclosure(_settingInstance.File.EncloseCsvFields), filedValue, GetEnclosure(_settingInstance.File.EncloseCsvFields));
            //}

            public static FileShare FileShareMode
            {
                get
                {
                    if (_settingInstance.File.ReadLock)
                        return FileShare.None;
                    else if (_settingInstance.File.WriteLock)
                        return FileShare.Read;
                    else
                        return FileShare.ReadWrite;
                }
            }

            public static Encoding IOEncoding
            {
                get { return Encoding.GetEncoding(_settingInstance.File.IOEncodingCodePage); }
            }

            public static TabToolStripMode ToolStripMode
            {
                get
                {
                    if (!_settingInstance.Tab.EnableToolStripText)
                        return TabToolStripMode.None;

                    if (_settingInstance.Tab.ShowInputTextOnToolStrip)
                        return TabToolStripMode.InputText;
                    else
                        return TabToolStripMode.Path;
                }
            }

            static string GetEnclosure(bool enclose)
            {
                if (enclose)
                    return Constants.StringDoubleQuotation;

                return string.Empty;
            }

            static string GetSeparator(FieldSeparatorType separatorType)
            {
                switch (separatorType)
                {
                    case Preference.FieldSeparatorType.Comma:
                        return Constants.StringComma;
                    case Preference.FieldSeparatorType.Space:
                        return Constants.StringSpace;
                    case Preference.FieldSeparatorType.Tab:
                        return Constants.StringTab;
                    default:
                        return Constants.StringTab;
                }
            }
        }
    }
}
