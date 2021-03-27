using MasudaManager.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MasudaManager.Views
{
    public partial class EditResultView : EditResultViewSlice, IEditResultView, IChildView<string>
    {
        readonly int _numeriUpDownDisplayIndex = 5;
        readonly int _bulkInsertMinCount = 0;
        readonly int _bulkInsertMaxCount = 999;
        readonly string _awaitDialogCaption = "EditSqlResult";
        ProgressView _dialog = new ProgressView();
        IWin32Window _parent;

        public bool SuppressFormClosingEvent { get; set; }
        public Action ViewClosedAction { get; set; }

        #region Events
        public event EventHandler<GenericEventArgs<string>> Initiated;
        public event EventHandler AwaitDialogCancelButtonClicked;
        public event EventHandler EditorStatusRequested;
        public event EventHandler ApplyButtonClicked;
        public event EventHandler CancelButtonClicked;
        public event EventHandler InsertButtonClicked;
        public event EventHandler DeleteButtonClicked;
        public event EventHandler BulkInsertButtonClicked;
        public event EventHandler UndoButtonClicked;
        public event EventHandler RedoButtonClicked;
        public event EventHandler<CancelEventArgs> CellBeginEdit;
        public event EventHandler CellEndEdit;
        public event EventHandler DataCopyRequested;
        public event EventHandler DataPasteRequested;
        public event EventHandler DeleteKeyDown;
        public event EventHandler SearchViewRequested;
        public event EventHandler SearchForwardRequested;
        public event EventHandler SearchBackwardRequested;
        public event EventHandler<CancelEventArgs> ViewClosing;
        public event EventHandler<CancelEventArgs> ReleaseRequested;
        #endregion

        #region Constructor
        public EditResultView()
        {
            InitializeComponent();
            PrepareFormText();

            ToolStripControlHost tsHost = new ToolStripControlHost(this.bulkInsertNumericUpDown);
            tsHost.Alignment = ToolStripItemAlignment.Left;
            tsHost.Margin = new Padding(5, 0, 3, 0);
            this.menuToolStrip.Items.Insert(_numeriUpDownDisplayIndex, tsHost);

            ApplyMainGridDefaultSetting();
            ApplyBulkInserterSetting();
            PrepareContextMenu();
        }
        #endregion

        #region ApplyMainGridDefaultSetting
        void ApplyMainGridDefaultSetting()
        {
            this.MainGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.MainGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.MainGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.MainGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.MainGrid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            this.MainGrid.ShowCellErrors = false;
            this.MainGrid.ShowCellToolTips = false;
            this.MainGrid.ShowEditingIcon = false;
            this.MainGrid.ShowRowErrors = false;
            this.MainGrid.DisplaySpaceCharacter = false;
            this.MainGrid.AllowRightClickCellSelect = true;
            this.MainGrid.ForcePlainTextCopy = false;
        }
        #endregion

        #region ApplyBulkInserterSetting
        void ApplyBulkInserterSetting()
        {
            this.bulkInsertNumericUpDown.Minimum = _bulkInsertMinCount;
            this.bulkInsertNumericUpDown.Maximum = _bulkInsertMaxCount;
        }
        #endregion

        #region EventHandlers

        #region Closing
        private void SqlGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.SuppressFormClosingEvent)
                return;

            ViewClosing(sender, e);

            if (!e.Cancel)
                this.ViewClosedAction.Invoke();
        }
        #endregion

        #region Application Idle [DebuggerStepThrough]
        [DebuggerStepThrough]
        private void Application_Idle(object sender, EventArgs e)
        {
            EditorStatusRequested(sender, e);
        }
        #endregion

        #region Buttuns
        private void btApply_Click(object sender, EventArgs e)
        {
            ApplyButtonClicked(sender, e);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            CancelButtonClicked(sender, e);
        }

        private void btAddRow_Click(object sender, EventArgs e)
        {
            InsertButtonClicked(sender, e);
        }

        private void btDelRow_Click(object sender, EventArgs e)
        {
            DeleteButtonClicked(sender, e);
        }

        private void btAddRows_Click(object sender, EventArgs e)
        {
            BulkInsertButtonClicked(sender, e);
        }

        private void btUndo_Click(object sender, EventArgs e)
        {
            UndoButtonClicked(sender, e);
        }

        private void btRedo_Click(object sender, EventArgs e)
        {
            RedoButtonClicked(sender, e);
        }
        #endregion

        #region ContextMenu
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataCopyRequested(sender, e);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataPasteRequested(sender, e);
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertButtonClicked(sender, e);
        }

        private void addRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BulkInsertButtonClicked(sender, e);
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteButtonClicked(sender, e);
        }
        #endregion

        #region NumericUpDown keydown
        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BulkInsertButtonClicked(sender, e);
                e.Handled = true;
            }
        }
        #endregion

        #region MainGrid
        private void MainGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            CellBeginEdit(sender, e);
        }

        private void MainGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CellEndEdit(sender, e);
        }

        private void MainGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.MainGrid.CancelEdit();
        }

        private void MainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Back:
                case Keys.Delete:
                    e.Handled = true;
                    DeleteKeyDown(sender, e);
                    break;
            }

            this.MainGrid.CancelEdit();
        }
        #endregion

        #region MenuStrip mouse click
        private void toolStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.MainGrid.IsCurrentCellDirty)
                this.MainGrid.EndEdit();
        }
        #endregion

        #region StatusStrip mouse click
        private void statusStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.MainGrid.IsCurrentCellDirty)
                this.MainGrid.EndEdit();
        }
        #endregion

        #region ProcessCmdKey
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.F:
                    SearchViewRequested(this, EventArgs.Empty);
                    return true;
                case Keys.F3:
                    SearchForwardRequested(this, EventArgs.Empty);
                    return true;
                case Keys.Shift | Keys.F3:
                    SearchBackwardRequested(this, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.S:
                    ApplyButtonClicked(this, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.Z:
                    UndoButtonClicked(this, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.Y:
                    RedoButtonClicked(this, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.V:
                    DataPasteRequested(this, EventArgs.Empty);
                    return true;
                case Keys.Escape:
                    CancelButtonClicked(this, EventArgs.Empty);
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #endregion

        #region Methods

        #region PrepareFormText
        void PrepareFormText()
        {
            this.btApply.ToolTipText = LocalizedTextProvider.Form.ApplyChangesToolTip;
            this.btCancel.ToolTipText = LocalizedTextProvider.Form.CancelSqlToolTip;
            this.btInsert.ToolTipText = LocalizedTextProvider.Form.AddRowToolTip;
            this.btDelete.ToolTipText = LocalizedTextProvider.Form.DeleteRowToolTip;
            this.btBulkInsert.ToolTipText = LocalizedTextProvider.Form.AddRowsToolTip;
            this.btUndo.ToolTipText = LocalizedTextProvider.Form.UndoToolTip;
            this.btRedo.ToolTipText = LocalizedTextProvider.Form.RedoToolTip;
        }
        #endregion

        #region PrepareContextMenu
        void PrepareContextMenu()
        {
            this.copyToolStripMenuItem.Text = LocalizedTextProvider.ContextMenu.Copy;
            this.pasteToolStripMenuItem.Text = LocalizedTextProvider.ContextMenu.Paste;
            this.addRowToolStripMenuItem.Text = LocalizedTextProvider.ContextMenu.AddRow;
            this.addRowsToolStripMenuItem.Text = LocalizedTextProvider.ContextMenu.AddRows;
            this.deleteRowToolStripMenuItem.Text = LocalizedTextProvider.ContextMenu.DeleteRow;
        }
        #endregion

        #region GetWin32Window
        public IWin32Window GetWin32Window()
        {
            return this;
        }
        #endregion

        #region EditingCoordinator
        public IGridViewEditingCoordinator EditingCoordinator
        {
            get { return this.MainGrid.EditingCoordinator; }
        }
        #endregion

        #region Initiate
        public void Initiate(IWin32Window owner, GenericEventArgs<string> arg)
        {
            _parent = owner;
            Initiated(owner, arg);
        }
        #endregion

        #region GetParentWindow
        public IWin32Window GetParentWindow()
        {
            return _parent;
        }
        #endregion

        #region ShowAwaitDialog
        public void ShowAwaitDialog(IWin32Window owner)
        {
            _dialog.Caption = _awaitDialogCaption;
            _dialog.CloseOnCancel = false;
            _dialog.CancelButtonClicked += OnAwaitDialogCancelButtonClicked;

            _dialog.ShowView(owner);
        }

        public void SetAwaitDialogStatusText(string text)
        {
            _dialog.StatusLabelText = text;
        }

        void OnAwaitDialogCancelButtonClicked(object sender, EventArgs e)
        {
            AwaitDialogCancelButtonClicked(sender, e);
        }

        public void CloseAwaitDialog()
        {
            _dialog.CloseView();
        }
        #endregion

        #region ShowModeless
        public void ShowModeless()
        {
            Application.Idle += Application_Idle;
            this.Show();
        }
        #endregion

        #region Raise ChildViewClosed
        public void RaiseChildViewClosed()
        {
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

        #region Form caption
        public string FormText
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        #endregion

        #region Enable/disable
        public bool ApplyButtonEnabled
        {
            get { return this.btApply.Enabled; }
            set
            {
                if (this.menuToolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btApply.Enabled = value));
                else
                    this.btApply.Enabled = value;
            }
        }

        public bool CancelButtonEnabled
        {
            get { return this.btCancel.Enabled; }
            set
            {
                if (this.menuToolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btCancel.Enabled = value));
                else
                    this.btCancel.Enabled = value;
            }
        }

        public bool InsertButtonEnabled
        {
            get { return this.btInsert.Enabled; }
            set
            {
                if (this.menuToolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btInsert.Enabled = value));
                else
                    this.btInsert.Enabled = value;
            }
        }

        public bool DeleteButtonEnabled
        {
            get { return this.btDelete.Enabled; }
            set
            {
                if (this.menuToolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btDelete.Enabled = value));
                else
                    this.btDelete.Enabled = value;
            }
        }

        public bool BulkInsertButtonEnabled
        {
            get { return this.btBulkInsert.Enabled; }
            set
            {
                if (this.menuToolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btBulkInsert.Enabled = value));
                else
                    this.btBulkInsert.Enabled = value;
            }
        }

        public bool UndoButtonEnabled
        {
            get { return this.btUndo.Enabled; }
            set
            {
                if (this.menuToolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btUndo.Enabled = value));
                else
                    this.btUndo.Enabled = value;
            }
        }

        public bool RedoButtonEnabled
        {
            get { return this.btRedo.Enabled; }
            set
            {
                if (this.menuToolStrip.InvokeRequired)
                    this.Invoke(new Action(() => this.btRedo.Enabled = value));
                else
                    this.btRedo.Enabled = value;
            }
        }

        public bool MainGridViewEnabled
        {
            get { return this.MainGrid.Enabled; }
            set
            {
                if (this.MainGrid.InvokeRequired)
                    this.Invoke(new Action(() => this.MainGrid.Enabled = value));
                else
                    this.MainGrid.Enabled = value;
            }
        }

        public bool ContextMenuEnabled
        {
            get { return this.editResultContextMenu.Enabled; }
            set
            {
                if (this.editResultContextMenu.InvokeRequired)
                    this.Invoke(new Action(() => this.editResultContextMenu.Enabled = value));
                else
                    this.editResultContextMenu.Enabled = value;
            }
        }

        public bool DeleteContextMenuEnabled
        {
            get { return this.deleteRowToolStripMenuItem.Enabled; }
            set
            {
                if (this.editResultContextMenu.InvokeRequired)
                    this.Invoke(new Action(() => this.deleteRowToolStripMenuItem.Enabled = value));
                else
                    this.deleteRowToolStripMenuItem.Enabled = value;
            }
        }
        #endregion

        #region ShowMessage
        public DialogResult ShowMessage(string message)
        {
            return MessageBox.Show(message);
        }

        public DialogResult ShowMessage(string message, string caption, MessageBoxButtons button, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton)
        {
            return MessageBox.Show(message, caption, button, icon, defaultbutton);
        }
        #endregion

        #region BulkInsertRowCount
        public int BulkInsertRowCount
        {
            get { return (int)this.bulkInsertNumericUpDown.Value; }
        }
        #endregion

        #region MainGrid datasource
        public void SetMainGridDataSource(object datasource)
        {
            this.MainGrid.DataSource = datasource;
        }
        #endregion

        #region MainGrid disable/enable settings
        public void DisableMainGridSettings()
        {
            this.MainGrid.SuspendLayout();

            this.MainGrid.CausesValidation = false;
            this.MainGrid.ColumnHeadersVisible = false;
            this.MainGrid.RowHeadersVisible = false;
            this.MainGrid.EnableHeadersVisualStyles = false;
            this.MainGrid.MultiSelect = false;
            this.MainGrid.ReadOnly = true;
            this.MainGrid.ScrollBars = ScrollBars.None;
            this.MainGrid.DisplayRowNumber = false;
        }

        public void EnableMainGridSettings()
        {
            this.MainGrid.CausesValidation = true;
            this.MainGrid.ColumnHeadersVisible = true;
            this.MainGrid.RowHeadersVisible = true;
            this.MainGrid.EnableHeadersVisualStyles = true;
            this.MainGrid.MultiSelect = true;
            this.MainGrid.ReadOnly = false;
            this.MainGrid.ScrollBars = ScrollBars.Both;
            this.MainGrid.DisplayRowNumber = true;
            this.MainGrid.AdjustRowHeadersWidthToRowNumber();
            this.MainGrid.AutoResizeColumnHeadersHeight();

            this.MainGrid.ResumeLayout();
        }
        #endregion

        #region MainGrid clear/restore selection
        public void RestoreSelection(Cell cell)
        {
            this.MainGrid.CurrentCell = this.MainGrid[cell.ColumnIndex, cell.RowIndex];
            this.MainGrid.FocusOnCurrentCel();
        }

        public void ClearSelection()
        {
            this.MainGrid.ClearSelection();
        }
        #endregion

        #region MainGrid suspend/resume Layout
        public void SuspendMainGridLayout()
        {
            this.MainGrid.SuspendLayout();

            this.MainGrid.CausesValidation = false;
            this.MainGrid.EnableHeadersVisualStyles = false;
            this.MainGrid.DisplayRowNumber = false;
            this.MainGrid.ReadOnly = true;
        }

        public void ResumeMainGridLayout()
        {
            this.MainGrid.CausesValidation = true;
            this.MainGrid.EnableHeadersVisualStyles = true;
            this.MainGrid.ReadOnly = false;
            this.MainGrid.DisplayRowNumber = true;
            this.MainGrid.AdjustRowHeadersWidthToRowNumber();
            this.MainGrid.ResumeLayout();
        }
        #endregion

        #region MainGrid cell
        public Cell FirstSelectedCell { get { return this.MainGrid.FirstSelectedCell; } }

        public Cell LastSelectedCell { get { return this.MainGrid.LastSelectedCell; } }

        public int SelectedRowCount { get { return this.MainGrid.SelectedRowCount; } }

        public int SelectedColumnCount { get { return this.MainGrid.SelectedColumnCount; } }

        public void FocusOnCell(Cell cell)
        {
            if (this.MainGrid.InvokeRequired)
                this.Invoke(new Action(() => FocusOnCellThreadSafe(cell)));
            else
                FocusOnCellThreadSafe(cell);
        }

        void FocusOnCellThreadSafe(Cell cell)
        {
            this.MainGrid.CurrentCell = this.MainGrid[cell.ColumnIndex, cell.RowIndex];
            this.MainGrid[cell.ColumnIndex, cell.RowIndex].Selected = true;
        }

        public void SuspendCellValidation()
        {
            this.MainGrid.CausesValidation = false;
        }

        public void ResumeCellValidation()
        {
            this.MainGrid.CausesValidation = true;
        }

        public bool ValidateCellValue(Cell cell, object value)
        {
            return this.MainGrid.ValidateCellValue(cell.ColumnIndex, cell.RowIndex, value);
        }
        #endregion

        #region UpdateStatusLabelText
        public void UpdateStatusLabelText(string text)
        {
            this.lblStatus.Text = text;
        }
        #endregion

        #endregion
    }
}