using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MasudaManager.Views
{

    public partial class ImportView : ImportViewSlice, IImportView, IChildView<ExportImportParameter>
    {
        IWin32Window _owner;
        OpenFileDialog _dialog = new OpenFileDialog();

        public event EventHandler<GenericEventArgs<ExportImportParameter>> Initiated;
        public event EventHandler ImportFileTextChanged;
        public event EventHandler FileSelectButtonClicked;
        public event EventHandler ImportFormatChanged;
        public event EventHandler ImportButtonClicked;
        public event EventHandler CancelButtonClicked;
        public event EventHandler<CancelEventArgs> ViewClosing;
        public event EventHandler<CancelEventArgs> ReleaseRequested;

        public bool SuppressFormClosingEvent { get; set; }
        public Action ViewClosedAction { get; set; }

        public ImportView()
        {
            InitializeComponent();
            PrepareFormText();

            SetTabIndex();
            this.cmbFormat.SelectedIndexChanged += cmbFormat_SelectedIndexChanged;
            this.rdNoHeader.Checked = true;
        }

        #region EventHandlers

        #region Closing
        private void ImportView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.SuppressFormClosingEvent)
                return;

            ViewClosing(sender, e);
        }

        #endregion

        #region Select file button click
        private void btSelectFile_Click(object sender, EventArgs e)
        {
            FileSelectButtonClicked(sender, e);
        }
        #endregion

        #region Import format changed
        void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImportFormatChanged(sender, e);
        }
        #endregion

        #region Import text changed
        private void txtImportFile_TextChanged(object sender, EventArgs e)
        {
            ImportFileTextChanged(sender, e);
        }
        #endregion

        #region Import button click
        private void btImport_Click(object sender, EventArgs e)
        {
            ImportButtonClicked(sender, e);
        }
        #endregion

        #region Cacnel button click
        private void btCancel_Click(object sender, EventArgs e)
        {
            CancelButtonClicked(sender, e);
        }
        #endregion

        #region Close button click
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region ProcessDialogKey
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
            }

            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #endregion

        #region Methods

        #region PrepareFormText
        void PrepareFormText()
        {
            this.Text = LocalizedTextProvider.Form.Import;
            this.lblTable.Text = LocalizedTextProvider.Form.TableName;
            this.lblFile.Text = LocalizedTextProvider.Form.File;
            this.grpImportOption.Text = LocalizedTextProvider.Form.ImportOption;
            this.lblFormat.Text = LocalizedTextProvider.Form.Format;
            this.rdNoHeader.Text = LocalizedTextProvider.Form.WithoutHeader;
            this.rdWithHeader.Text = LocalizedTextProvider.Form.WithColumnNameHeader;
            this.btImport.Text = LocalizedTextProvider.Form.ImportToolTip;
            this.btCancel.Text = LocalizedTextProvider.Form.Cancel;
            this.btClose.Text = LocalizedTextProvider.Form.Close;
        }
        #endregion

        #region SetTabIndex
        void SetTabIndex()
        {
            this.txtImportFile.TabIndex = 0;
            this.btSelectFile.TabIndex = 1;
            this.grpImportOption.TabIndex = 2;
            this.btImport.TabIndex = 3;
            this.btCancel.TabIndex = 4;
            this.btClose.TabIndex = 5;
        }
        #endregion

        #region Initiate
        public void Initiate(IWin32Window owner, GenericEventArgs<ExportImportParameter> args)
        {
            _owner = owner;
            Initiated(this, args);
        }
        #endregion

        #region ShowModal
        public void ShowModal()
        {
            this.ShowDialog(_owner);
            this.ViewClosedAction.Invoke();
        }
        #endregion

        #region CloseView
        public void CloseView()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.Close()));
            else
                this.Close();
        }
        #endregion

        #region Release
        public void Release(object sender, CancelEventArgs e)
        {
            ReleaseRequested(sender, e);
        }
        #endregion
        
        #region ShowMessage
        public DialogResult ShowMessage(string message)
        {
            return MessageBox.Show(message);
        }
        #endregion

        #region Enable/disable
        public bool ImportFileTextBoxEnabled
        {
            get { return this.txtImportFile.Enabled; }
            set 
            {
                if (this.txtImportFile.InvokeRequired)
                    this.Invoke(new Action(() => this.txtImportFile.Enabled = value));
                else
                    this.txtImportFile.Enabled = value;
            }
        }

        public bool FileSelectButtonEnabled
        {
            get { return this.btSelectFile.Enabled; }
            set
            {
                if (this.btSelectFile.InvokeRequired)
                    this.Invoke(new Action(() => this.btSelectFile.Enabled = value));
                else
                    this.btSelectFile.Enabled = value;
            }
        }

        public bool ImportFormatListEnabled
        {
            get { return this.cmbFormat.Enabled; }
            set
            {
                if (this.cmbFormat.InvokeRequired)
                    this.Invoke(new Action(() => this.cmbFormat.Enabled = value));
                else
                    this.cmbFormat.Enabled = value;
            }
        }

        public void DisableHeaderOptions()
        {
            if (this.grpImportOption.InvokeRequired)
                this.Invoke(new Action(() => DisableHeaderOptionButtons()));
            else
                DisableHeaderOptionButtons();
        }

        void DisableHeaderOptionButtons()
        {
            this.rdNoHeader.Enabled = false;
            this.rdWithHeader.Enabled = false;
        }

        public void EnableHeaderOptions()
        {
            if (this.grpImportOption.InvokeRequired)
                this.Invoke(new Action(() => EnableHeaderOptionButtons()));
            else
                EnableHeaderOptionButtons();
        }

        void EnableHeaderOptionButtons()
        {
            this.rdNoHeader.Enabled = true;
            this.rdWithHeader.Enabled = true;
        }

        public bool ImportButtonEnabled
        {
            get { return this.btImport.Enabled; }
            set
            {
                if (this.btImport.InvokeRequired)
                    this.Invoke(new Action(() => this.btImport.Enabled = value));
                else
                    this.btImport.Enabled = value;
            }
        }
        
        public bool CancelButtonEnabled
        {
            get { return this.btCancel.Enabled; }
            set
            {
                if (this.btCancel.InvokeRequired)
                    this.Invoke(new Action(() => this.btCancel.Enabled = value));
                else
                    this.btCancel.Enabled = value;
            }
        }

        public bool ProgressBarVisible
        {
            get { return this.importProgressBar.Visible; }
            set
            {
                if (this.statusStrip1.InvokeRequired)
                    this.Invoke(new Action(() => this.importProgressBar.Visible = value));
                else
                    this.importProgressBar.Visible = value;
            }
        }
        #endregion
        
        #region InitializeOpenFileDialog
        public void InitializeOpenFileDialog(string InitialDirectory, string filterText, int filterIndex)
        {
            _dialog.FileName = "";
            _dialog.InitialDirectory = InitialDirectory;
            _dialog.Filter = filterText;
            _dialog.FilterIndex = filterIndex;
            _dialog.Title = "Select file to import";
            _dialog.RestoreDirectory = true;
            _dialog.CheckFileExists = false;
            _dialog.CheckPathExists = true;
        }
        #endregion

        #region SetOpenFileDialogFilterText
        public void SetOpenFileDialogFilterText(string filterText)
        {
            _dialog.Filter = filterText;
        }
        #endregion

        #region Table name
        public void SetImportTableName(string tableName)
        {
            this.lblTableName.Text = tableName;
        }
        #endregion

        #region Import file text
        public string ImportFileText
        {
            get { return this.txtImportFile.Text; }
            set { this.txtImportFile.Text = value; }
        }

        public void FocusOnImportFileText()
        {
            this.ActiveControl = this.txtImportFile;
        }
        #endregion

        #region ShowOpenFileDialog
        public string ShowOpenFileDialog()
        {
            _dialog.ShowDialog();
            return _dialog.FileName;
        }
        #endregion

        #region Import format
        public void SetImportFormatDatasource(object datasource)
        {
            this.cmbFormat.DataSource = datasource;
        }

        public ExportImportFormat ImportFormat
        {
            get { return (ExportImportFormat)this.cmbFormat.SelectedIndex; }
        }
        #endregion

        #region Csv header option
        public CsvHeaderOption CsvImportHeaderOption
        {
            get
            {
                if (this.rdNoHeader.Checked)
                    return CsvHeaderOption.NoHeader;

                return CsvHeaderOption.ColumnNameHeader;
            }
        }
        #endregion

        #region StatuslabelText
        public void SetStatusLabelText(string status)
        {
            if (this.statusStrip1.InvokeRequired)
                this.Invoke(new Action(() => this.lblStatus.Text = status));
            else
                this.lblStatus.Text = status;
        }

        public void SetStatusToolTipText(string text)
        {
            if (this.statusStrip1.InvokeRequired)
                this.Invoke(new Action(() => this.lblStatus.ToolTipText = text));
            else
                this.lblStatus.ToolTipText = text;
        }
        #endregion

        #region ProgressBar
        public void SetProgressBarMaxValue(int maxValue)
        {
            this.importProgressBar.Maximum = maxValue;
        }

        public void SetProgressBarValue(int progress)
        {
            if (this.statusStrip1.InvokeRequired)
                this.Invoke(new Action(() => this.importProgressBar.Value = progress));
            else
                this.importProgressBar.Value = progress;
        }
        #endregion

        #endregion
    }
}