using MasudaManager.DataAccess;
using MasudaManager.Utility;
using MasudaManager.Utility.Preference;
using MasudaManager.Views;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMvp;
using WinFormsMvp.Binder;
using WinFormsMvp.Messaging;

namespace MasudaManager.Presenters
{
    public class DbObjectInfoPresenter : Presenter<IDbObjectInfoView>, IObserver
    {
        ISqlTaskManager _sqlTaskManager;
        SqlStatementBuilder _statementBuilder = new SqlStatementBuilder(false);
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        DbObjectStrategyContext _strategyContext = new DbObjectStrategyContext();
        DbObjectInfoStateContext _stateContext;
        Task _objectViewBindingTask;
        string _selectedObjectName = null;

        #region Constructor
        public DbObjectInfoPresenter(IDbObjectInfoView view)
            : base(view)
        {
            View.Model = new DbObjectInfoModel();
            _sqlTaskManager = new DbObjectSqlTaskManager(this, View.GetInvoker());
            _stateContext = new DbObjectInfoStateContext(view);

            RegisterHandlers();
            RegisterMessages();
            RegisterMethods();
        }
        #endregion

        #region EventHandlers

        #region RegisterHandlers
        void RegisterHandlers()
        {
            View.Loaded += View_Loaded;
            View.CurrentDbObjectNameRequested += View_CurrentObjectNameRequested;
            View.ObjectListSelectionChanged += View_ObjectListSelectionChanged;
            View.ObjectViewSelectionChanged += View_ObjectViewSelectionChanged;
            View.ObjectViewItemDoubleClicked += View_ObjectViewItemDoubleClicked;
            View.ObjectViewFilterTextChanged += View_ObjectViewFilterTextChanged;
            View.RefreshButtonClicked += View_RefreshButtonClicked;
            View.DisplayDataClicked += View_DisplayDataClicked;
            View.CreateSelectStmtClicked += View_CreateSelectStmtClicked;
            View.CreateSelectCountStmtClicked += View_CreateSelectCountStmtClicked;
            View.CreateInsertStmtClicked += View_CreateInsertStmtClicked;
            View.CreateDeleteStmtClicked += View_CreateDeleteStmtClicked;
            View.EditResultClicked += View_EditResultClicked;
            View.ExportClicked += View_ExportClicked;
            View.ImportClicked += View_ImportClicked;
            View.PropertyViewItemDoubleClicked += View_PropertyViewItemDoubleClicked;
            View.PropertyViewFilterTextChanged += View_PropertyViewFilterTextChanged;
        }
        #endregion

        #region Loaded
        void View_Loaded(object sender, EventArgs e)
        {
        }
        #endregion

        #region ObjectList
        void View_ObjectListSelectionChanged(object sender, EventArgs e)
        {
            ChangeObject();
        }
        #endregion

        #region ObjectView
        void View_CurrentObjectNameRequested(object sender, EventArgs e)
        {
            View.Model.SelectedDbObjectName = _strategyContext.Strategy.CurrentKeyValue.ToStringOrEmpty();
        }
        
        void View_RefreshButtonClicked(object sender, EventArgs e)
        {
            RefreshObjectView();
        }
        
        void View_ObjectViewFilterTextChanged(object sender, EventArgs e)
        {
            FilterObjectName();
        }
        
        void View_ObjectViewSelectionChanged(object sender, EventArgs e)
        {
            ChangeProperties();
        }

        void View_ObjectViewItemDoubleClicked(object sender, MouseEventArgs e)
        {
            TryInsertObjectValue(e);
        }
        #endregion

        #region ObjectView ContextMenu
        void View_DisplayDataClicked(object sender, EventArgs e)
        {
            TryRequestDisplayData();
        }

        void View_CreateSelectStmtClicked(object sender, EventArgs e)
        {
            TryRequestInsertSqlStatement(CreateSqlStatementType.Select);
        }

        void View_CreateSelectCountStmtClicked(object sender, EventArgs e)
        {
            TryRequestInsertSqlStatement(CreateSqlStatementType.SelectCount);
        }
       
        void View_CreateDeleteStmtClicked(object sender, EventArgs e)
        {
            TryRequestInsertSqlStatement(CreateSqlStatementType.Delete);
        }

        void View_CreateInsertStmtClicked(object sender, EventArgs e)
        {
            TryRequestInsertSqlStatement(CreateSqlStatementType.Insert);
        }

        void View_ExportClicked(object sender, EventArgs e)
        {
            TryRequestShowExportView();
        }

        void View_ImportClicked(object sender, EventArgs e)
        {
            TryRequestShowImportView();
        }

        void View_EditResultClicked(object sender, EventArgs e)
        {
            TryRequestEditResultView();
        }
        #endregion
        
        #region PropertyView
        void View_PropertyViewFilterTextChanged(object sender, EventArgs e)
        {
            FilterPropertyValues();
        }
        
        void View_PropertyViewItemDoubleClicked(object sender, MouseEventArgs e)
        {
            TryInsertPropertyValue(e);
        }
        #endregion

        #endregion

        #region Messages

        #region Register messages
        void RegisterMessages()
        {
            PresenterBinder.MessageBus.Register(this, PresenterTokens.ForceDisconnectToken
                , new Action<GenericMessage<object>>(OnForceDisconnect)); 
            
            PresenterBinder.MessageBus.Register(this, PresenterTokens.ConnectionOpenToken
                , new Action<GenericMessage<object>>(OnConnectionOpen));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.ConnectionCloseToken
                , new Action<GenericMessage<object>>(OnConnectionClose));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.SqlTaskStartToken
                , new Action<GenericMessage<object>>(OnSqlTaskStart));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.SqlTaskCompleteToken
                , new Action<GenericMessage<TaskCompleteEventArgs>>(OnSqlTaskComplete));
            
            PresenterBinder.MessageBus.Register(this, PresenterTokens.RequestDisposeToken
                , new Action<GenericMessage<CancelEventArgs>>(OnDisposeRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.RequestDisconnectToken
                , new Action<GenericMessage<CancelEventArgs>>(OnDisconnectRequest));           
            
            PresenterBinder.MessageBus.Register(this, PresenterTokens.CopyToObjectViewFilterToken
                , new Action<GenericMessage<string>>(OnInsertIntoObjectViewFilterRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.CopyToPropertyViewFilterToken
                , new Action<GenericMessage<string>>(OnInsertIntoPropertyViewFilterRequest));
            
            PresenterBinder.MessageBus.Register(this, PresenterTokens.RefreshObjectViewToken
                , new Action<GenericMessage<object>>(OnRefreshObjectViewRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.DbObjectInfoViewApplySettingToken
                , new Action<GenericMessage<object>>(OnApplySettingsRequest));
        }
        #endregion

        #region Receive messages
        void OnForceDisconnect(GenericMessage<object> message)
        {
            _strategyContext.Reset();
            ResetAllView();
            _stateContext.ChangeState();
        }

        void OnConnectionOpen(GenericMessage<object> message)
        {
            RefreshObjectList();
            _stateContext.ResetState();
            _stateContext.ChangeState();
        }

        void OnConnectionClose(GenericMessage<object> message)
        {
            ResetObjectList();
            _stateContext.ChangeState();
        }

        void OnDisconnectRequest(GenericMessage<CancelEventArgs> message)
        {
            if (_sqlTaskManager.IsProcessBusy())
            {
                _sqlTaskManager.Cancel();
                message.Content.Cancel = true;
            }
        }

        void OnDisposeRequest(GenericMessage<CancelEventArgs> message)
        {
            if (_sqlTaskManager.IsProcessBusy())
            {
                _sqlTaskManager.Cancel();
                message.Content.Cancel = true;
                return;
            }

            ReleasePresenter();
        }

        void OnSqlTaskStart(GenericMessage<object> message)
        {
            _stateContext.ChangeState();
        }

        void OnSqlTaskComplete(GenericMessage<TaskCompleteEventArgs> message)
        {
            _stateContext.ChangeState();
        }

        void OnRefreshObjectViewRequest(GenericMessage<object> message)
        {
            RefreshObjectView();
        }

        void OnInsertIntoObjectViewFilterRequest(GenericMessage<string> message)
        {
            View.ObjectViewFilterText = message.Content;
        }

        void OnInsertIntoPropertyViewFilterRequest(GenericMessage<string> message)
        {
            View.PropertyViewFilterText = message.Content;
        }

        void OnApplySettingsRequest(GenericMessage<object> message)
        {
            View.ApplySettingToObjectView();
            View.ApplySettingToPropertyView();
        }
        #endregion

        #region Send messages
        void RequestDisplayData(string sql)
        {            
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(sql)
                , PresenterTokens.DisplayDataToken);
        }

        void RequestInsertText(string text)
        {           
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(text)
                , PresenterTokens.InsertTextToken);
        }

        void RequestShowExportView(string tableName)
        {           
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(tableName)
                , PresenterTokens.ShowExportViewToken);
        }
        
        void RequestShowImportView(string tableName)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(tableName)
                , PresenterTokens.ShowImportViewToken);
        }

        void RequestShowEditResultView(string sql)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(sql)
                , PresenterTokens.ShowEditSqlResults);
        }
        #endregion

        #endregion

        #region Methods

        #region Register methods
        private void RegisterMethods()
        {
            View.Model.RegisterBindDataSourceMethod(DbObjectPropertyType.None, BindObjectViewDataSource
                , View.SetObjectViewDataSource);

            View.Model.RegisterBindDataSourceMethod(DbObjectPropertyType.GeneralProperty, BindPropertyViewDataSrouce
                , View.SetPropertyViewDataSource);

            View.Model.RegisterBindDataSourceMethod(DbObjectPropertyType.TableColumn, BindPropertyViewDataSrouce
                , View.SetColumnViewDataSource);

            View.Model.RegisterBindDataSourceMethod(DbObjectPropertyType.IndexColumn, BindPropertyViewDataSrouce
                , View.SetIndexColumnViewDataSource);

            View.Model.RegisterBindDataSourceMethod(DbObjectPropertyType.TableIndex, BindPropertyViewDataSrouce
                , View.SetIndexViewDataSource);

            View.Model.RegisterBindDataSourceMethod(DbObjectPropertyType.TableConstraint, BindPropertyViewDataSrouce
                , View.SetConstraintViewDataSource);
        }
        #endregion

        #region Invoke registered method
        void InvokeMethod(IDbObjectSqlTask sqlTask)
        {
            View.Model.InvokeBindDataSourceMethod(sqlTask.Strategy.PropertyType, sqlTask);
        }
        #endregion

        #region ExecuteCommand
        async void ExecuteCommand(IDbObjectStrategy strategy)
        {
            await _sqlTaskManager.RunSqlTask(strategy, strategy.CommandBuilder, true);
        }
        #endregion
        
        #region Release Presenter
        void ReleasePresenter()
        {
            View.Model.ReleaseModel();

            _strategyContext.Reset();

            _sqlTaskManager.Release(this);

            PresenterBinder.Factory.Release(this);
        }
        #endregion

        #region Refresh ObjectList
        void RefreshObjectList()
        {
            _strategyContext.Reset();
            View.SetObjectListDataSource(null);
            View.SetObjectListDataSource(View.Model.SchemaObjectList);
        }
        #endregion

        #region Reset ObjectList
        void ResetObjectList()
        {
            _strategyContext.Reset();
            if (View.IsObjectListDataBound())
                ResetAllView();
            else
                ChangeObject();
        }

        void ResetAllView()
        {
            View.SetObjectListDataSource(null);
            View.SetObjectViewDataSource(null);
            View.SetPropertyViewObjectName(null);
            View.ClearPropertyViews(_strategyContext.GetPropertyTypeFlags());
        }
        #endregion
 
        #region Change object
        void ChangeObject()
        {
            _strategyContext.ChangeStrategy(GetObjectType());

            _stateContext.ChangeState(_strategyContext.Strategy);

            View.SetPropertyViewObjectName(null);
            View.SetVisibleProperty(_strategyContext.GetPropertyTypeFlags());
            View.ClearPropertyViews(_strategyContext.GetPropertyTypeFlags());

            RefreshObjectView();
        }
        #endregion

        #region Get ObjectType
        DbObjectType GetObjectType()
        {
            if (View.ObjectListSelectedIndex < 0)
                return _strategyContext.Strategy.ObjectType;

            return DbObjectToken.ConverToDbObjectType(DbObjectToken.Tokens[View.ObjectListSelectedIndex]);
        }
        #endregion

        #region Refresh ObjectView
        void RefreshObjectView()
        {
            ExecuteCommand(_strategyContext.Strategy);
        }
        #endregion
       
        #region Bind ObjectView
        void BindObjectViewDataSource(Action<object> datasourceMethod, IDbObjectSqlTask sqlTask)
        {
            using (sqlTask)
            {
                _objectViewBindingTask = Task.Run(() =>
                    {
                        datasourceMethod.Invoke(sqlTask.BindingModel);
                        OnObjectViewBindComplete();
                    });
            }
        }
        #endregion

        #region OnObjectViewBindeComplete
        void OnObjectViewBindComplete()
        {
            View.AdjustObjectViewColumnsWidth(DataGridViewAutoSizeColumnMode.Fill);
            
            if (!String.IsNullOrEmpty(View.ObjectViewFilterText))
                FilterObjectName();
        }
        #endregion

        #region Filter object name
        void FilterObjectName()
        {
            _sqlTaskManager.FilterTaskResult(_strategyContext.Strategy, View.ObjectViewFilterText);
        }
        #endregion

        #region Change properties
        async void ChangeProperties()
        {
            await _objectViewBindingTask;

            object currentKeyValue = GetSelectedObjectKeyValue();

            if (currentKeyValue == _strategyContext.Strategy.CurrentKeyValue)
                return;
            else
                _strategyContext.Strategy.CurrentKeyValue = currentKeyValue;

            View.SetPropertyViewObjectName(_strategyContext.Strategy.CurrentKeyValue.ToStringOrNull());

            if (_strategyContext.CanChangeProperties())
                RefreshPropertyView();
        }
        #endregion

        #region Refresh PropertyView
        void RefreshPropertyView()
        {
            if (_strategyContext.Strategy.CurrentKeyValue == null)
                ClearPropertyView();
            else
                GetNextPropertyData();
        }

        void ClearPropertyView()
        {
            View.ClearPropertyViews(_strategyContext.GetPropertyTypeFlags());
            _strategyContext.ResetProperty();
        }

        void GetNextPropertyData()
        {           
            if (_strategyContext.MoveNextProperty())
                ExecuteCommand(_strategyContext.Property);
        }
        #endregion

        #region Bind PropertyView
        void BindPropertyViewDataSrouce(Action<object> datasourceMethod, IDbObjectSqlTask sqlTask)
        {
            using (sqlTask)
            {
                datasourceMethod.Invoke(sqlTask.BindingModel);
                OnPropertyViewBindComplete(sqlTask);
                RefreshPropertyView();
            }
        }
        #endregion

        #region OnPropertyViewBindComplete
        void OnPropertyViewBindComplete(IDbObjectSqlTask sqlTask)
        {
            ResizePropertyViewColumnsWidth(sqlTask.Strategy.PropertyType);

            if (!String.IsNullOrEmpty(View.PropertyViewFilterText))
                FilterPropertyValue(_strategyContext.Property);
        }

        void ResizePropertyViewColumnsWidth(DbObjectPropertyType propertyType)
        {
            if (propertyType == DbObjectPropertyType.GeneralProperty)
                View.AdjustPropertyViewColumnsWidth(propertyType, DataGridViewAutoSizeColumnMode.Fill);
            else
                View.AdjustPropertyViewColumnsWidth(propertyType, DataGridViewAutoSizeColumnMode.AllCells);
        }
        #endregion
      
        #region Filter all property values
        void FilterPropertyValues()
        {
            foreach (var strategy in _strategyContext.GetPropertyStrategies())
            {
                FilterPropertyValue(strategy);
            }
        }
        #endregion

        #region Filter property value
        void FilterPropertyValue(IDbObjectStrategy strategy)
        {
            _sqlTaskManager.FilterTaskResult(strategy, View.PropertyViewFilterText);
        }
        #endregion
        
        #region Get selected object key/name value
        object GetSelectedObjectKeyValue()
        {
            Cell cell = View.ObjectViewCurrentCell;

            if (cell.RowIndex < 0 || cell.ColumnIndex < 0)
                return null;

            cell.ColumnIndex = _strategyContext.Strategy.KeyColumnIndex;
            return View.GetObjectViewCellValue(cell);
        }

        string GetSelectedObjectName()
        {
            Cell cell = View.ObjectViewCurrentCell;

            if (cell.RowIndex < 0 || cell.ColumnIndex < 0)
                return null;

            cell.ColumnIndex = _strategyContext.Strategy.NameColumnIndex;

            return View.GetObjectViewCellValue(cell).ToStringOrNull();
        }
        #endregion

        #region CanClickInsert
        bool CanClickInsert(MouseButtons button, IDbObjectStrategy strategy, Cell cell)
        {
            if (button != MouseButtons.Left)
                return false; 
            
            if (!strategy.SupportClickInsert)
                return false;

            if (cell.RowIndex < 0 || cell.ColumnIndex < 0)
                return false;

            if (cell.ColumnIndex != strategy.InsertableColumnIndex)
                return false;

            return true;
        }
        #endregion

        #region Try Insert object/property value
        void TryInsertObjectValue(MouseEventArgs e)
        {
            if (!UserPreference.Setting.List.InsertObjectName)
                return;

            if (!CanClickInsert(e.Button, _strategyContext.Strategy, View.ObjectViewCurrentCell))
                return;

            if (View.ObjectViewCurrentValue == null)
                return;

            RequestInsertText(UserPreference.Reflector.GetEnclosedObjectName(View.ObjectViewCurrentValue.ToString()));
        }

        void TryInsertPropertyValue(MouseEventArgs e)
        {
            if (!UserPreference.Setting.List.InsertPropertyValue)
                return;

            _strategyContext.ChangeProperty(View.SelectedPropertyType);

            if (!CanClickInsert(e.Button, _strategyContext.Property, View.PropertyViewCurrentCell))
                return;

            if (View.PropertyViewCurrentValue == null)
                return;

            RequestInsertText(UserPreference.Reflector.GetEnclosedPropertyValue(View.PropertyViewCurrentValue.ToString()));
        }
        #endregion

        #region Try Export/Import
        void TryRequestShowExportView()
        {
            if (!_strategyContext.Strategy.SupportExport)
                return;

            _selectedObjectName = GetSelectedObjectName();

            if (_selectedObjectName == null)
                return;

            RequestShowExportView(_selectedObjectName);
        }

        void TryRequestShowImportView()
        {
            if (!_strategyContext.Strategy.SupportImport)
                return;

            _selectedObjectName = GetSelectedObjectName();

            if (_selectedObjectName == null)
                return;

            RequestShowImportView(_selectedObjectName);
        }
        #endregion

        #region Try Insert SQL
        void TryRequestDisplayData()
        {
            if (!_strategyContext.Strategy.SupportDisplayData)
                return;

            _selectedObjectName = GetSelectedObjectName();

            if (_selectedObjectName == null)
                return;

            RequestDisplayData(GetDisplayDataSql(_selectedObjectName));
        }

        void TryRequestEditResultView()
        {
            if (!_strategyContext.Strategy.SupportEditData)
                return;

            _selectedObjectName = GetSelectedObjectName();

            if (_selectedObjectName == null)
                return;

            RequestShowEditResultView(GetEditResultSql(_selectedObjectName));
        }

        void TryRequestInsertSqlStatement(CreateSqlStatementType statementType)
        {
            if (!_strategyContext.Strategy.SupportCreateSql)
                return;

            _selectedObjectName = GetSelectedObjectName();

            if (_selectedObjectName == null)
                return;

            switch (statementType)
            {
                case CreateSqlStatementType.Select:
                    RequestInsertText(GetSelectSql(_selectedObjectName));
                    break;
                case CreateSqlStatementType.SelectCount:
                    RequestInsertText(GetSelectCountSql(_selectedObjectName));
                    break;
                case CreateSqlStatementType.Insert:
                    RequestInsertText(GetInsertSql(_selectedObjectName));
                    break;
                case CreateSqlStatementType.Delete:
                    RequestInsertText(GetDeleteSql(_selectedObjectName));
                    break;
            }        
        }
        #endregion

        #region Get SQL statement
        string GetDisplayDataSql(string tableName)
        {
            return _dataAccess.SqlLibrary.FormatSelectAllFromTable(tableName);
        }
        
        string GetEditResultSql(string tableName)
        {
            return _dataAccess.SqlLibrary.FormatSelectAllFromTable(tableName);
        }

        string GetSelectSql(string tableName)
        {
            if (!_strategyContext.GetPropertyTypeFlags().HasFlag(DbObjectPropertyType.TableColumn))
                return null;

            _strategyContext.ChangeProperty(DbObjectPropertyType.TableColumn);

            using (var sqlTask = _sqlTaskManager.Factory.GetSqlTask(_strategyContext.Property))
            {
                return _statementBuilder.CreateSelectStatement(
                    tableName,
                    sqlTask.BindingModel.ToSavedSourceList().Select(list => list.RowValues[(int)TableColumnDataColumn.ColumnName])
                    );
            }
        }

        string GetSelectCountSql(string tableName)
        {
            return _dataAccess.SqlLibrary.FormatSelectCountAllFromTable(tableName);
        }

        string GetInsertSql(string tableName)
        {
            return _statementBuilder.CreateInsertStatement(tableName);
        }

        string GetDeleteSql(string tableName)
        {
            return _statementBuilder.CreateDeleteStatement(tableName);
        }

        #endregion

        #region Observer
        public void Update(object sender)
        {
            InvokeMethod(sender as IDbObjectSqlTask);
        }

        public void Complete(object sender)
        {
            InvokeMethod(sender as IDbObjectSqlTask);
        }

        public void Error(object sender, Exception e)
        {
            InvokeMethod(sender as IDbObjectSqlTask);
        }
        #endregion

        #endregion
    }
}
