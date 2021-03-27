using ScintillaNET;
using System;
using System.ComponentModel;

namespace MasudaManager.Utility.Preference.Setting
{
    [ToolboxItem(false)]
    public partial class EditorPanel : PreferencePanelBase
    {
        WrapMode _wrapMode;

        public EditorPanel()
        {
            InitializeComponent();
            PrepareFormText();

            this.groupBox1.TabStop = false;
            this.groupBox1.TabIndex = MaxTabIndex;
            DisplayPreference();
        }

        void PrepareFormText()
        {
            this.chkShowLineNumber.Text = LocalizedTextProvider.Form.PrefShowLineNumber;
            this.chkShowSelectionMargin.Text = LocalizedTextProvider.Form.PrefShowMargin;
            this.chkBraceHighlight.Text = LocalizedTextProvider.Form.PrefHighlightBracket;
            this.chkEnableWordwrap.Text = LocalizedTextProvider.Form.PrefEnableWordwrap;
            this.rdWorwrapChar.Text = LocalizedTextProvider.Form.PrefWordwrapChar;
            this.rdWorwrapWord.Text = LocalizedTextProvider.Form.PrefWordwrapWord;
            this.rdWorwrapSpace.Text = LocalizedTextProvider.Form.PrefWordwrapSpace;
        }

        public override void SetPanelItemTabIndex(int startIndex, int increment)
        {
            InitializeItemTabIndex(startIndex);
            this.chkShowLineNumber.TabIndex = GetIncrementedTabIndex(increment);
            this.chkShowSelectionMargin.TabIndex = GetIncrementedTabIndex(increment);
            this.chkBraceHighlight.TabIndex = GetIncrementedTabIndex(increment);
            this.chkEnableWordwrap.TabIndex = GetIncrementedTabIndex(increment);
            this.rdWorwrapChar.TabIndex = GetIncrementedTabIndex(increment);
            this.rdWorwrapWord.TabIndex = GetIncrementedTabIndex(increment);
            this.rdWorwrapSpace.TabIndex = GetIncrementedTabIndex(increment);
        }

        public override void DisplayPreference()
        {
            this.chkShowLineNumber.Checked = UserPreference.Setting.Editor.ShowLineNumber;
            this.chkShowSelectionMargin.Checked = UserPreference.Setting.Editor.ShowSelectionMargin;
            this.chkBraceHighlight.Checked = UserPreference.Setting.Editor.EnableBraceHighlight;
            this.chkEnableWordwrap.Checked = UserPreference.Setting.Editor.EnableWordwrap;
            _wrapMode = UserPreference.Setting.Editor.WordwrapMode;

            switch (_wrapMode)
            {
                case WrapMode.None:
                    this.rdWorwrapChar.Checked = true;
                    break;
                case WrapMode.Char:
                    this.rdWorwrapChar.Checked = true;
                    break;
                case WrapMode.Word:
                    this.rdWorwrapWord.Checked = true;
                    break;
                case WrapMode.Whitespace:
                    this.rdWorwrapSpace.Checked = true;
                    break;
            }
        }

        public override void RetrievePreference()
        {
            bool settingChanged = ApplySettingRequired();

            UserPreference.Setting.Editor.ShowLineNumber = this.chkShowLineNumber.Checked;
            UserPreference.Setting.Editor.ShowSelectionMargin = this.chkShowSelectionMargin.Checked;
            UserPreference.Setting.Editor.EnableBraceHighlight = this.chkBraceHighlight.Checked;
            UserPreference.Setting.Editor.EnableWordwrap = this.chkEnableWordwrap.Checked;
            UserPreference.Setting.Editor.WordwrapMode = _wrapMode;

            UserPreference.Proxy.EditorSettingChanged = settingChanged;
        }

        protected override bool ApplySettingRequired()
        {
            if (UserPreference.Setting.Editor.ShowLineNumber != this.chkShowLineNumber.Checked)
                return true;

            if (UserPreference.Setting.Editor.ShowSelectionMargin != this.chkShowSelectionMargin.Checked)
                return true;

            if (UserPreference.Setting.Editor.EnableBraceHighlight != this.chkBraceHighlight.Checked)
                return true;

            if (UserPreference.Setting.Editor.WordwrapMode != _wrapMode)
                return true;

            return false;
        }

        private void chkEnableWordwrap_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEnableWordwrap.Checked)
            {
                this.rdWorwrapChar.Enabled = true;
                this.rdWorwrapWord.Enabled = true;
                this.rdWorwrapSpace.Enabled = true;
            }
            else
            {
                _wrapMode = WrapMode.None;
                this.rdWorwrapChar.Enabled = false;
                this.rdWorwrapWord.Enabled = false;
                this.rdWorwrapSpace.Enabled = false;
            }
        }

        private void rdWorwrapChar_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdWorwrapChar.Checked)
                _wrapMode = WrapMode.Char;
        }

        private void rdWorwrapWord_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdWorwrapWord.Checked)
                _wrapMode = WrapMode.Word;
        }

        private void rdWorwrapSpace_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdWorwrapSpace.Checked)
                _wrapMode = WrapMode.Whitespace;
        }
    }
}
