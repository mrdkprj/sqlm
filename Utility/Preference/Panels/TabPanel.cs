using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MasudaManager.Utility.Preference.Setting
{
    [ToolboxItem(false)]
    public partial class TabPanel : PreferencePanelBase
    {
        Font _tabFont;
        
        public TabPanel()
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
            this.chkShowToolstrip.Text = LocalizedTextProvider.Form.PrefShowToolStrip;
            this.rdShowText.Text = LocalizedTextProvider.Form.PrefShowInputText;
            this.rdShowFilePath.Text = LocalizedTextProvider.Form.PrefShowFilePath;
        }

        public override void SetPanelItemTabIndex(int startIndex, int increment)
        {
            InitializeItemTabIndex(startIndex);
            this.txtFont.TabIndex = GetIncrementedTabIndex(increment);
            this.btFont.TabIndex = GetIncrementedTabIndex(increment);
            this.chkShowToolstrip.TabIndex = GetIncrementedTabIndex(increment);
            this.rdShowText.TabIndex = GetIncrementedTabIndex(increment);
            this.rdShowFilePath.TabIndex = GetIncrementedTabIndex(increment);
        }
        
        public override void DisplayPreference()
        {
            _tabFont = UserPreference.Setting.Tab.Font;
            this.txtFont.Text = GetFontName(_tabFont);

            this.chkShowToolstrip.Checked = UserPreference.Setting.Tab.EnableToolStripText;
            this.rdShowText.Checked = UserPreference.Setting.Tab.ShowInputTextOnToolStrip;
            this.rdShowFilePath.Checked = UserPreference.Setting.Tab.ShowFilePathOnToolStrip;
        }

        public override void RetrievePreference()
        {
            bool settingChanged = ApplySettingRequired();

            UserPreference.Setting.Tab.Font = _tabFont;
            UserPreference.Setting.Tab.EnableToolStripText = this.chkShowToolstrip.Checked;
            UserPreference.Setting.Tab.ShowInputTextOnToolStrip = this.rdShowText.Checked;
            UserPreference.Setting.Tab.ShowFilePathOnToolStrip = this.rdShowFilePath.Checked;

            UserPreference.Proxy.TabSettingChanged = settingChanged;
        }

        protected override bool ApplySettingRequired()
        {
            if (!_tabFont.Equals(UserPreference.Setting.Tab.Font))
                return true;

            if (this.chkShowToolstrip.Checked != UserPreference.Setting.Tab.EnableToolStripText)
                return true;

            if (this.rdShowText.Checked != UserPreference.Setting.Tab.ShowInputTextOnToolStrip)
                return true;

            if (this.rdShowFilePath.Checked != UserPreference.Setting.Tab.ShowFilePathOnToolStrip)
                return true;
            
            return false;
        }
         
        private void chkShowToolstrip_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkShowToolstrip.Checked)
            {
                this.rdShowText.Enabled = true;
                this.rdShowFilePath.Enabled = true;
            }
            else
            {
                this.rdShowText.Enabled = false;
                this.rdShowFilePath.Enabled = false;
            }
        }

        private void btFont_Click(object sender, EventArgs e)
        {
            this.FontDialog.Font = _tabFont;
            if (this.FontDialog.ShowDialog() == DialogResult.OK)
            {
                _tabFont = this.FontDialog.Font;
                this.txtFont.Text = GetFontName(_tabFont);
            }
        }
    }
}
