using MasudaManager.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MasudaManager.Views
{
    public partial class MainMenuView : MainMenuViewSlice, IMainMenuView
    {
        readonly string _zoomRatioFormat = "{0}%";
        readonly Size _minimumSize = new Size(949, 608);

        SqlTabView _sqlTabView = new SqlTabView();
        DbObjectInfoView _objectInfoView = new DbObjectInfoView();
        OpenFileDialog _openFileDialog = new OpenFileDialog();
        SaveFileDialog _saveFileDialog = new SaveFileDialog();

        #region Events
        public event EventHandler ComponentInitialized;
        public event EventHandler<CancelEventArgs> ViewClosing;
        public event EventHandler ConnectButtonClicked;
        public event EventHandler DisconnectButtonClicked;
        public event EventHandler AddTabButtonClicked;
        public event EventHandler OpenButtonClicked;
        public event EventHandler SaveButtonClicked;
        public event EventHandler ExportButtonClicked;
        public event EventHandler ExecuteButtonClicked;
        public event EventHandler CancelButtonClicked;
        public event EventHandler SearchButtonClicked;
        public event EventHandler EditResultButtonClicked;
        public event EventHandler PreferenceButtonClicked;
        #endregion

        #region Constructor
        public MainMenuView()
        {
            InitializeComponent();
            PrepareFormText();

            this.ssProgressBar.Style = ProgressBarStyle.Marquee;
            this.MinimumSize = _minimumSize;

            AlignStatusLabels();
            CreateSqlTab();
            CreateDbObjectInfoView();

            ComponentInitialized(this, EventArgs.Empty);
        }
        #endregion

        #region EventHandlers

        #region Application idle [DebuggerStepThrough]
        [DebuggerStepThrough]
        private void Application_Idle(object sender, EventArgs e)
        {
            UpdateWriteModeDisplay();
            UpdateZoomRatioDisplay();

            if (Control.IsKeyLocked(Keys.NumLock))
                this.ssNum.Text = LocalizedTextProvider.Form.NumLock;
            else
                this.ssNum.Text = string.Empty;

            if (Control.IsKeyLocked(Keys.CapsLock))
                this.ssCaps.Text = LocalizedTextProvider.Form.CapsLock;
            else
                this.ssCaps.Text = string.Empty;
        }
        #endregion

        #region View
        private void MainMenu_Load(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
            this.ActiveControl = _sqlTabView;
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            ViewClosing(this, e);
        }
        #endregion

        #region ToolStrip Click
        private void connectToolStrip_Click(object sender, EventArgs e)
        {
            OnConnectButtonClicked(this, e);
        }

        private void disconnToolStrip_Click(object sender, EventArgs e)
        {
            OnDisconnectButtonClicked(sender, e);
        }

        private void addTabToolStrip_Click(object sender, EventArgs e)
        {
            OnAddTabButtonClicked(sender, e);
        }

        private void openTextToolStrip_Click(object sender, EventArgs e)
        {
            OnOpenButtonClicked(sender, e);
        }

        private void saveTextToolStrip_Click(object sender, EventArgs e)
        {
            OnSaveButtonClicked(sender, e);
        }

        private void executeToolStrip_Click(object sender, EventArgs e)
        {
            OnExecuteButtonClicked(sender, e);
        }

        private void abortToolStrip_Click(object sender, EventArgs e)
        {
            OnCancelButtonClicked(sender, e);
        }

        private void btExportToolStrip_Click(object sender, EventArgs e)
        {
            OnExportButtonClicked(sender, e);
        }

        private void searchToolStrip_Click(object sender, EventArgs e)
        {
            OnSearchButtonClicked(sender, e);
        }

        private void sqlgridToolStrip_Click(object sender, EventArgs e)
        {
            OnEditResultButtonClicked(sender, e);
        }

        private void settingToolStrip_Click(object sender, EventArgs e)
        {
            OnPreferenceButtonClicked(this, e);
        }
        #endregion

        #region IMainMenuView EventHandlers
        void OnConnectButtonClicked(object sender, EventArgs e)
        {
            if (this.btConnect.Enabled)
                ConnectButtonClicked(sender, e);
        }

        void OnDisconnectButtonClicked(object sender, EventArgs e)
        {
            if (this.btDisconnect.Enabled)
                DisconnectButtonClicked(sender, e);
        }

        void OnAddTabButtonClicked(object sender, EventArgs e)
        {
            if (this.btAddTab.Enabled)
                AddTabButtonClicked(sender, e);
        }

        void OnOpenButtonClicked(object sender, EventArgs e)
        {
            if (this.btOpenText.Enabled)
                OpenButtonClicked(sender, e);
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (this.btSaveText.Enabled)
                SaveButtonClicked(sender, e);
        }

        void OnExportButtonClicked(object sender, EventArgs e)
        {
            if (this.btExport.Enabled)
                ExportButtonClicked(sender, e);
        }

        void OnExecuteButtonClicked(object sender, EventArgs e)
        {
            if (this.btExecuteSql.Enabled)
                ExecuteButtonClicked(sender, e);
        }

        void OnCancelButtonClicked(object sender, EventArgs e)
        {
            if (this.btCancelSql.Enabled)
                CancelButtonClicked(sender, e);
        }

        void OnSearchButtonClicked(object sender, EventArgs e)
        {
            if (this.btSearch.Enabled)
                SearchButtonClicked(sender, e);
        }

        void OnEditResultButtonClicked(object sender, EventArgs e)
        {
            if (this.btEditResult.Enabled)
                EditResultButtonClicked(sender, e);
        }

        void OnPreferenceButtonClicked(object sender, EventArgs e)
        {
            if (this.btPreference.Enabled)
                PreferenceButtonClicked(sender, e);
        }
        #endregion

        #region ProcessCmdKey
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.D:
                    OnConnectButtonClicked(this, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.F:
                    OnSearchButtonClicked(this, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.G:
                    OnEditResultButtonClicked(this, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.N:
                    OnAddTabButtonClicked(this, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.O:
                    OnOpenButtonClicked(this, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.R:
                    OnExecuteButtonClicked(this, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.T:
                    _objectInfoView.FocusOnObjectViewFilter();
                    return true;
                case Keys.Escape:
                    OnCancelButtonClicked(this, EventArgs.Empty);
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #endregion

        #region Methods

        #region AlignStatusLabels
        void AlignStatusLabels()
        {
            this.ssMode.Alignment = ToolStripItemAlignment.Right;
            this.ssNum.Alignment = ToolStripItemAlignment.Right;
            this.ssCaps.Alignment = ToolStripItemAlignment.Right;
            this.ssInfo.Alignment = ToolStripItemAlignment.Right;
            this.ssZoomRatio.Alignment = ToolStripItemAlignment.Right;
        }
        #endregion

        #region PrepareFormText
        void PrepareFormText()
        {
            this.btConnect.ToolTipText = LocalizedTextProvider.Form.ConnectToolTip;
            this.btDisconnect.ToolTipText = LocalizedTextProvider.Form.DisconnectToolTip;
            this.btAddTab.ToolTipText = LocalizedTextProvider.Form.AddNewTabToolTip;
            this.btOpenText.ToolTipText = LocalizedTextProvider.Form.OpenFileToolTip;
            this.btSaveText.ToolTipText = LocalizedTextProvider.Form.SaveFileToolTip;
            this.btExecuteSql.ToolTipText = LocalizedTextProvider.Form.ExecuteSqlToolTip;
            this.btCancelSql.ToolTipText = LocalizedTextProvider.Form.CancelSqlToolTip;
            this.btExport.ToolTipText = LocalizedTextProvider.Form.ExportToolTip;
            this.btSearch.ToolTipText = LocalizedTextProvider.Form.SearchToolTip;
            this.btEditResult.ToolTipText = "EditResult";
            this.btPreference.ToolTipText = LocalizedTextProvider.Form.PreferenceToolTip;
        }
        #endregion

        #region CreateSqlTab
        void CreateSqlTab()
        {
            _sqlTabView.Dock = System.Windows.Forms.DockStyle.Fill;
            _sqlTabView.Name = "SqlTab";
            this.baseSplitContainer.Panel1.Controls.Add(_sqlTabView);
        }
        #endregion

        #region CreateDbObjectInfoView
        public void CreateDbObjectInfoView()
        {
            _objectInfoView.Name = "ObjectInfoView";
            _objectInfoView.Dock = DockStyle.Fill;
            this.baseSplitContainer.Panel2.Controls.Add(_objectInfoView);
        }
        #endregion

        #region GetInvoker
        public ISynchronizeInvoke GetInvoker()
        {
            return this;
        }
        #endregion

        #region GetChildViewOwner
        public IWin32Window GetChildViewOwner()
        {
            return this;
        }
        #endregion

        #region View
        public string FormText
        {
            get { return this.Text; }
            set
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(() => this.Text = value));
                else
                    this.Text = value;
            }
        }

        public Rectangle CurrentClientSize
        {
            get { return this.Bounds; }
            set { this.Bounds = value; }
        }

        public FormWindowState CurrentWindowState
        {
            get { return this.WindowState; }
            set { this.WindowState = value; }
        }
        #endregion

        #region ShowMessageBox
        public DialogResult ShowMessageBox(string message)
        {
            return MessageBox.Show(message);
        }

        public DialogResult ShowErrorMessageBox(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons button, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton)
        {
            return MessageBox.Show(message, caption, button, icon, defaultbutton);
        }
        #endregion

        #region Enable/Disable
        public bool ConnectButtonEnabled
        {
            get { return this.btConnect.Enabled; }
            set
            {
                if (this.toolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btConnect.Enabled = value));
                else
                    this.btConnect.Enabled = value;
            }
        }

        public bool DisconnectButtonEnabled
        {
            get { return this.btDisconnect.Enabled; }
            set
            {
                if (this.toolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btDisconnect.Enabled = value));
                else
                    this.btDisconnect.Enabled = value;
            }
        }

        public bool AddTabButtonEnabled
        {
            get { return this.btAddTab.Enabled; }
            set
            {
                if (this.toolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btAddTab.Enabled = value));
                else
                    this.btAddTab.Enabled = value;
            }
        }

        public bool ExecuteButtonEnabled
        {
            get { return this.btExecuteSql.Enabled; }
            set
            {
                if (this.toolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btExecuteSql.Enabled = value));
                else
                    this.btExecuteSql.Enabled = value;
            }
        }

        public bool CancelButtonEnabled
        {
            get { return this.btCancelSql.Enabled; }
            set
            {
                if (this.toolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btCancelSql.Enabled = value));
                else
                    this.btCancelSql.Enabled = value;
            }
        }

        public bool ExportButtonEnabled
        {
            get { return this.btExport.Enabled; }
            set
            {
                if (this.toolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btExport.Enabled = value));
                else
                    this.btExport.Enabled = value;
            }
        }

        public bool SearchButtonEnabled
        {
            get { return this.btSearch.Enabled; }
            set
            {
                if (this.toolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btSearch.Enabled = value));
                else
                    this.btSearch.Enabled = value;
            }
        }

        public bool EditResultButtonEnabled
        {
            get { return this.btEditResult.Enabled; }
            set
            {
                if (this.toolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btEditResult.Enabled = value));
                else
                    this.btEditResult.Enabled = value;
            }
        }

        public bool PreferenceButtonEnabled
        {
            get { return this.btPreference.Enabled; }
            set
            {
                if (this.toolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btPreference.Enabled = value));
                else
                    this.btPreference.Enabled = value;
            }
        }

        #endregion
        
        #region Open/Save Dialog
        public void InitializeSaveFileDialog(string filterText, int filterIndex)
        {
            _saveFileDialog.Filter = filterText;
            _saveFileDialog.FilterIndex = filterIndex;
            _saveFileDialog.RestoreDirectory = true;
        }

        public void InitializeOpenFileDialog(string filterText, int filterIndex)
        {
            _openFileDialog.Filter = filterText;
            _openFileDialog.FilterIndex = filterIndex;
            _openFileDialog.RestoreDirectory = true;
        }

        public string GetSaveFilePath()
        {
            if (_saveFileDialog.ShowDialog() != DialogResult.Cancel)
                return _saveFileDialog.FileName;

            return null;
        }

        public string GetOpenFilePath()
        {
            if (_openFileDialog.ShowDialog() != DialogResult.Cancel)
                return _openFileDialog.FileName;

            return null;
        }
        #endregion

        #region Update write mode display
        void UpdateWriteModeDisplay()
        {
            if (_sqlTabView.ActiveInputViewWriteMode == WriteMode.Overwrite)
                this.ssMode.Text = LocalizedTextProvider.Form.OverWriteMode;
            else
                this.ssMode.Text = LocalizedTextProvider.Form.InsertMode;
        }
        #endregion

        #region Update zoom ratio display
        void UpdateZoomRatioDisplay()
        {
            this.ssZoomRatio.Text = String.Format(_zoomRatioFormat, _sqlTabView.ActiveInputViewZoomRatioText);
        }
        #endregion

        #region Get InputView file path
        public string GetActiveInputViewFilePath()
        {
            return _sqlTabView.ActiveInputViewFilePath;
        }
        #endregion

        #region Get InputView text
        public string GetActiveInputViewText()
        {
            return _sqlTabView.ActiveInputViewText;
        }
        #endregion

        #region GetDbObjectInfoViewSelectedObjectName
        public string GetDbObjectInfoViewSelectedObjectName()
        {
            return _objectInfoView.GetSelectedDbObjectName();
        }
        #endregion

        #region StatusStrip
        public void SetStatusLabelColor(Color color)
        {
            if (this.statusStrip.InvokeRequired)
                this.Invoke(new Action(() => this.lblStatus.ForeColor = color));
            else
                this.lblStatus.ForeColor = color;
        }

        public void SetStatusLabelText(string text)
        {
            if (this.statusStrip.InvokeRequired)
                this.Invoke(new Action(() => this.lblStatus.Text = text));
            else
                this.lblStatus.Text = text;
        }

        public bool ProgressBarVisible
        {
            get { return this.ssProgressBar.Visible; }
            set
            {
                if (this.statusStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.ssProgressBar.Visible = value));
                else
                    this.ssProgressBar.Visible = value;
            }
        }

        public void SetInfomationLabelText(string information)
        {
            this.ssInfo.Text = information;
        }
        #endregion

        #endregion
    }
}