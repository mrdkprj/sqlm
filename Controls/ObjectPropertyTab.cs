using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MasudaManager.Utility.Preference;

namespace MasudaManager.Controls
{
    public partial class ObjectPropertyTab : UserControl, IMsdControl, IEnumerable<TabPage>
    {
        public event EventHandler CreateColumnSelectStmtClick;
        public event EventHandler CreateColumUpdateStmtClick;
        public event EventHandler CreateColumnDeleteStmtClick;

        Dictionary<DbObjectPropertyType, TabPage> _tabPageDictionary = new Dictionary<DbObjectPropertyType, TabPage>();
        Dictionary<DbObjectPropertyType, XDataGridView> _gridDictionary = new Dictionary<DbObjectPropertyType, XDataGridView>();

        public ObjectPropertyTab()
        {
            InitializeComponent();

            this.ContextMenuStrip = this.propertyTabContextMenu;

            this.Load += ObjectPropertyTab_Load;
            this.copyMenuItem.Click += OnCopyClick;
            this.createColumnSelectStmtMenuItem.Click += OnCreateColumnSelectStmtClick;
            this.createColumUpdateStmMenuItem.Click += OnCreateColumUpdateStmtClick;
            this.createColumnDeleteStmtMenuItem.Click += OnCreateColumnDeleteStmtClick;

            SetTabPageTag();
            PrepareDictionary();
            PrepareDataGridView();

            ApplyPreference();
        }

        #region Property
        public XDataGridView CurrentView { get { return _gridDictionary.GetValueOrDefault(CurrentPropertyType); } }
        public XDataGridView GeneralPropertyView { get { return this.GeneralPropertyGrid; } }
        public XDataGridView TableColumnView { get { return this.TableColumnGrid; } }
        public XDataGridView TableIndexView { get { return this.TableIndexGrid; } }
        public XDataGridView TableConstraintView { get { return this.TableConstraintGrid; } }
        public XDataGridView IndexColumnView { get { return this.IndexColumnGrid; } }
        public int CurrentIndex { get { return this.PropertyTab.SelectedIndex; } }
        public DbObjectPropertyType CurrentPropertyType { get { return (DbObjectPropertyType)this.PropertyTab.SelectedTab.Tag; } }
        #endregion

        #region Event handlers
        void ObjectPropertyTab_Load(object sender, EventArgs e)
        {
            UpdateAllTabPageWidth();
        }
        
        void OnCopyClick(object sender, EventArgs e)
        {
            _gridDictionary.GetValueOrDefault(CurrentPropertyType).CopyPlainText();
        }

        void OnCreateColumnSelectStmtClick(object sender, EventArgs e)
        {
            if (this.CreateColumnSelectStmtClick != null)
                this.CreateColumnSelectStmtClick(sender, e);
        }

        void OnCreateColumUpdateStmtClick(object sender, EventArgs e)
        {
            if (this.CreateColumUpdateStmtClick != null)
                this.CreateColumUpdateStmtClick(sender, e);
        }

        void OnCreateColumnDeleteStmtClick(object sender, EventArgs e)
        {
            if (this.CreateColumnDeleteStmtClick != null)
                this.CreateColumnDeleteStmtClick(sender, e);
        }
        #endregion

        #region SetTabPageTag
        void SetTabPageTag()
        {
            this.PropertyTab.TabPages[0].Tag = DbObjectPropertyType.GeneralProperty;
            this.PropertyTab.TabPages[1].Tag = DbObjectPropertyType.TableColumn;
            this.PropertyTab.TabPages[2].Tag = DbObjectPropertyType.TableIndex;
            this.PropertyTab.TabPages[3].Tag = DbObjectPropertyType.TableConstraint;
            this.PropertyTab.TabPages[4].Tag = DbObjectPropertyType.IndexColumn;
        }
        #endregion

        #region PrepareDictionary
        void PrepareDictionary()
        {
            foreach (TabPage tabPage in this.PropertyTab.TabPages)
            {
                _tabPageDictionary.Add((DbObjectPropertyType)tabPage.Tag, tabPage);
                _gridDictionary.Add((DbObjectPropertyType)tabPage.Tag, (XDataGridView)tabPage.Controls[0]);
            }
        }
        #endregion

        #region PrepareDataGridView
        void PrepareDataGridView()
        {
            foreach (var grid in _gridDictionary.Values)
            {
                grid.AllowRightClickCellSelect = true;
                grid.ThrowOnDataError = true;
            }
        }
        #endregion

        #region Get property gridview
        public XDataGridView GetPropertyGridView(DbObjectPropertyType propertyType)
        {
            return _gridDictionary.GetValueOrDefault(propertyType);
        }
        #endregion

        #region Set visible tabpage
        public void SetVisibleTabPage(DbObjectPropertyType propertyType)
        {
            this.PropertyTab.TabPages.Clear();

            foreach (var flag in GetSelectedDbObjectPropertyTypeFlags(propertyType))
            {
                this.PropertyTab.TabPages.Add(_tabPageDictionary[flag]);
            }
        }

        IEnumerable<DbObjectPropertyType> GetSelectedDbObjectPropertyTypeFlags(DbObjectPropertyType propertyType)
        {
            return Enum.GetValues(typeof(DbObjectPropertyType))
                       .Cast<DbObjectPropertyType>()
                       .Where(s => propertyType.HasFlag(s) && s != DbObjectPropertyType.None);
        }
        #endregion

        #region ResetPropertyGridViewDataSrouce
        public void ResetPropertyGridViewDataSrouce(DbObjectPropertyType propertyType)
        {
            foreach (var pair in _gridDictionary)
            {
                if (propertyType.HasFlag(pair.Key) && pair.Value.DataSource != null)
                    pair.Value.DataSource = null;
            }
        }
        #endregion

        #region UpdateAllTabPageWidth
        void UpdateAllTabPageWidth()
        {
            foreach (var pair in _tabPageDictionary)
            {
                pair.Value.Width = this.PropertyTab.DisplayRectangle.Width;
            }
        }
        #endregion

        #region Disable/Enable ContextMenu
        public void DisableContextMenu()
        {
            this.ContextMenuStrip = null;
        }

        public void EnableContextMenu()
        {
            this.ContextMenuStrip = this.propertyTabContextMenu;
        }
        #endregion

        #region ApplyPreference
        public void ApplyPreference()
        {
            foreach (var pair in _tabPageDictionary)
            {
                pair.Value.Font = UserPreference.Setting.List.Font;
            }
        }
        #endregion

        #region IEnumerable
        public IEnumerator<TabPage> GetEnumerator()
        {
            return _tabPageDictionary.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
