using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MasudaManager.Utility.Preference.Setting
{
    [ToolboxItem(false)]
    public partial class OutputPanel : PreferencePanelBase
    {
        Font _gridFont;

        public OutputPanel()
        {
            InitializeComponent();
            PrepareFormText();

            this.grpFont.TabStop = false;
            this.grpFont.TabIndex = MinTabIndex;
            this.grpCopyFormat.TabStop = false;
            this.grpCopyFormat.TabIndex = MinTabIndex;
            this.cmbSeparator.DataSource = Enum.GetValues(typeof(FieldSeparatorType));

            DisplayPreference();
        }

        void PrepareFormText()
        {
            this.grpFont.Text = LocalizedTextProvider.Form.PrefFont;
            this.grpCopyFormat.Text = LocalizedTextProvider.Form.PrefCopyDataFormat;
            this.lblSeparator.Text = LocalizedTextProvider.Form.PrefSeparator;
            this.chkDisplayRowNumber.Text = LocalizedTextProvider.Form.PrefDisplayRowNumber;
            this.chkDisplaySpace.Text = LocalizedTextProvider.Form.PrefDisplaySpace;
        }

        public override void SetPanelItemTabIndex(int startIndex, int increment)
        {
            InitializeItemTabIndex(startIndex);
            this.txtFont.TabIndex = GetIncrementedTabIndex(increment);
            this.btFont.TabIndex = GetIncrementedTabIndex(increment);
            this.cmbSeparator.TabIndex = GetIncrementedTabIndex(increment);
            this.chkDisplayRowNumber.TabIndex = GetIncrementedTabIndex(increment);
            this.chkDisplaySpace.TabIndex = GetIncrementedTabIndex(increment);
        }
        
        public override void DisplayPreference()
        {
            _gridFont = UserPreference.Setting.Output.Font;
            this.txtFont.Text = GetFontName(_gridFont);

            this.chkDisplayRowNumber.Checked = UserPreference.Setting.Output.DisplayRowNumber;
            this.chkDisplaySpace.Checked = UserPreference.Setting.Output.DisplaySpaceCharacter;
            this.cmbSeparator.SelectedItem = UserPreference.Setting.Output.CopyDataSeparatorType;
        }

        public override void RetrievePreference()
        {
            bool settingChanged = ApplySettingRequired();

            UserPreference.Setting.Output.Font = _gridFont;
            UserPreference.Setting.Output.DisplayRowNumber = this.chkDisplayRowNumber.Checked;
            UserPreference.Setting.Output.DisplaySpaceCharacter = this.chkDisplaySpace.Checked;
            UserPreference.Setting.Output.CopyDataSeparatorType = (FieldSeparatorType)this.cmbSeparator.SelectedItem;

            UserPreference.Proxy.OutputViewSettingChanged = settingChanged;
        }

        protected override bool ApplySettingRequired()
        {
            if (!_gridFont.Equals(UserPreference.Setting.Output.Font))
                return true;

            if (UserPreference.Setting.Output.DisplayRowNumber != this.chkDisplayRowNumber.Checked)
                return true;

            if (UserPreference.Setting.Output.DisplaySpaceCharacter != this.chkDisplaySpace.Checked)
                return true;

            return false;
        }

        private void btFont_Click(object sender, EventArgs e)
        {
            this.FontDialog.Font = _gridFont;
            if (this.FontDialog.ShowDialog() == DialogResult.OK)
            {
                _gridFont = this.FontDialog.Font;
                this.txtFont.Text = GetFontName(_gridFont);
            }
        }
    }
}
