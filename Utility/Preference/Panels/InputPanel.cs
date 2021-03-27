using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MasudaManager.Utility.Preference.Setting
{
    [ToolboxItem(false)]
    public partial class InputPanel : PreferencePanelBase
    {
        Font _inputFont;

        public InputPanel()
        {
            InitializeComponent();
            PrepareFormText();

            this.groupBox1.TabStop = false;
            this.groupBox1.TabIndex = MinTabIndex;
            this.grpFont.TabStop = false;
            this.grpFont.TabIndex = MinTabIndex;
            DisplayPreference();
        }

        void PrepareFormText()
        {
            this.grpFont.Text = LocalizedTextProvider.Form.PrefFont;
            this.chkUseAssistant.Text = LocalizedTextProvider.Form.PrefShowInputSupport;
            this.chkShowObjectList.Text = LocalizedTextProvider.Form.PrefEnableTableNameSupport;
            this.chkShowColumnList.Text = LocalizedTextProvider.Form.PrefEnableColumnNameSupport;
        }

        public override void SetPanelItemTabIndex(int startIndex, int increment)
        {
            InitializeItemTabIndex(startIndex);
            this.txtFont.TabIndex = GetIncrementedTabIndex(increment);
            this.btFont.TabIndex = GetIncrementedTabIndex(increment);
            this.chkUseAssistant.TabIndex = GetIncrementedTabIndex(increment); ;
            this.chkShowObjectList.TabIndex = GetIncrementedTabIndex(increment);
            this.chkShowColumnList.TabIndex = GetIncrementedTabIndex(increment);
        }
        
        public override void DisplayPreference()
        {
            _inputFont = UserPreference.Setting.Input.Font;
            this.txtFont.Text = GetFontName(_inputFont);

            this.chkUseAssistant.Checked = UserPreference.Setting.Input.UseAssistant;
            this.chkShowObjectList.Checked = UserPreference.Setting.Input.ShowTableNameSupport;
            this.chkShowColumnList.Checked = UserPreference.Setting.Input.ShowColumnNameSupport;
        }

        public override void RetrievePreference()
        {
            bool settingChanged = ApplySettingRequired();

            UserPreference.Setting.Input.Font = _inputFont;
            UserPreference.Setting.Input.UseAssistant = this.chkUseAssistant.Checked;
            UserPreference.Setting.Input.ShowTableNameSupport = this.chkShowObjectList.Checked;
            UserPreference.Setting.Input.ShowColumnNameSupport = this.chkShowColumnList.Checked;

            UserPreference.Proxy.InputViewSettingChanged = settingChanged;
        }

        protected override bool ApplySettingRequired()
        {
            if (!_inputFont.Equals(UserPreference.Setting.Input.Font))
                return true;

            return false;
        }

        private void chkUseAssistant_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkUseAssistant.Checked)
            {
                this.chkShowObjectList.Enabled = true;
                this.chkShowColumnList.Enabled = true;
                this.chkShowObjectList.Checked = true;
                this.chkShowColumnList.Checked = true;
            }
            else
            {
                this.chkShowObjectList.Enabled = false;
                this.chkShowColumnList.Enabled = false;
                this.chkShowObjectList.Checked = false;
                this.chkShowColumnList.Checked = false;
            }
        }

        private void btFont_Click(object sender, EventArgs e)
        {
            this.FontDialog.Font = _inputFont;
            if (this.FontDialog.ShowDialog() == DialogResult.OK)
            {
                _inputFont = this.FontDialog.Font;
                this.txtFont.Text = GetFontName(_inputFont);
            }
        }
    }
}
