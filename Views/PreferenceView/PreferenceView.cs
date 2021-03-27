using MasudaManager.Utility.Preference;
using MasudaManager.Utility.Preference.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MasudaManager.Views
{
    public partial class PreferenceView : PreferenceViewSlice, IPreferenceView, IChildView<object>
    {
        IWin32Window _owner;

        public event EventHandler ComponentInitialized;
        public event EventHandler<GenericEventArgs<object>> Initiated;
        public event EventHandler<TreeViewCancelEventArgs> NodeSelectionChanging;
        public event EventHandler NodeSelectionChanged;
        public event EventHandler OkButtonClicked;
        public event EventHandler ResetButtonClicked;
        public event EventHandler CancelButtonClicked;
        public event EventHandler CloseButtonClicked;
        public event EventHandler<CancelEventArgs> ViewClosing;
        public event EventHandler<CancelEventArgs> ReleaseRequested;
        
        public PreferenceView()
        {
            InitializeComponent();
            PrepareFotmText();

            this.splitContainer1.TabStop = false;
            this.splitContainer2.TabStop = false;

            ComponentInitialized(this, EventArgs.Empty);
        }

        public bool SuppressFormClosingEvent { get; set; }
        public Action ViewClosedAction { get; set; }

        #region EventHandlers
        private void menuTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            NodeSelectionChanging(sender, e);
        }

        private void menuTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NodeSelectionChanged(sender, EventArgs.Empty);
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            OkButtonClicked(sender, e);
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            ResetButtonClicked(sender, e);
        }
        
        private void btCancel_Click(object sender, EventArgs e)
        {
            CancelButtonClicked(sender, e);
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            CloseButtonClicked(sender, e);
        }

        private void PreferenceView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.SuppressFormClosingEvent)
                return;

            ViewClosing(sender, e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    CloseButtonClicked(this, EventArgs.Empty);
                    return true;
            }

            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region Methods

        #region PrepareFotmText
        void PrepareFotmText()
        {
            this.Text = LocalizedTextProvider.Form.Preference;
            this.btOK.Text = LocalizedTextProvider.Form.OK;
            this.btReset.Text = LocalizedTextProvider.Form.Reset;
            this.btCancel.Text = LocalizedTextProvider.Form.Cancel;
            this.btClose.Text = LocalizedTextProvider.Form.Close;
        }
        #endregion

        #region Initiate
        public void Initiate(IWin32Window owner, GenericEventArgs<object> args)
        {
            _owner = owner;
            Initiated(owner, args);
        }
        #endregion

        #region Show
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

        #region TreeView
        public void FocusOnTreeView()
        {
            this.ActiveControl = this.menuTree;
        }

        public void AddTreeNode(NodeData nodeData)
        {
            this.menuTree.AddNode(nodeData);
        }

        public void AddChildNode(NodeData nodeData)
        {
            this.menuTree.AddChildNode(nodeData);
        }

        public void ExpandAllNodes()
        {
            this.menuTree.ExpandAll();
        }
        #endregion

        #region Panel
        public PreferencePanelType CurrentPanelType
        {
            get { return this.menuTree.CurrentPanelType; }
        }

        public void SetPanels(IEnumerable<PreferencePanelBase> panels)
        {
            foreach (var panel in panels)
            {
                this.splitContainer1.Panel2.Controls.Add(panel);
            }
        }
        #endregion

        #endregion
    }
}