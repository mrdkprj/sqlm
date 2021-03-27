using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MasudaManager.Utility.Preference.Setting
{
    [ToolboxItem(false)]
    partial class ListPanel : PreferencePanelBase
    {       
        Font _listFont;

        public ListPanel()
        {
            InitializeComponent();
            PrepareFormText();

            this.grpInsert.TabStop = false;
            this.grpInsert.TabIndex = MinTabIndex;
            this.grpFont.TabStop = false;
            this.grpFont.TabIndex = MinTabIndex;
            DisplayPreference();
        }

        void PrepareFormText()
        {
            this.grpFont.Text = LocalizedTextProvider.Form.PrefFont;
            this.grpInsert.Text = LocalizedTextProvider.Form.PrefDoubleClickInsert;
            this.chkInsertObjectName.Text = LocalizedTextProvider.Form.PrefInsertObjectName;
            this.chkEncloseObjectName.Text = LocalizedTextProvider.Form.PrefEncloseObjectName;
            this.chkInserPropertyValue.Text = LocalizedTextProvider.Form.PrefInsertPropertyValue;
            this.chkEnclosePropertyValue.Text = LocalizedTextProvider.Form.PrefEnclosePropertyValue;
        }

        public override void SetPanelItemTabIndex(int startIndex, int increment)
        {
            InitializeItemTabIndex(startIndex);
            this.txtFont.TabIndex = GetIncrementedTabIndex(increment);
            this.btFont.TabIndex = GetIncrementedTabIndex(increment);
            this.chkInsertObjectName.TabIndex = GetIncrementedTabIndex(increment);
            this.chkEncloseObjectName.TabIndex = GetIncrementedTabIndex(increment);
            this.chkInserPropertyValue.TabIndex = GetIncrementedTabIndex(increment);
            this.chkEnclosePropertyValue.TabIndex = GetIncrementedTabIndex(increment);
        }
        
        public override void DisplayPreference()
        {
            _listFont = UserPreference.Setting.List.Font;
            this.txtFont.Text = GetFontName(_listFont);

            this.chkInsertObjectName.Checked = UserPreference.Setting.List.InsertObjectName;
            this.chkEncloseObjectName.Checked = UserPreference.Setting.List.EncloseObjectName;
            this.chkInserPropertyValue.Checked = UserPreference.Setting.List.InsertPropertyValue;
            this.chkEnclosePropertyValue.Checked = UserPreference.Setting.List.EnclosePropertyValue;
        }

        public override void RetrievePreference()
        {
            bool settingChanged = ApplySettingRequired();

            UserPreference.Setting.List.Font = _listFont;
            UserPreference.Setting.List.InsertObjectName = this.chkInsertObjectName.Checked;
            UserPreference.Setting.List.EncloseObjectName = this.chkEncloseObjectName.Checked;
            UserPreference.Setting.List.InsertPropertyValue = this.chkInserPropertyValue.Checked;
            UserPreference.Setting.List.EnclosePropertyValue = this.chkEnclosePropertyValue.Checked;

            UserPreference.Proxy.ListViewSettingChanged = settingChanged;
        }

        protected override bool ApplySettingRequired()
        {
            if (!_listFont.Equals(UserPreference.Setting.List.Font))
                return true;

            return false;
        }

        private void btFont_Click(object sender, EventArgs e)
        {
            this.FontDialog.Font = _listFont;
            if (this.FontDialog.ShowDialog() == DialogResult.OK)
            {
                _listFont = this.FontDialog.Font;
                this.txtFont.Text = GetFontName(_listFont);
            }
        }
    }
}
