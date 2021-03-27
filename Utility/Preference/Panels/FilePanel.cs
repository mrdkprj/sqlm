using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MasudaManager.Utility.Preference.Setting
{

    [ToolboxItem(false)]
    public partial class FilePanel : PreferencePanelBase
    {
        readonly string _displayMemberName = "Value";
        readonly string _valueMemberName = "Key";

        Dictionary<int, string> _encodingDictionary = new Dictionary<int, string>();

        public FilePanel()
        {
            InitializeComponent();
            PrepareFormText();

            this.grpExportImport.TabStop = false;
            this.grpExportImport.TabIndex = MinTabIndex;
            this.grpOpenMode.TabStop = false;
            this.grpOpenMode.TabIndex = MinTabIndex;
            this.grpEncoding.TabStop = false;
            this.grpEncoding.TabIndex = MinTabIndex;

            this.cmbSeparator.DataSource = Enum.GetValues(typeof(FieldSeparatorType));

            foreach (var value in Enum.GetValues(typeof(EncodingType)))
            {
                _encodingDictionary.Add((int)value, value.ToString());
            }

            this.cmbEncoding.DataSource = new BindingSource(_encodingDictionary, null);
            this.cmbEncoding.DisplayMember = _displayMemberName;
            this.cmbEncoding.ValueMember = _valueMemberName;

            DisplayPreference();
        }

        void PrepareFormText()
        {
            this.grpEncoding.Text = LocalizedTextProvider.Form.PrefEncoding;
            this.grpOpenMode.Text = LocalizedTextProvider.Form.PrefFileOpenMode;
            this.chkWriteLock.Text = LocalizedTextProvider.Form.PrefOpenWriteLock;
            this.chkReadLock.Text = LocalizedTextProvider.Form.PrefOpenReadLock;
            this.grpExportImport.Text = LocalizedTextProvider.Form.PrefExportImport;
            this.lblSeparator.Text = LocalizedTextProvider.Form.PrefSeparator;
            this.chkEncloseFields.Text = LocalizedTextProvider.Form.PrefEncloseFields;
        }

        public override void SetPanelItemTabIndex(int startIndex, int increment)
        {
            InitializeItemTabIndex(startIndex);
            this.cmbEncoding.TabIndex = GetIncrementedTabIndex(increment);
            this.chkWriteLock.TabIndex = GetIncrementedTabIndex(increment);
            this.chkReadLock.TabIndex = GetIncrementedTabIndex(increment);
            this.cmbSeparator.TabIndex = GetIncrementedTabIndex(increment);
            this.chkEncloseFields.TabIndex = GetIncrementedTabIndex(increment);
        }
        
        public override void DisplayPreference()
        {
            this.chkWriteLock.Checked = UserPreference.Setting.File.WriteLock;
            this.chkReadLock.Checked = UserPreference.Setting.File.ReadLock;
            this.cmbEncoding.SelectedValue = UserPreference.Setting.File.IOEncodingCodePage;
            this.cmbSeparator.SelectedItem = UserPreference.Setting.File.CsvFieldSeparatorType;
            this.chkEncloseFields.Checked = UserPreference.Setting.File.EncloseCsvFields;
        }

        public override void RetrievePreference()
        {
            UserPreference.Setting.File.WriteLock = this.chkWriteLock.Checked;
            UserPreference.Setting.File.ReadLock = this.chkReadLock.Checked;
            UserPreference.Setting.File.IOEncodingCodePage = (int)this.cmbEncoding.SelectedValue;
            UserPreference.Setting.File.CsvFieldSeparatorType = (FieldSeparatorType)this.cmbSeparator.SelectedItem;
            UserPreference.Setting.File.EncloseCsvFields = this.chkEncloseFields.Checked;
        }

    }
}
