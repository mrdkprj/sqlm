using MasudaManager.Controls.Base;
using MasudaManager.Utility.Preference;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MasudaManager.Controls
{
    public partial class MainTabControl : XTabControl, IMsdControl
    {
        readonly string _tabPageTextLeftSpace = "        ";
        readonly string _tabPageTextFormat = "SQL{0}";
        readonly int _tabPagePadding = 3;
        readonly int _tabPageMargin = 3;
        readonly int _tabTextIncrementalValue = 1;
        int _tabCount = 0;
        //TabPage _newTabPage = null;
        XTabPage _newTabPage = null;
        MainTabComponent _newComponent = null;
        Dictionary<object, XTabPage> _tabPageDictionary = new Dictionary<object, XTabPage>();
        Dictionary<object, MainTabComponent> _componentDictionary = new Dictionary<object, MainTabComponent>();

        public MainTabControl()
        {
            InitializeComponent();

            this.ShowToolTips = true;
            this.ToolStripMode = UserPreference.Reflector.ToolStripMode;
            this.ShowCloseButton = true;
            this.AllowUserTabMove = true;
            this.Font = UserPreference.Setting.Tab.Font;
        }

        #region Property
        public TabToolStripMode ToolStripMode { get; private set; }
        public MainTabComponent SelectedTabComponent
        {
            //get { return GetTabComponent(this.SelectedTab.Tag); }
            get { return GetTabComponent(this.SelectedXTab.Guid); }
        }
        #endregion

        #region Events override
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

            if (this.ToolStripMode != TabToolStripMode.None)
                ShowToolStripText();
        }

        
        #endregion

        #region ShowToolStripText
        void ShowToolStripText()
        {
            //TabPage page = GetTabPageFromPoint(this.PointToClient(MousePosition));
            //if (page == null)
            //    return;

            //page.ToolTipText = GetToolStripText(page.Tag);
            XTabPage page = GetTabPageFromPoint(this.PointToClient(MousePosition)) as XTabPage;
            if (page == null)
                return;

            page.ToolTipText = GetToolStripText(page.Guid);
        }
        #endregion

        #region GetToolStripText
        string GetToolStripText(object guid)
        {
            //if (this.ToolStripMode == TabToolStripMode.InputText)
            //    return page.Text;

            MainTabComponent component = GetTabComponent(guid);

            if (component == null)
                return null;
            
            return component.InputView.FilePath ?? component.Text;
        }
        #endregion

        #region Add Maintab component
        public MainTabComponent AddMainTabComponent()
        {
            Guid guid = Guid.NewGuid();
            _tabCount += _tabTextIncrementalValue;

            _newComponent = new MainTabComponent();
            _newComponent.Dock = DockStyle.Fill;
            //_newComponent.Tag = guid;
            _newComponent.Guid = guid;
            _newComponent.Text = String.Format(_tabPageTextFormat, _tabCount.ToString());

            //_newTabPage = new TabPage(String.Format(_tabPageTextFormat, _tabCount.ToString()));
            _newTabPage = new XTabPage(String.Format(_tabPageTextFormat, _tabCount.ToString()));
            //_newTabPage.Text = String.Format(_tabPageTextFormat, _tabCount.ToString()) + _tabPageTextLeftSpace;
            _newTabPage.Text = _newComponent.Text + _tabPageTextLeftSpace;
            _newTabPage.Margin = new Padding(_tabPageMargin);
            _newTabPage.Padding = new Padding(_tabPagePadding);
            //_newTabPage.Tag = guid;
            _newTabPage.Guid = guid;
            _newTabPage.Controls.Add(_newComponent);

            _tabPageDictionary.Add(guid, _newTabPage);
            _componentDictionary.Add(guid, _newComponent);

            this.TabPages.Add(_newTabPage);
            this.SelectedTab = _newTabPage;

            return _newComponent;
        }
        #endregion

        #region SetTabPageText
        public void SetTabPageText(object guid, string text)
        {
            _tabPageDictionary.GetValueOrDefault(guid).Text = text + _tabPageTextLeftSpace;
        }
        #endregion

        #region SetTabPageInfoMessage
        public void SetTabPageInfoMessage(object guid, string message)
        {
            _tabPageDictionary.GetValueOrDefault(guid).InfoMessage = message;
        }
        #endregion

        #region SelectTabPage
        public void SelectTabPage(object guid)
        {
            this.SelectedTab = _tabPageDictionary[guid];
        }
        #endregion

        #region RemoveTabPage
        public void RemoveTabPage(object guid)
        {
            this.TabPages.Remove(_tabPageDictionary.GetValueOrDefault(guid));
            _tabPageDictionary.Remove(guid);
        }
        #endregion

        #region Get TabComponent
        public MainTabComponent GetTabComponent(object guid)
        {
            return _componentDictionary.GetValueOrDefault(guid);
        }
        #endregion

        #region Disable/Enable ContextMenu
        public void DisableEditResult()
        {
            foreach (var component in _componentDictionary)
            {
                component.Value.AllowEditResults = false;
            }
        }

        public void EnableEditResult()
        {
            foreach (var component in _componentDictionary)
            {
                component.Value.AllowEditResults = true;
            }
        }

        public void DisableResultViewContextMenu()
        {
            foreach (var component in _componentDictionary)
            {
                component.Value.ResultViewContextMenuEnabled = false;
            }
        }

        public void EnableResultViewContextMenu()
        {
            foreach (var component in _componentDictionary)
            {
                component.Value.ResultViewContextMenuEnabled = true;
            }
        }
        #endregion

        #region ApplyPreference
        public void ApplyPreference()
        {
            if (!this.Font.Equals(UserPreference.Setting.Tab.Font))
                this.Font = UserPreference.Setting.Tab.Font;
            
            this.ToolStripMode = UserPreference.Reflector.ToolStripMode;
        }

        public void ApplyPreferenceToInputView()
        {
            foreach (var component in _componentDictionary.Values)
            {
                component.InputView.ApplyPreference();
            }
        }

        public void ApplyPreferenceToResultView()
        {
            foreach (var component in _componentDictionary.Values)
            {
                component.ResultView.ApplyPreference();
                component.LogView.ApplyPreference();
            }
        }
        #endregion
    }
}
