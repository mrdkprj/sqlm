using MasudaManager.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MasudaManager.Views
{
    public partial class SqlTabView : SqlTabViewSlice, ISqlTabView
    {
        MainTabControl _mainTab;
        readonly int _mainTabStartTabIndex = 100;
        readonly int _tabIndexIncrementalValue = 10;

        #region Events
        public event EventHandler Loaded;
        public event EventHandler NewTabRequested;
        public event EventHandler TabSelectionChanged;
        public event EventHandler<CancelEventArgs> TabSelecting;
        public event EventHandler TabCloseButtonClicked;

        public event EventHandler InputViewSaveStatusChanged;
        public event EventHandler<DragEventArgs> InputViewFileDropped;
        public event EventHandler<KeyEventArgs> InputViewEnterKeyDown;
        public event EventHandler InputViewPeriodKeyUp;
        public event EventHandler InputViewFilterObjectViewClicked;
        public event EventHandler InputViewFilterPropertyViewClicked;
        public event EventHandler InputViewCopyFromObjectViewClicked;
        public event EventHandler InputViewSaveTextRequested;

        public event EventHandler ResultViewAutoResizeHeadersClicked; 
        public event EventHandler ResultViewCopyTextClicked;
        public event EventHandler ResultViewCopyHeaderClicked;
        public event EventHandler ResultViewCopyTextWithHeaderClicked;
        public event EventHandler ResultViewClearResultClicked;
        public event EventHandler ResultViewEditResultClicked;
        public event EventHandler ResultViewSearchForwardRequested;
        public event EventHandler ResultViewSearchBackwardRequested;
        #endregion

        #region Constructor
        public SqlTabView()
        {
            InitializeComponent();

            _mainTab = this.MainTab;
            ConfigureMainTab();
            this.closeTabMenuItem.Text = LocalizedTextProvider.ContextMenu.CloseTab;
        }
        #endregion

        #region Write mode
        public WriteMode ActiveInputViewWriteMode
        {
            get
            {
                if (_mainTab.SelectedTabComponent.InputView.Overtype)
                    return WriteMode.Overwrite;
                else
                    return WriteMode.Insert;
            }
        }
        #endregion

        #region Zoom ratio
        public string ActiveInputViewZoomRatioText
        {
            get { return _mainTab.SelectedTabComponent.InputView.ZoomRatio; }
        }
        #endregion

        #region InputView file path
        public string ActiveInputViewFilePath
        {
            get { return _mainTab.SelectedTabComponent.InputView.FilePath; }
        }
        #endregion

        #region InputView text
        public string ActiveInputViewText
        {
            get { return _mainTab.SelectedTabComponent.InputView.Text; }
        }
        #endregion

        #region EventHandlers

        #region View
        void SqlTabView_Load(object sender, EventArgs e)
        {
            Loaded(sender, e);
        }
        #endregion

        #region Tab
        void SqlTabView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                NewTabRequested(sender, e);
        }
        
        void MainTab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabSelecting(sender, e);
        }

        void MainTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabSelectionChanged(sender, e);
        }

        void MainTab_CloseTabButtonClick(object sender, EventArgs e)
        {
            TabCloseButtonClicked(sender, e);
        }
        #endregion

        #region InputView
        void InputView_SaveStatusChanged(object sender, EventArgs e)
        {
            InputViewSaveStatusChanged(sender, e);
        }

        void InputView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        void InputView_DragDrop(object sender, DragEventArgs e)
        {
            InputViewFileDropped(sender, e);
        }

        void InputView_CopyFromObjectView(object sender, EventArgs e)
        {
            InputViewCopyFromObjectViewClicked(sender, e);
        }

        void InputView_CopyToObjectViewFilterClick(object sender, EventArgs e)
        {
            InputViewFilterObjectViewClicked(sender, e);
        }

        void InputView_CopyToPropertyViewFilterClick(object sender, EventArgs e)
        {
            InputViewFilterPropertyViewClicked(sender, e);
        }

        void InputView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.S:
                    InputViewSaveTextRequested(sender, e);
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    InputViewEnterKeyDown(sender, e);
                    break;
            }
        }

        void InputView_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.OemPeriod:
                    InputViewPeriodKeyUp(sender, EventArgs.Empty);
                    e.Handled = true;
                    break;
            }
        }
        #endregion

        #region ResultView
        void ResultView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {                    
                case Keys.F3:
                    ResultViewSearchForwardRequested(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F2:
                case Keys.Shift | Keys.F3:
                    ResultViewSearchBackwardRequested(sender, e);
                    e.Handled = true;
                    break;
            }
        }

        void ResultView_AutoResizeHeadersClick(object sender, EventArgs e)
        {
            ResultViewAutoResizeHeadersClicked(sender, e);
        }

        void ResultView_CopyTextClick(object sender, EventArgs e)
        {
            ResultViewCopyTextClicked(sender, e);
        }

        void ResultView_CopyHeaderClick(object sender, EventArgs e)
        {
            ResultViewCopyHeaderClicked(sender, e);
        }

        void ResultView_CopyTextWithHeader(object sender, EventArgs e)
        {
            ResultViewCopyTextWithHeaderClicked(sender, e);
        }

        void ResultView_EditResultClick(object sender, EventArgs e)
        {
            ResultViewEditResultClicked(sender, e);
        }

        void ResultView_ClearResultClick(object sender, EventArgs e)
        {
            ResultViewClearResultClicked(sender, e);
        }

        #endregion

        #endregion

        #region Methods

        #region Get invoker
        public ISynchronizeInvoke GetInvoker()
        {
            return this;
        }
        #endregion

        #region Get Win32Window
        public IWin32Window GetWin32Window()
        {
            return this;
        }
        #endregion

        #region MessageBox
        public DialogResult ShowMessageBox(string message)
        {
            return MessageBox.Show(message);
        }

        public DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons button, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton)
        {
            return MessageBox.Show(message, caption, button, icon, defaultbutton);
        }
        #endregion

        #region ConfigureMainTab
        void ConfigureMainTab()
        {
            _mainTab.AllowDrop = true;
            _mainTab.ContextMenuStrip = this.sqlTabContextMenuStrip;
            _mainTab.Dock = DockStyle.Fill;
            _mainTab.Name = "MainTab";
            _mainTab.TabStop = false;
            _mainTab.SelectedIndexChanged += this.MainTab_SelectedIndexChanged;
            _mainTab.Selecting += this.MainTab_Selecting;
            _mainTab.TabCloseButtonClick += MainTab_CloseTabButtonClick;
        }
        #endregion

        #region CreateNewTabPage
        public Guid CreateNewTabPage()
        {
            MainTabComponent component = _mainTab.AddMainTabComponent();
            AssignEventHandersToComponent(component);
            SetMainTabComponentTabIndex(component);

            return component.Guid;
        }

        void AssignEventHandersToComponent(MainTabComponent component)
        {
            component.InputViewSaveStatusChanged += InputView_SaveStatusChanged;
            
            component.InputView.KeyDown += InputView_KeyDown;
            component.InputView.KeyUp += InputView_KeyUp;
            component.InputView.DragEnter += InputView_DragEnter;
            component.InputView.DragDrop += InputView_DragDrop;
            component.CopyToObjectViewFilterClick += InputView_CopyToObjectViewFilterClick;
            component.CopyToPropertyViewFilterClick += InputView_CopyToPropertyViewFilterClick;
            component.CopyFromObjectViewClick += InputView_CopyFromObjectView;

            component.ResultView.KeyDown += ResultView_KeyDown;
            component.AutoResizeHeadersClicked += ResultView_AutoResizeHeadersClick;
            component.CopyTextClick += ResultView_CopyTextClick;
            component.CopyHeaderClick += ResultView_CopyHeaderClick;
            component.CopyTextWithHeaderClick += ResultView_CopyTextWithHeader;
            component.EditResultClick += ResultView_EditResultClick;
            component.ClearResultClick += ResultView_ClearResultClick;
        }
        #endregion

        #region SetMainTabComponentTabIndex
        void SetMainTabComponentTabIndex(MainTabComponent component)
        {
            int count = _tabIndexIncrementalValue;

            foreach (var control in component)
            {
                control.TabIndex = _mainTabStartTabIndex + count;
                count += _tabIndexIncrementalValue;
            }
        }
        #endregion

        #region TabPage
        public void SetTabPageText(object guid, string filename)
        {
            _mainTab.SetTabPageText(guid, filename);
        }

        public void SetTabPageInfoMessage(object guid, string message)
        {
            _mainTab.SetTabPageInfoMessage(guid, message);
        }

        public object CurrentTabPageGuid
        {
            get { return _mainTab.SelectedXTab.Guid; }
        }

        public void SelectTabPage(object guid)
        {
            _mainTab.SelectTabPage(guid);
        }

        public void RemoveTabPage(object guid)
        {
            _mainTab.RemoveTabPage(guid);
        }

        #endregion

        #region InputView
        public void ShowSqlInputAssistant(object guid)
        {
            _mainTab.GetTabComponent(guid).InputView.ShowInputAssistant(this.TopLevelControl);
        }

        public bool IsInputViewTextSaved(object guid)
        {
            return _mainTab.GetTabComponent(guid).InputView.Saved;
        }

        public bool IsCommented(object guid, int position)
        {
            return _mainTab.GetTabComponent(guid).InputView.IsCommented(position);
        }

        public string GetInputViewSelectedText(object guid)
        {
            return _mainTab.GetTabComponent(guid).InputView.SelectedText;
        }

        public void SetInputViewSelectedText(object guid, string text)
        {
            _mainTab.GetTabComponent(guid).InputView.ReplaceSelection(text);
        }

        public void SetInputViewSelection(object guid, int startPosition)
        {
            _mainTab.GetTabComponent(guid).InputView.SetSelection(startPosition, startPosition + 1);
        }

        public string GetInputViewText(object guid)
        {
            return _mainTab.GetTabComponent(guid).InputView.Text;
        }

        public void MarkInputViewAsSaved(object guid)
        {
            _mainTab.GetTabComponent(guid).InputView.MarkAsSaved();
        }
        
        public void LoadInputViewText(object guid, string text)
        {
            _mainTab.GetTabComponent(guid).InputView.LoadText(text);
        }

        public int GetInputViewLastIndexOf(object guid, char target)
        {
            return _mainTab.GetTabComponent(guid).InputView.Text.LastIndexOf(target);
        }

        public void SetInputViewFilePath(object guid, string path)
        {
            _mainTab.GetTabComponent(guid).InputView.FilePath = path;
        }

        public void SetInputViewFocus(object guid)
        {
            _mainTab.GetTabComponent(guid).InputView.Focus();
        }

        public bool InputViewFocused(object guid)
        {
            return _mainTab.GetTabComponent(guid).InputView.Focused;
        }
        #endregion

        #region ResultView Settings
        public void SuspendResultViewSettings(object guid)
        {
            if (_mainTab.GetTabComponent(guid).ResultView.InvokeRequired)
                this.Invoke(new Action(() => DisableGridSettings(guid)));
            else
                DisableGridSettings(guid);
        }

        void DisableGridSettings(object guid)
        {
            var gridview = _mainTab.GetTabComponent(guid).ResultView;

            gridview.SuspendLayout();

            gridview.CausesValidation = false;
            gridview.ColumnHeadersVisible = false;
            gridview.RowHeadersVisible = false;
            gridview.EnableHeadersVisualStyles = false;
            gridview.MultiSelect = false;
            gridview.ScrollBars = ScrollBars.None;
        }

        public void ResumeResultViewSettings(object guid)
        {
            if (_mainTab.GetTabComponent(guid).ResultView.InvokeRequired)
                this.Invoke(new Action(() => EnableGridSettings(guid)));
            else
                EnableGridSettings(guid);
        }

        void EnableGridSettings(object guid)
        {
            var gridview = _mainTab.GetTabComponent(guid).ResultView;

            gridview.CausesValidation = false;
            gridview.ColumnHeadersVisible = true;
            gridview.RowHeadersVisible = true;
            gridview.EnableHeadersVisualStyles = true;
            gridview.MultiSelect = true;
            gridview.ScrollBars = ScrollBars.Both;
            gridview.AdjustRowHeadersWidthToRowNumber();
            gridview.AutoResizeColumnHeadersHeight();

            gridview.ResumeLayout();
        }
        #endregion

        #region ResultView resize ColumnHeader height
        public void AdjustResultViewColumnHeaderHeight(object guid)
        {
            if (_mainTab.GetTabComponent(guid).ResultView.InvokeRequired)
                this.Invoke(new Action(() => ResizeResultViewColumnHeaderHeight(guid)));
            else
                ResizeResultViewColumnHeaderHeight(guid);
        }

        void ResizeResultViewColumnHeaderHeight(object guid)
        {
            _mainTab.GetTabComponent(guid).ResultView.AutoResizeColumnHeadersHeight();
        }
        #endregion

        #region ResultView resize Column width
        public void AdjustResultViewColumnsWidth(object guid, DataGridViewAutoSizeColumnsMode mode)
        {
            _mainTab.GetTabComponent(guid).ResultView.ResizeAllColumnsWidth();
        }
        #endregion

        #region ResultView resize RowHeader width
        public void AdjustResultViewRowHeaderWidth(object guid)
        {
            if (_mainTab.GetTabComponent(guid).ResultView.InvokeRequired)
                this.Invoke(new Action(() => ResizeResultViewRowHeaderWidth(guid)));
            else
                ResizeResultViewRowHeaderWidth(guid);
        }

        void ResizeResultViewRowHeaderWidth(object guid)
        {
            _mainTab.GetTabComponent(guid).ResultView.AdjustRowHeadersWidthToRowNumber();
        }
        #endregion

        #region ResultView
        public void DisableResultViewContextMenu()
        {
            _mainTab.DisableResultViewContextMenu();
        }

        public void EnableResultViewContextMenu()
        {
            _mainTab.EnableResultViewContextMenu();
        }

        public void DisableEditResult()
        {
            _mainTab.DisableEditResult();
        }

        public void EnableEditResult()
        {
            _mainTab.EnableEditResult();
        }
        
        public void ResetResultViewDataSource(object guid)
        {
            _mainTab.GetTabComponent(guid).ResultView.DataSource = null;
        }

        public object GetResultViewDataSource(object guid)
        {
            if (_mainTab.GetTabComponent(guid).ResultView.InvokeRequired)
                return this.Invoke(new Func<object>(() => _mainTab.GetTabComponent(guid).ResultView.DataSource));
            else
                return _mainTab.GetTabComponent(guid).ResultView.DataSource;
        }

        public void SetResultViewDataSource(object guid, object datasource)
        {
            if (_mainTab.GetTabComponent(guid).ResultView.InvokeRequired)
                this.Invoke(new Action(() => _mainTab.GetTabComponent(guid).ResultView.DataSource = datasource));
            else
                _mainTab.GetTabComponent(guid).ResultView.DataSource = datasource;
        }

        public void SetResultViewFocus(object guid)
        {
            _mainTab.GetTabComponent(guid).ResultView.Focus();
        }

        public void ClearResultViewSelection(object guid)
        {
            _mainTab.GetTabComponent(guid).ResultView.ClearSelection();
        }

        public Cell GetResultViewCurrentCell(object guid)
        {
            if (_mainTab.GetTabComponent(guid).ResultView.CurrentCell != null)
                return new Cell(_mainTab.GetTabComponent(guid).ResultView.CurrentCell.ColumnIndex, _mainTab.GetTabComponent(guid).ResultView.CurrentCell.RowIndex);
            else
                return Cell.Empty;
        }

        public Cell GetResultViewFirstSelectedCell(object guid)
        {
            return _mainTab.GetTabComponent(guid).ResultView.FirstSelectedCell;
        }

        public Cell GetResultViewLastSelectedCell(object guid)
        {
            return _mainTab.GetTabComponent(guid).ResultView.LastSelectedCell;
        }

        public int GetResultViewSelectedRowCount(object guid)
        {
            return _mainTab.GetTabComponent(guid).ResultView.SelectedRowCount;
        }

        public int GetResultViewSelectedColumnCount(object guid)
        {
            return _mainTab.GetTabComponent(guid).ResultView.SelectedColumnCount;
        }

        public void SetResultViewCurrentCell(object guid, Cell cell)
        {
            _mainTab.GetTabComponent(guid).ResultView.CurrentCell = _mainTab.GetTabComponent(guid).ResultView[cell.ColumnIndex, cell.RowIndex];
            _mainTab.GetTabComponent(guid).ResultView[cell.ColumnIndex, cell.RowIndex].Selected = true;
        }

        public void BringResultViewFront(object guid)
        {
            if (_mainTab.GetTabComponent(guid).InvokeRequired)
                this.Invoke(new Action(() => _mainTab.GetTabComponent(guid).BringResultViewFront()));
            else
                _mainTab.GetTabComponent(guid).BringResultViewFront();
        }
        #endregion

        #region LogView
        public void SetLogViewText(object guid, string text, DateTime? date, bool writeLine, string lineText = null)
        {
            _mainTab.GetTabComponent(guid).LogView.WriteLogData(text, date, writeLine, lineText);
        }

        public void BringLogViewFront(object guid)
        {
            if (_mainTab.GetTabComponent(guid).InvokeRequired)
                this.Invoke(new Action(() => _mainTab.GetTabComponent(guid).BringLogViewFront()));
            else
                _mainTab.GetTabComponent(guid).BringLogViewFront();
        }

        public void SetLogViewFocus(object guid)
        {
            _mainTab.GetTabComponent(guid).LogView.Focus();
        }

        public void ClearResult(object guid)
        {
            _mainTab.GetTabComponent(guid).LogView.Text = string.Empty;
        }

        #endregion

        #region Preference
        public void ApplyTabSetting()
        {
            _mainTab.ApplyPreference();
        }

        public void ApplyInputViewSetting()
        {
            _mainTab.ApplyPreferenceToInputView();
        }

        public void ApplyResultViewSetting()
        {
            _mainTab.ApplyPreferenceToResultView();
        }
        
        public void SetInputViewFont(object guid, Font font)
        {
            _mainTab.GetTabComponent(guid).InputView.Font = font;
        }

        public void SetResultViewFont(object guid, Font font)
        {
            _mainTab.GetTabComponent(guid).ResultView.Font = font;
            _mainTab.GetTabComponent(guid).LogView.Font = font;
        }
        #endregion

        #endregion
    }
}
