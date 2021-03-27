using MasudaManager.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MasudaManager.Views
{
    public partial class SearchGridView : SearchGridViewSlice, ISearchGridView
    {
        SearchMode _searchMode;
        
        public event EventHandler<ShowSearchGridEventArgs> Initiated;
        public event EventHandler DisposeDialogRequested;
        public event EventHandler SearchBackwardButtonClicked;
        public event EventHandler SearchForwardButtonClicked;

        public SearchGridView()
        {
            InitializeComponent();
            PrepareFormText();

            this.combSearchText.Focus();
            this.rdPartial.Checked = true;
            _searchMode = SearchMode.Partial;

            SetTabIndex();
            PrepareSearchOption();
        }

        #region PrepareFormText
        void PrepareFormText()
        {
            this.Text = LocalizedTextProvider.Form.Search;
            this.btNext.Text = LocalizedTextProvider.Form.Next;
            this.btPrevious.Text = LocalizedTextProvider.Form.Previous;
            this.btClose.Text = LocalizedTextProvider.Form.Close;
            this.grpMode.Text = LocalizedTextProvider.Form.SearchMode;
            this.rdExact.Text = LocalizedTextProvider.Form.SearchModeExact;
            this.rdPartial.Text = LocalizedTextProvider.Form.SearchModePartial;
            this.rdPrefix.Text = LocalizedTextProvider.Form.SearchModePrefix;
            this.rdSuffix.Text = LocalizedTextProvider.Form.SearchModeSuffix;
            this.grpSearchOption.Text = LocalizedTextProvider.Form.SearchOption;
            this.chkSearchHeader.Text = LocalizedTextProvider.Form.SearchOptionSearchHeader;
            this.chkAutoClose.Text = LocalizedTextProvider.Form.SearchOptionCloseOnSearch;
            this.chkCaseSensitive.Text = LocalizedTextProvider.Form.SearchOptionCaseSensitive;
        }
        #endregion

        #region SetTabIndex
        void SetTabIndex()
        {
            this.combSearchText.TabIndex = 0;
            this.grpMode.TabIndex = 1;
            this.grpSearchOption.TabIndex = 2;
            this.btNext.TabIndex = 3;
            this.btPrevious.TabIndex = 4;
            this.btClose.TabIndex = 5;
        }
        #endregion

        #region Prepare search option
        void PrepareSearchOption()
        {
            this.chkSearchHeader.Tag = SearchOptionFlags.SearchHeader;
            this.chkCaseSensitive.Tag = SearchOptionFlags.CaseSensitive;
            this.chkAutoClose.Tag = SearchOptionFlags.CloseDialog;
        }
        #endregion

        #region ProcessCmdKey
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    HideView();
                    return true;
                case Keys.F3:
                    SearchForwardButtonClicked(this, EventArgs.Empty);
                    return true;
                case Keys.Shift | Keys.F3:
                    SearchBackwardButtonClicked(this, EventArgs.Empty);
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region EventHandlers

        #region View activated
        private void SearchGrid_Activated(object sender, EventArgs e)
        {
            this.combSearchText.Focus();
        }
        #endregion

        #region Closing
        private void SearchGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            HideView();
        }
        #endregion

        #region Close button click
        private void btClose_Click(object sender, EventArgs e)
        {
            HideView();
        }
        #endregion      

        #region Search text enter
        private void combSearchText_Enter(object sender, EventArgs e)
        {
            this.combSearchText.SelectAll();
        }
        #endregion

        #region Search text keydown
        private void combSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.ActiveControl = this.btNext;
                SearchForwardButtonClicked(sender, e);
            }
        }
        #endregion
        
        #region Next/Prev button click
        private void btNext_Click(object sender, EventArgs e)
        {
            SearchForwardButtonClicked(sender, e);
        }

        private void btPrevious_Click(object sender, EventArgs e)
        {
            SearchBackwardButtonClicked(sender, e);
        }
        #endregion

        #region SearchMode radio buttons
        private void rdExact_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdExact.Checked)
                _searchMode = SearchMode.Exact;
        }

        private void rdPartial_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdPartial.Checked)
                _searchMode = SearchMode.Partial;
        }

        private void rdPrefix_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdPrefix.Checked)
                _searchMode = SearchMode.Prefix;
        }

        private void rdSuffix_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdSuffix.Checked)
                _searchMode = SearchMode.Suffix;
        }
        #endregion

        #endregion

        #region Methods

        #region Initiate
        public void Initiate(IWin32Window owner, SearchViewRequestData requestData)
        {
            Initiated(owner, new ShowSearchGridEventArgs(requestData));
        }

        public void ShowModeless()
        {
            if (this.Visible)
                this.BringToFront();
            else
                this.Show();
        }

        #endregion

        #region HideView
        public void HideView()
        {
            this.Visible = false;
        }
        #endregion

        #region DisposeDialog
        public void DisposeDialog()
        {
            DisposeDialogRequested(this, EventArgs.Empty);
        }
        #endregion

        #region GetSearchString
        public string GetSearchString()
        {
            return this.combSearchText.Text;
        }
        #endregion

        #region Add search string
        public void AddSearchedString(string searchedString)
        {
            this.combSearchText.Items.Add(searchedString);
        }
        #endregion
        
        #region SearchMode

        public SearchMode SearchMode { get { return _searchMode; } }

        #endregion

        #region GetSearchOption
        public SearchOptionFlags GetSearchOption()
        {
            SearchOptionFlags option = SearchOptionFlags.None;

            foreach (Control control in this.grpSearchOption.Controls)
            {
                CheckBox checkBox = control as CheckBox;
                if (checkBox != null && checkBox.Checked)
                    option = option | (SearchOptionFlags)checkBox.Tag;
            }

            return option;
        }
        #endregion

        #endregion
    }

}