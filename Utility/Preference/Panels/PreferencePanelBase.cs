using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MasudaManager.Utility.Preference.Setting
{
   
    [ToolboxItem(false)]
    public partial class PreferencePanelBase : UserControl
    {
        readonly int _maxTabIndex = 9999;
        readonly int _minTabIndex = 0;
        readonly string _fontNameFormat = "Name={0}, Size={1}";
        FontDialog _fontDialog = new FontDialog();
        int _itemTabIndex = 0;

        public PreferencePanelBase()
        {
            InitializeComponent();
        }

        protected internal FontDialog FontDialog { get { return _fontDialog; } }
        protected int MaxTabIndex { get { return _maxTabIndex; } }
        protected int MinTabIndex { get { return _minTabIndex; } }

        public virtual void SetPanelItemTabIndex(int startIndex, int increment)
        {
        }

        public virtual void DisplayPreference() { }

        public virtual void RetrievePreference() { }

        protected virtual bool ApplySettingRequired() { return false; }

        protected void InitializeItemTabIndex(int startIndex)
        {
            _itemTabIndex = startIndex;
        }

        protected int GetIncrementedTabIndex(int increment)
        {
            _itemTabIndex += increment;
            return _itemTabIndex;
        }

        protected internal string GetFontName(Font font)
        {
            return String.Format(_fontNameFormat, font.Name, font.Size);
        }
    }
}
