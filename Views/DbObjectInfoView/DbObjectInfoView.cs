using MasudaManager.Controls;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MasudaManager.Views
{
    public partial class DbObjectInfoView : DbObjectInfoViewSlice, IDbObjectInfoView
    {
        readonly int _objectViewStartTabIndex = 200;
        readonly int _propertyViewStartTabIndex = 300;
        readonly int _tabIndexIncrementalValue = 10;
        readonly int _maxTabIndex = 9999;
        int _objectListCurrentIndex = -1;
        ObjectView _objectView = new ObjectView();
        ObjectPropertyTab _propertyView = new ObjectPropertyTab();
  
        #region Events
        public event EventHandler Loaded;
        public event EventHandler CurrentDbObjectNameRequested;
        public event EventHandler ObjectListSelectionChanged;
        public event EventHandler ObjectViewSelectionChanged;
        public event EventHandler<MouseEventArgs> ObjectViewItemDoubleClicked;
        public event EventHandler ObjectViewFilterTextChanged;
        public event EventHandler RefreshButtonClicked;
        public event EventHandler DisplayDataClicked;
        public event EventHandler CreateSelectStmtClicked;
        public event EventHandler CreateSelectCountStmtClicked;
        public event EventHandler CreateInsertStmtClicked;
        public event EventHandler CreateDeleteStmtClicked;
        public event EventHandler EditResultClicked;
        public event EventHandler ExportClicked;
        public event EventHandler ImportClicked;
        public event EventHandler<MouseEventArgs> PropertyViewItemDoubleClicked;
        public event EventHandler PropertyViewFilterTextChanged;
        #endregion
        
        #region Constructor
        public DbObjectInfoView()
        {
            InitializeComponent();

            CreateObjectView();
            CreatePropertyTab();

            SetObjectViewTabIndex();
            SetPropertyViewTabIndex();

            this.splitContainer1.TabStop = false;
            this.splitContainer1.TabIndex = _maxTabIndex;
            this.grpboxObject.TabStop = false;
            this.grpboxObject.TabIndex = _maxTabIndex;
            this.btRefreshObjectView.TabStop = false;
            this.btRefreshObjectView.TabIndex = _maxTabIndex;
            this.grpboxObjectProperty.TabStop = false;
            this.grpboxObjectProperty.TabIndex = _maxTabIndex;
        }
        #endregion

        #region CreateObjectView
        void CreateObjectView()
        {
            _objectView.Name = "ObjectView";
            _objectView.Dock = DockStyle.Fill;
            _objectView.TabStop = true;
            _objectView.SelectionChanged += this.ObjectView_SelectionChanged;
            _objectView.MouseDoubleClick += this.ObjectView_MouseDoubleClick;
            _objectView.DisplayDataClick += this.ObjectView_DisplayDataClick;
            _objectView.CreateSelectStmtClick += this.ObjectView_CreateSelectStmtClick;
            _objectView.CreateSelectCountStmtClick += this.ObjectView_CreateSelectCountStmtClick;
            _objectView.CreateInsertStmtClick += this.ObjectView_CreateInsertStmtClick;
            _objectView.CreateDeleteStmtClick += this.ObjectView_CreateDeleteStmtClick;
            _objectView.ExportClick += this.ObjectView_ExportClick;
            _objectView.ImportClick += this.ObjectView_ImportClick;
            _objectView.EditResultClick += this.ObjectView_EditResultClick;
            this.objectViewPanel.Controls.Add(_objectView);
        }

        void SetObjectViewTabIndex()
        {
            int count = _tabIndexIncrementalValue;

            this.cmbObjectList.TabIndex = _objectViewStartTabIndex;
            this.txtFilterObject.TabIndex = _objectViewStartTabIndex + count;
            count += _tabIndexIncrementalValue;
            _objectView.TabIndex = _objectViewStartTabIndex + count;
        }
        #endregion

        #region CreatePropertyTab
        void CreatePropertyTab()
        {
            _propertyView.Name = "PropertyView";
            _propertyView.Dock = DockStyle.Fill;
            _propertyView.TabStop = true;
            _propertyView.GeneralPropertyView.MouseDoubleClick += PropertyView_MouseDoubleClick;
            _propertyView.TableColumnView.MouseDoubleClick += PropertyView_MouseDoubleClick;
            _propertyView.TableIndexView.MouseDoubleClick += PropertyView_MouseDoubleClick;
            _propertyView.TableConstraintView.MouseDoubleClick += PropertyView_MouseDoubleClick;
            _propertyView.IndexColumnView.MouseDoubleClick += PropertyView_MouseDoubleClick;
            this.propertyTabPanel.Controls.Add(_propertyView);
        }

        void SetPropertyViewTabIndex()
        {
            this.txtFilterProperty.TabIndex = _propertyViewStartTabIndex;

            int count = _tabIndexIncrementalValue;

            foreach (Control page in _propertyView)
            {
                page.Controls[0].TabStop = true;
                page.Controls[0].TabIndex = _propertyViewStartTabIndex + count;
                count += _tabIndexIncrementalValue;
            }
        }
        #endregion

        #region EventHandlers

        #region View
        private void DbObjectInfoView_Load(object sender, EventArgs e)
        {
            Loaded(sender, e);
        }
        #endregion

        #region ObjectList
        void cmbObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_objectListCurrentIndex != this.cmbObjectList.SelectedIndex)
            {
                _objectListCurrentIndex = this.cmbObjectList.SelectedIndex;
                ObjectListSelectionChanged(this, e);
            }
        }
        #endregion

        #region Refresh button
        void btRefreshObjectView_Click(object sender, EventArgs e)
        {
            RefreshButtonClicked(sender, e);
        }
        #endregion

        #region ObjectView

        private void ObjectView_SelectionChanged(object sender, EventArgs e)
        {
            ObjectViewSelectionChanged(sender, e);
        }

        private void ObjectView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ObjectViewItemDoubleClicked(sender, e);
        }

        private void txtFilterObject_TextChanged(object sender, EventArgs e)
        {
            ObjectViewFilterTextChanged(sender, e);
        }

        private void ObjectView_DisplayDataClick(object sender, EventArgs e)
        {
            DisplayDataClicked(sender, e);
        }

        private void ObjectView_CreateSelectStmtClick(object sender, EventArgs e)
        {
            CreateSelectStmtClicked(sender, e);
        }

        private void ObjectView_CreateSelectCountStmtClick(object sender, EventArgs e)
        {
            CreateSelectCountStmtClicked(sender, e);
        }

        private void ObjectView_CreateInsertStmtClick(object sender, EventArgs e)
        {
            CreateInsertStmtClicked(sender, e);
        }

        private void ObjectView_CreateDeleteStmtClick(object sender, EventArgs e)
        {
            CreateDeleteStmtClicked(sender, e);
        }

        private void ObjectView_ExportClick(object sender, EventArgs e)
        {
            ExportClicked(sender, e);
        }

        private void ObjectView_ImportClick(object sender, EventArgs e)
        {
            ImportClicked(sender, e);
        }

        void ObjectView_EditResultClick(object sender, EventArgs e)
        {
            EditResultClicked(sender, e);
        }
        #endregion

        #region PropertyTab

        private void PropertyView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PropertyViewItemDoubleClicked(sender, e);
        }

        private void txtFilterProperty_TextChanged(object sender, EventArgs e)
        {
            PropertyViewFilterTextChanged(sender, e);
        }

        #endregion

        #endregion

        #region Methods

        #region GetInvoker
        public ISynchronizeInvoke GetInvoker()
        {
            return this;
        }
        #endregion
       
        #region Enable/Disable
        public void DisableContextMenu()
        {
            _objectView.DisableContextMenu();
            _propertyView.DisableContextMenu();
        }

        public void EnableContextMenu()
        {
            _objectView.EnableContextMenu();
            _propertyView.EnableContextMenu();
        }

        public bool RefreshButtonEnabled
        {
            get { return this.btRefreshObjectView.Enabled; }
            set 
            {
                if (this.btRefreshObjectView.InvokeRequired)
                    this.Invoke(new Action(() => this.btRefreshObjectView.Enabled = value));
                else
                    this.btRefreshObjectView.Enabled = value;
            }
        }

        public bool DisplayDataEnabled
        {
            get { return _objectView.AllowDisplayData; }
            set { _objectView.AllowDisplayData = value; }
        }

        public bool CreateSqlEnabled
        {
            get { return _objectView.AllowCreateSql; }
            set { _objectView.AllowCreateSql = value; }
        }

        public bool EditResultEnabled
        {
            get { return _objectView.AllowEditData; }
            set { _objectView.AllowEditData = value; }
        }

        public bool ExportEnabled
        {
            get { return _objectView.AllowExport; }
            set { _objectView.AllowExport = value; }
        }

        public bool ImportEnabled
        {
            get { return _objectView.AllowImport; }
            set { _objectView.AllowImport = value; }
        }
        #endregion

        #region ObjectList
        public bool IsObjectListDataBound()
        {
            return this.cmbObjectList.DataSource != null;
        }

        public void SetObjectListDataSource(object datasource)
        {
            if (this.cmbObjectList.InvokeRequired)
                this.Invoke(new Action(() => this.cmbObjectList.DataSource = datasource));
            else
                this.cmbObjectList.DataSource = datasource;
        }

        public int ObjectListSelectedIndex { get { return this.cmbObjectList.SelectedIndex; } }

        #endregion

        #region ObjectView
        public Cell ObjectViewCurrentCell
        {
            get
            {
                if (_objectView.CurrentCell == null)
                    return Cell.Empty;
                else
                    return new Cell(_objectView.CurrentCell.ColumnIndex, _objectView.CurrentCell.RowIndex);
            }
        }

        public object ObjectViewCurrentValue
        {
            get
            {
                if (_objectView.CurrentCell == null)
                    return null;
                else
                    return _objectView.CurrentCell.Value;
            }
        }

        public object GetObjectViewCellValue(Cell cell)
        {
            return _objectView[cell.ColumnIndex, cell.RowIndex].Value;
        }
       
        public string GetSelectedDbObjectName()
        {
            CurrentDbObjectNameRequested(this, EventArgs.Empty);
            return this.Model.SelectedDbObjectName;
        }

        public void SetObjectViewDataSource(object datasource)
        {
            if (_objectView.InvokeRequired)
                this.Invoke(new Action(() => _objectView.DataSource = datasource));
            else
                _objectView.DataSource = datasource;
        }

        public string ObjectViewFilterText
        {
            get { return this.txtFilterObject.Text; }
            set { this.txtFilterObject.Text = value; }
        }

        public void FocusOnObjectViewFilter()
        {
            this.txtFilterObject.Focus();
        }

        public void AdjustObjectViewColumnsWidth(DataGridViewAutoSizeColumnMode autoSizeMode)
        {
            if (_objectView.InvokeRequired)
                this.Invoke(new Action(() => _objectView.ForceAutoResizeColumnsWidth(autoSizeMode)));
            else
                _objectView.ForceAutoResizeColumnsWidth(autoSizeMode);
        }
        
        #endregion

        #region PropertyTab
        public Cell PropertyViewCurrentCell
        {
            get
            {
                if (_propertyView.CurrentView.CurrentCell == null)
                    return Cell.Empty;
                else
                    return new Cell(_propertyView.CurrentView.CurrentCell.ColumnIndex, _propertyView.CurrentView.CurrentCell.RowIndex);
            }
        }

        public object PropertyViewCurrentValue
        {
            get
            {
                if (_propertyView.CurrentView.CurrentCell == null)
                    return null;
                else
                    return _propertyView.CurrentView.CurrentCell.Value;
            }
        }

        public object GetPropertyViewCellValue(Cell cell)
        {
            return _propertyView.CurrentView[cell.ColumnIndex, cell.RowIndex].Value;
        }
        
        public void ClearPropertyViews(DbObjectPropertyType dbObjectPropertyType)
        {
            if (_propertyView.InvokeRequired)
                this.Invoke(new Action(() => _propertyView.ResetPropertyGridViewDataSrouce(dbObjectPropertyType)));
            else
                _propertyView.ResetPropertyGridViewDataSrouce(dbObjectPropertyType);
        }

        public void SetVisibleProperty(DbObjectPropertyType dbObjectPropertyType)
        {
            _propertyView.SetVisibleTabPage(dbObjectPropertyType);
            SetPropertyViewTabIndex();
        }

        public string PropertyViewFilterText
        {
            get { return this.txtFilterProperty.Text; }
            set { this.txtFilterProperty.Text = value; }
        }

        public void SetPropertyViewObjectName(string objectName)
        {
            if (this.grpboxObjectProperty.InvokeRequired)
                this.Invoke(new Action(() => this.grpboxObjectProperty.Text = objectName));
            else
                this.grpboxObjectProperty.Text = objectName;
        }

        public DbObjectPropertyType SelectedPropertyType
        {
            get { return _propertyView.CurrentPropertyType; }
        }

        public void AdjustPropertyViewColumnsWidth(DbObjectPropertyType dbObjectPropertyType, DataGridViewAutoSizeColumnMode autoSizeMode)
        {
            var grid = _propertyView.GetPropertyGridView(dbObjectPropertyType);

            if (grid.InvokeRequired)
                this.Invoke(new Action(() => AdjustPropertyViewWidth(grid, autoSizeMode)));
            else
                AdjustPropertyViewWidth(grid, autoSizeMode);
        }

        void AdjustPropertyViewWidth(XDataGridView grid, DataGridViewAutoSizeColumnMode autoSizeMode)
        {
            grid.ForceAutoResizeColumnsWidth(autoSizeMode);
        }
        #endregion

        #region General property
        public void SetPropertyViewDataSource(object datasource)
        {
            if (_propertyView.GeneralPropertyView.InvokeRequired)
                this.Invoke(new Action(() => _propertyView.GeneralPropertyView.DataSource = datasource));
            else
                _propertyView.GeneralPropertyView.DataSource = datasource;
        }
        #endregion

        #region Column property
        public void SetColumnViewDataSource(object datasource)
        {
            if (_propertyView.TableColumnView.InvokeRequired)
                this.Invoke(new Action(() => _propertyView.TableColumnView.DataSource = datasource));
            else
                _propertyView.TableColumnView.DataSource = datasource;
        }
        #endregion

        #region Index property
        public void SetIndexViewDataSource(object datasource)
        {
            if (_propertyView.TableIndexView.InvokeRequired)
                this.Invoke(new Action(() => _propertyView.TableIndexView.DataSource = datasource));
            else
                _propertyView.TableIndexView.DataSource = datasource;
        }
        #endregion

        #region Constraint property
        public void SetConstraintViewDataSource(object datasource)
        {
            if (_propertyView.TableConstraintView.InvokeRequired)
                this.Invoke(new Action(() => _propertyView.TableConstraintView.DataSource = datasource));
            else
                _propertyView.TableConstraintView.DataSource = datasource;
        }
        #endregion

        #region IndexColumn property
        public void SetIndexColumnViewDataSource(object datasource)
        {
            if (_propertyView.IndexColumnView.InvokeRequired)
                this.Invoke(new Action(() => _propertyView.IndexColumnView.DataSource = datasource));
            else
                _propertyView.IndexColumnView.DataSource = datasource;
        }
        #endregion

        #region Apply preference
        public void ApplySettingToObjectView()
        {
            _objectView.ApplyPreference();
        }

        public void ApplySettingToPropertyView()
        {
            _propertyView.ApplyPreference();
        }
        #endregion

        #endregion
    }
}
