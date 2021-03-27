using System.Configuration;
using System.IO;
using System.Text;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace MasudaManager.Utility.Preference.Setting
{
    internal class ApplicationSettings
    {
        List<SettingBase> _settings = new List<SettingBase>();

        static ApplicationSettings _instance = new ApplicationSettings();
        static ApplicationSettings() { }
        private ApplicationSettings()
        {
            
            _settings.Add(TabSetting.Instance);
            _settings.Add(InputSetting.Instance);
            _settings.Add(EditorColorSetting.Instance);
            _settings.Add(EditorSetting.Instance);
            _settings.Add(OutputSetting.Instance);
            _settings.Add(ListSetting.Instance);
            _settings.Add(SqlSetting.Instance);
            _settings.Add(FileSetting.Instance);
        }

        internal static ApplicationSettings Instance
        {
            get { return _instance; }
        }

        public TabSetting Tab { get { return TabSetting.Instance; } }
        public InputSetting Input { get { return InputSetting.Instance; } }
        public EditorColorSetting EditorColor { get { return EditorColorSetting.Instance; } }
        public EditorSetting Editor { get { return EditorSetting.Instance; } }
        public OutputSetting Output { get { return OutputSetting.Instance; } }
        public ListSetting List { get { return ListSetting.Instance; } }
        public SqlSetting Sql { get { return SqlSetting.Instance; } }
        public FileSetting File { get { return FileSetting.Instance; } }

        public void RestoreDefault()
        {
            foreach (var setting in _settings)
                setting.RestoreDefault();
        }

        public void Reload()
        {
            foreach (var setting in _settings)
                setting.Reload();
        }

        public void Save()
        {
            foreach (var setting in _settings)
                setting.Save();
        }
    }
}
