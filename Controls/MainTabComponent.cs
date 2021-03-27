using MasudaManager.Utility;
using MasudaManager.Utility.Preference;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace MasudaManager.Controls
{
    public partial class MainTabComponent : UserControl, IMsdControl, IEnumerable<Control>
    {
        public event EventHandler InputViewSaveStatusChanged;
        public event EventHandler AutoResizeHeadersClicked;
        public event EventHandler CopyFromObjectViewClick;
        public event EventHandler CopyToObjectViewFilterClick;
        public event EventHandler CopyToPropertyViewFilterClick;
        public event EventHandler CopyTextClick;
        public event EventHandler CopyHeaderClick;
        public event EventHandler CopyTextWithHeaderClick;
        public event EventHandler EditResultClick;
        public event EventHandler ClearResultClick;

        const int SWITCHABLE_CONTROL_COUNT = 2;
        readonly int _logViewIndent = 5;
        List<Control> _componentList = new List<Control>();
        Queue<Control> _switchableControls = new Queue<Control>(SWITCHABLE_CONTROL_COUNT);

        public MainTabComponent()
        {
            InitializeComponent();

            PrepareContextMenu();
            PrepareComponents();

            this.XtcResultView.TabStop = true;
            this.XtcLogView.TabStop = false;

            _switchableControls.Enqueue(this.XtcResultView);
            _switchableControls.Enqueue(this.XtcLogView);

            _componentList.Add(this.XtcInputView);
            _componentList.Add(this.XtcResultView);
            _componentList.Add(this.XtcLogView);

            this.ResultViewContextMenuEnabled = true;
        }

        #region Property
        public Guid Guid { get; set; }
        public bool ResultViewContextMenuEnabled { get; set; }
        public bool AllowEditResults
        {
            get { return this.ResultViewEditResultMenuItem.Enabled; }
            set { this.ResultViewEditResultMenuItem.Enabled = value; }
        }
        public SqlResultView ResultView { get { return this.XtcResultView; } }
        public SqlInputView InputView { get { return this.XtcInputView; } }
        public SqlLogView LogView { get { return this.XtcLogView; } }
        #endregion

        #region Prepare ContextMenu
        void PrepareContextMenu()
        {
            PrepareInputViewContextMenu();
            PrepareResultViewContextMenu();
        }

        void PrepareInputViewContextMenu()
        {
            this.InputViewCutMenuItem.Text = LocalizedTextProvider.ContextMenu.Cut;
            this.InputViewCopyMenuItem.Text = LocalizedTextProvider.ContextMenu.Copy;
            this.InputViewPasteMenuItem.Text = LocalizedTextProvider.ContextMenu.Paste;
            this.InputViewCopyFromObjectViewMenuItem.Text = LocalizedTextProvider.ContextMenu.CopyFromObjectView;
            this.InputViewCopyToObjectViewFilterMenuItem.Text = LocalizedTextProvider.ContextMenu.CopyToObjectViewFilter;
            this.InputViewCopyToPropertyViewFilterMenuItem.Text = LocalizedTextProvider.ContextMenu.CopyToPropertyViewFilter;
            this.InputViewZoomInMenuItem.Text = LocalizedTextProvider.ContextMenu.ZoomIn;
            this.InputViewZoomOutMenuItem.Text = LocalizedTextProvider.ContextMenu.ZoomOut;
            this.InputViewResetZoomMenuItem.Text = LocalizedTextProvider.ContextMenu.ResetZoom;
            
            this.InputViewCutMenuItem.Click += OnCutClick;
            this.InputViewCopyMenuItem.Click += OnCopyClick;
            this.InputViewPasteMenuItem.Click += OnPasteClick;
            this.InputViewCopyFromObjectViewMenuItem.Click += OnCopyFromObjectViewClick;
            this.InputViewCopyToObjectViewFilterMenuItem.Click += OnCopyToObjectViewFilterClick;
            this.InputViewCopyToPropertyViewFilterMenuItem.Click += OnCopyToPropertyViewFilterClick;
            this.InputViewToUpperMenuItem.Click += OnToUpperCaseClick;
            this.InputViewToLowerMenuItem.Click += OnToLowerCaseClick;
            this.InputViewZoomInMenuItem.Click += OnZoomInClick;
            this.InputViewZoomOutMenuItem.Click += OnZoomOutClick;
            this.InputViewResetZoomMenuItem.Click += OnResetZoomClick;
        }

        void PrepareResultViewContextMenu()
        {
            this.ResultViewAutoResizeHeaderMenuItem.Text = LocalizedTextProvider.ContextMenu.AdjustHeaderWidth;
            this.ResultViewCopyTextMenuItem.Text = LocalizedTextProvider.ContextMenu.CopyText;
            this.ResultViewCopyHeaderMenuItem.Text = LocalizedTextProvider.ContextMenu.CopyHeader;
            this.ResultViewCopyTextWithHeaderMenuItem.Text = LocalizedTextProvider.ContextMenu.CopyTextWithHeader;
            this.ResultViewEditResultMenuItem.Text = LocalizedTextProvider.ContextMenu.Edit;
            this.ResultViewClearResultMenuItem.Text = LocalizedTextProvider.ContextMenu.ClearLog;
            this.ResultViewSwitchViewMenuItem.Text = LocalizedTextProvider.ContextMenu.SwitchView; 
            
            this.ResultViewAutoResizeHeaderMenuItem.Click += OnAutoResizeHeadersClick;
            this.ResultViewCopyTextMenuItem.Click += OnCopyTextClick;
            this.ResultViewCopyHeaderMenuItem.Click += OnCopyHeaderClick;
            this.ResultViewCopyTextWithHeaderMenuItem.Click += OnCopyTextWithHeaderClick;
            this.ResultViewEditResultMenuItem.Click += OnEditResultsClick;
            this.ResultViewClearResultMenuItem.Click += OnClearResultClick;
            this.ResultViewSwitchViewMenuItem.Click += OnSwitchViewClick;
        }
        #endregion

        #region PrepareComponents
        void PrepareComponents()
        {
            PrepareInputView();
            PrepareResultView();
            PrepareLogView();
        }

        void PrepareInputView()
        {
            this.XtcInputView.AllowDrop = true;
            this.XtcInputView.Font = UserPreference.Setting.Input.Font;
            this.XtcInputView.SaveStatusChanged += OnSaveStatusChanged;
            this.XtcInputView.ContextMenuStrip = this.InputViewContextMenu;
        }

        void PrepareResultView()
        {
            this.XtcResultView.DefaultCellStyle.SelectionBackColor = Color.Aqua;
            this.XtcResultView.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.XtcResultView.Font = UserPreference.Setting.Output.Font;
            this.XtcResultView.DisplayRowNumber = UserPreference.Setting.Output.DisplayRowNumber;
            this.XtcResultView.DisplaySpaceCharacter = UserPreference.Setting.Output.DisplaySpaceCharacter;
            this.XtcResultView.ForcePlainTextCopy = false;
            this.XtcResultView.ContextMenuStrip = this.ResultViewContextMenu;
            this.XtcResultView.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            this.XtcResultView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.XtcResultView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.XtcResultView.ThrowOnDataError = true;
        }

        void PrepareLogView()
        {
            this.XtcLogView.SelectionIndent = _logViewIndent;
            this.XtcLogView.Font = UserPreference.Setting.Output.Font;
            this.XtcLogView.ContextMenuStrip = this.ResultViewContextMenu;
        }
        #endregion

        #region Event handlers
        void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (!this.ResultViewContextMenuEnabled)
                e.Cancel = true;
        }

        void OnSaveStatusChanged(object sender, EventArgs e)
        {
            if (this.InputViewSaveStatusChanged != null)
                InputViewSaveStatusChanged(this.Guid, e);
        }

        void OnCutClick(object sender, EventArgs e)
        {
            this.XtcInputView.Cut();
        }

        void OnCopyClick(object sender, EventArgs e)
        {
            this.XtcInputView.Copy();
        }

        void OnPasteClick(object sender, EventArgs e)
        {
            this.XtcInputView.Paste();
            this.XtcInputView.ApplySetting();
        }

        void OnCopyFromObjectViewClick(object sender, EventArgs e)
        {
            if (this.CopyFromObjectViewClick != null)
                this.CopyFromObjectViewClick(sender, e);
        }
        
        void OnCopyToObjectViewFilterClick(object sender, EventArgs e)
        {
            if (this.CopyToObjectViewFilterClick != null)
                this.CopyToObjectViewFilterClick(sender, e);
        }

        void OnCopyToPropertyViewFilterClick(object sender, EventArgs e)
        {
            if (this.CopyToPropertyViewFilterClick != null)
                this.CopyToPropertyViewFilterClick(sender, e);
        }

        void OnToUpperCaseClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.XtcInputView.SelectedText))
                this.XtcInputView.ReplaceSelection(this.XtcInputView.SelectedText.ToUpper());
        }

        void OnToLowerCaseClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.XtcInputView.SelectedText))
                this.XtcInputView.ReplaceSelection(this.XtcInputView.SelectedText.ToLower());
        }

        void OnZoomInClick(object sender, EventArgs e)
        {
            this.XtcInputView.ZoomIn();
        }

        void OnZoomOutClick(object sender, EventArgs e)
        {
            this.XtcInputView.ZoomOut();
        }

        private void OnResetZoomClick(object sender, EventArgs e)
        {
            this.XtcInputView.Zoom = 0;
        }

        void OnAutoResizeHeadersClick(object sender, EventArgs e)
        {
            if (this.AutoResizeHeadersClicked != null)
                this.AutoResizeHeadersClicked(sender, e);
        }

        void OnCopyTextClick(object sender, EventArgs e)
        {
            if (this.CopyTextClick != null)
                this.CopyTextClick(sender, e);
        }

        void OnCopyHeaderClick(object sender, EventArgs e)
        {
            if (this.CopyHeaderClick != null)
                this.CopyHeaderClick(sender, e);
        }

        void OnCopyTextWithHeaderClick(object sender, EventArgs e)
        {
            if (this.CopyTextWithHeaderClick != null)
                this.CopyTextWithHeaderClick(sender, e);
        }

        void OnEditResultsClick(object sender, EventArgs e)
        {
            if (this.EditResultClick != null)
                this.EditResultClick(sender, e);
        }

        void OnClearResultClick(object sender, EventArgs e)
        {
            if (this.ClearResultClick != null)
                this.ClearResultClick(sender, e);
        }

        void OnSwitchViewClick(object sender, EventArgs e)
        {
            SwitchComponent();
        }
        #endregion
        
        #region SwitchComponent
        void SwitchComponent()
        {
            _switchableControls.Peek().TabStop = false;
            _switchableControls.Enqueue(_switchableControls.Dequeue());
            _switchableControls.Peek().BringToFront();
        }
        #endregion

        #region BringResultViewFront
        public void BringResultViewFront()
        {
            while (!_switchableControls.Peek().Equals(this.XtcResultView))
            {
                _switchableControls.Peek().TabStop = false;
                _switchableControls.Enqueue(_switchableControls.Dequeue());
            }

            _switchableControls.Peek().TabStop = true;
            _switchableControls.Peek().BringToFront();
        }
        #endregion

        #region BringLogViewFront
        public void BringLogViewFront()
        {
            while (!_switchableControls.Peek().Equals(this.XtcLogView))
            {
                _switchableControls.Peek().TabStop = false;
                _switchableControls.Enqueue(_switchableControls.Dequeue());
            }

            _switchableControls.Peek().TabStop = true;
            _switchableControls.Peek().BringToFront();
        }
        #endregion
        
        #region ResultViwe KeyDown
        private void XtcGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.C:
                    OnCopyTextClick(sender, e);
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.H:
                    OnCopyTextWithHeaderClick(sender, e);
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.S:
                    SwitchComponent();
                    e.Handled = true;
                    break;
            }
        }
        #endregion

        #region LogView KeyDown
        private void XtcLogView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.S:
                    SwitchComponent();
                    e.Handled = true;
                    break;
            }
        }
        #endregion

        #region ApplyPreference
        public void ApplyPreference()
        {
            this.XtcResultView.ApplyPreference();
            this.XtcInputView.ApplyPreference();
            this.XtcLogView.ApplyPreference();
        }
        #endregion

        #region IEnumerable<T>
        public IEnumerator<Control> GetEnumerator()
        {
            return _componentList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
