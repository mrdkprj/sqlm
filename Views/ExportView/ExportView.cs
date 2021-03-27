using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MasudaManager.Views
{
    public partial class ExportView : ExportViewSlice, IExportView, IChildView<ExportImportParameter>
    {
        IWin32Window _owner;
        OpenFileDialog _dialog = new OpenFileDialog();

        public bool SuppressFormClosingEvent { get; set; }
        public Action ViewClosedAction { get; set; }

        public event EventHandler<GenericEventArgs<ExportImportParameter>> Initiated;
        public event EventHandler ExportFileTextChanged;
        public event EventHandler FileSelectButtonClicked;
        public event EventHandler ExportFormatChanged;
        public event EventHandler ExportButtonClicked;
        public event EventHandler CancelButtonClicked;
        public event EventHandler<CancelEventArgs> ViewClosing;
        public event EventHandler<CancelEventArgs> ReleaseRequested;

        public ExportView()
        {
            InitializeComponent();
            PrepareFormText();

            SetTabIndex();
            this.cmbFormat.SelectedIndexChanged += cmbFormat_SelectedIndexChanged;
            this.rdNoHeader.Checked = true;
        }

        #region EvnetHandlers

        #region Closing
        private void ExportView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.SuppressFormClosingEvent)
                return;

            ViewClosing(sender, e);
        }
        #endregion

        #region ExportFile text changed
        private void txtExportFile_TextChanged(object sender, EventArgs e)
        {
            ExportFileTextChanged(sender, e);
        }
        #endregion

        #region File select button click
        private void btSelectFile_Click(object sender, EventArgs e)
        {
            FileSelectButtonClicked(sender, e);
        }
        #endregion

        #region Export format changed
        void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExportFormatChanged(sender, e);
        }
        #endregion

        #region Export button click
        private void btExport_Click(object sender, EventArgs e)
        {
            ExportButtonClicked(sender, e);
        }
        #endregion

        #region Cancel button click
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
            this.Text = LocalizedTextProvider.Form.Export;
            this.lblTable.Text = LocalizedTextProvider.Form.TableName;
            this.lblFile.Text = LocalizedTextProvider.Form.File;
            this.grpExportOption.Text = LocalizedTextProvider.Form.ExportOption;
            this.lblFormat.Text = LocalizedTextProvider.Form.Format;
            this.rdNoHeader.Text = LocalizedTextProvider.Form.WithoutHeader;
            this.rdColumnNameHeader.Text = LocalizedTextProvider.Form.WithColumnNameHeader;
            this.btExport.Text = LocalizedTextProvider.Form.ExportToolTip;
            this.btCancel.Text = LocalizedTextProvider.Form.Cancel;
            this.btClose.Text = LocalizedTextProvider.Form.Close;
        }
        #endregion

        #region SetTabIndex
        void SetTabIndex()
        {
            this.txtSql.TabIndex = 0;
            this.txtExportFile.TabIndex = 1;
            this.btSelectFile.TabIndex = 2;
            this.grpExportOption.TabIndex = 3;
            this.btExport.TabIndex = 4;
            this.btCancel.TabIndex = 5;
            this.btClose.TabIndex = 6;
        }
        #endregion

        #region Initiate
        public void Initiate(IWin32Window owner, GenericEventArgs<ExportImportParameter> arg)
        {
            _owner = owner;
            Initiated(this, arg);
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
        public bool SqlInputViewEnabled
        {
            get { return this.txtSql.Enabled; }
            set
            {
                if (this.txtSql.InvokeRequired)
                    this.Invoke(new Action(() => this.txtSql.Enabled = value));
                else
                    this.txtSql.Enabled = value;
            }
        }

        public bool ExportFileTextBoxEnabled
        {
            get { return this.txtExportFile.Enabled; }
            set
            {
                if (this.txtExportFile.InvokeRequired)
                    this.Invoke(new Action(() => this.txtExportFile.Enabled = value));
                else
                    this.txtExportFile.Enabled = value;
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

        public bool ExportFormatListEnabled
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
            if (this.grpExportOption.InvokeRequired)
                this.Invoke(new Action(() => DisableHeaderOptionButtons()));
            else
                DisableHeaderOptionButtons();
        }

        void DisableHeaderOptionButtons()
        {
            this.rdNoHeader.Enabled = false;
            this.rdColumnNameHeader.Enabled = false;
        }
        
        public void EnableHeaderOptions()
        {
            if (this.grpExportOption.InvokeRequired)
                this.Invoke(new Action(() => EnableHeaderOptionButtons()));
            else
                EnableHeaderOptionButtons();
        }

        void EnableHeaderOptionButtons()
        {
            this.rdNoHeader.Enabled = true;
            this.rdColumnNameHeader.Enabled = true;
        }

        public bool ExportButtonEnabled
        {
            get { return this.btExport.Enabled; }
            set
            {
                if (this.btExport.InvokeRequired)
                    this.Invoke(new Action(() => this.btExport.Enabled = value));
                else
                    this.btExport.Enabled = value;
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
            get { return this.exportProgressBar.Visible; }
            set
            {
                if (this.statusStrip1.InvokeRequired)
                    this.Invoke(new Action(() => this.exportProgressBar.Visible = value));
                else
                    this.exportProgressBar.Visible = value; ;
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
            _dialog.Title = "Select file to export";
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

        #region Tablename
        public void SetExportTableName(string tableName)
        {
            this.lblTableName.Text = tableName;
        }
        #endregion
        
        #region SqlInputView
        public string SqlInputViewText
        {
            get { return this.txtSql.Text; }
            set { this.txtSql.Text = value; }
        }
        #endregion
        
        #region Export file text
        public string ExportFileText
        {
            get { return this.txtExportFile.Text; }
            set { this.txtExportFile.Text = value; }
        }

        public void FocusOnExportFileText()
        {
            this.ActiveControl = this.txtExportFile;
        }
        #endregion
        
        #region ShowOpenFileDialog
        public string ShowOpenFileDialog()
        {
            _dialog.ShowDialog();
            return _dialog.FileName;
        }
        #endregion

        #region Export format
        public void SetExportFormatDatasource(object datasource)
        {
            this.cmbFormat.DataSource = datasource;
        }

        public ExportImportFormat ExportFormat
        {
            get { return (ExportImportFormat)this.cmbFormat.SelectedIndex; }
        }
        #endregion

        #region Csv header option
        public CsvHeaderOption CsvExportHeaderOption
        {
            get
            {
                if (rdNoHeader.Checked)
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
            this.exportProgressBar.Maximum = maxValue;
        }

        public void SetProgressBarValue(int progress)
        {
            if (this.statusStrip1.InvokeRequired)
                this.Invoke(new Action(() => this.exportProgressBar.Value = progress));
            else
                this.exportProgressBar.Value = progress;
        }
        #endregion

        #endregion
    }
}