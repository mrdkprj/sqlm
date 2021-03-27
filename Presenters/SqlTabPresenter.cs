using MasudaManager.Utility;
using MasudaManager.Utility.Preference;
using MasudaManager.Views;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using WinFormsMvp;
using WinFormsMvp.Binder;
using WinFormsMvp.Messaging;

namespace MasudaManager.Presenters
{
    public class SqlTabPresenter : Presenter<ISqlTabView>, IObserver
    {
        SqlTabManager _tabManager = new SqlTabManager();
        ISqlTaskManager _sqlTaskManager;
        SqlTabStateContext _stateContext;
        SqlTabObserverContext _observerContext = new SqlTabObserverContext();

        readonly string _unavedTabText = "{0}*";

        #region Constructor
        public SqlTabPresenter(ISqlTabView view)
            : base(view)
        {
            View.Model = new SqlTabModel();

            _sqlTaskManager = new SqlTaskManager(this, View.GetInvoker());
            _sqlTaskManager.TaskStartAction = OnSqlTaskStart;
            _sqlTaskManager.TaskCompleteAction = OnSqlTaskComplete;
            _sqlTaskManager.DefaultErrorAction = SqlTaskOnErrorActionType.ComplyWithPreference;

            _stateContext = new SqlTabStateContext(view);

            _observerContext.View = view;
            _observerContext.RegisterCallBack(NotifySqlTaskStatusChange);

            RegisterHandlers();
            RegisterMessages();
            PrepareSearchViewRequestData();
        }
        #endregion

        #region EventHandlers

        #region RegisterHandlers
        void RegisterHandlers()
        {
            View.Loaded += View_Loaded;

            View.NewTabRequested += View_NewTabRequested;
            View.TabSelectionChanged += View_TabSelectionChanged;
            View.TabSelecting += View_TabSelecting;
            View.TabCloseButtonClicked += View_TabCloseButtonClicked;

            View.InputViewSaveStatusChanged += View_InputViewSaveStatusChanged;
            View.InputViewFileDropped += View_InputViewFileDropped;
            View.InputViewEnterKeyDown += View_InputViewEnterKeyDown;
            View.InputViewPeriodKeyUp += View_InputViewPeriodKeyUp;
            View.InputViewFilterObjectViewClicked += View_InputViewFilterObjectViewClicked;
            View.InputViewFilterPropertyViewClicked += View_InputViewFilterPropertyViewClicked;
            View.InputViewCopyFromObjectViewClicked += View_InputViewCopyFromObjectViewClicked;
            View.InputViewSaveTextRequested += View_InputViewSaveTextRequested;

            View.ResultViewAutoResizeHeadersClicked += View_ResultViewAutoResizeHeadersClicked;
            View.ResultViewCopyTextClicked += View_ResultViewCopyTextClicked;
            View.ResultViewCopyHeaderClicked += View_ResultViewCopyHeaderClicked;
            View.ResultViewCopyTextWithHeaderClicked += View_ResultViewCopyTextWithHeaderClicked;
            View.ResultViewEditResultClicked += View_ResultViewEditResultClicked;
            View.ResultViewClearResultClicked += View_ResultViewClearClicked;
            View.ResultViewSearchBackwardRequested += View_ResultViewSearchBackwardRequested;
            View.ResultViewSearchForwardRequested += View_ResultViewSearchForwardRequested;

        }
        #endregion

        #region Loaded
        void View_Loaded(object sender, EventArgs e)
        {
            AddNewTab();
            View.SetInputViewFocus(_tabManager.CurrentGuid);
        }
        #endregion

        #region Tab
        void View_NewTabRequested(object sender, EventArgs e)
        {
            AddNewTab();
        }

        void View_TabSelecting(object sender, CancelEventArgs e)
        {
            if (_sqlTaskManager.IsProcessBusy())
                _sqlTaskManager.Cancel();
        }

        void View_TabSelectionChanged(object sender, EventArgs e)
        {
            _tabManager.Activate(View.CurrentTabPageGuid);
            View.SetInputViewFocus(_tabManager.CurrentGuid);
            NotifySelectedTabChange(_sqlTaskManager.Factory.GetSqlTask(_tabManager.CurrentGuid));
        }

        void View_TabCloseButtonClicked(object sender, EventArgs e)
        {
            TryCloseTab(sender, true);
        }
        #endregion

        #region SearchGridView
        void View_ResultViewSearchBackwardRequested(object sender, EventArgs e)
        {
            RequestSearch(SearchDirection.Backward);
        }

        void View_ResultViewSearchForwardRequested(object sender, EventArgs e)
        {
            RequestSearch(SearchDirection.Forward);
        }
        #endregion

        #region InputView
        void View_InputViewSaveStatusChanged(object sender, EventArgs e)
        {
            if (View.IsInputViewTextSaved(sender))
                View.SetTabPageText(sender, _tabManager.GetFileName(sender));
            else
                View.SetTabPageText(sender, String.Format(_unavedTabText, _tabManager.GetFileName(sender)));
        }
        
        void View_InputViewFileDropped(object sender, DragEventArgs e)
        {
            TryOpenTextFile(e);
        }

        void View_InputViewPeriodKeyUp(object sender, EventArgs e)
        {
            ShowInputAssistant();
        }

        void View_InputViewEnterKeyDown(object sender, KeyEventArgs e)
        {
            ExecuteSqlByEnterKey(e);
        }

        void View_InputViewSaveTextRequested(object sender, EventArgs e)
        {
            RequestShowSaveFileDialog();
        }
        #endregion

        #region ContextMenu
        void View_InputViewCopyFromObjectViewClicked(object sender, EventArgs e)
        {
            RequestCopyFromObjectView();
        }

        void View_InputViewFilterObjectViewClicked(object sender, EventArgs e)
        {
            RequestCopyToObjectViewFilter(View.GetInputViewSelectedText(_tabManager.CurrentGuid));
        }

        void View_InputViewFilterPropertyViewClicked(object sender, EventArgs e)
        {
            RequestCopyToPropertyViewFilter(View.GetInputViewSelectedText(_tabManager.CurrentGuid));
        }

        void View_ResultViewAutoResizeHeadersClicked(object sender, EventArgs e)
        {
            View.AdjustResultViewColumnsWidth(_tabManager.CurrentGuid, DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        void View_ResultViewCopyTextClicked(object sender, EventArgs e)
        {
            CopyGridData(CopyTargetType.Text);
        }

        void View_ResultViewCopyTextWithHeaderClicked(object sender, EventArgs e)
        {
            CopyGridData(CopyTargetType.TextWithHeader);
        }

        void View_ResultViewCopyHeaderClicked(object sender, EventArgs e)
        {
            CopyGridData(CopyTargetType.Header);
        }

        void View_ResultViewEditResultClicked(object sender, EventArgs e)
        {
            using (var sqlTask = _sqlTaskManager.Factory.GetSqlTask(_tabManager.CurrentGuid))
            {
                RequestShowEditResultView(sqlTask.ExcutedSqlInfo.ExecutedSql);
            }
        }

        void View_ResultViewClearClicked(object sender, EventArgs e)
        {
            TryClearResult();
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
            
            PresenterBinder.MessageBus.Register(this, PresenterTokens.RequestDisposeToken
                , new Action<GenericMessage<CancelEventArgs>>(OnDisposeRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.RequestDisconnectToken
                , new Action<GenericMessage<CancelEventArgs>>(OnDisconnectRequest));
            
            PresenterBinder.MessageBus.Register(this, PresenterTokens.AddSqlTabToken
                , new Action<GenericMessage<object>>(OnAddNewTabRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.OpenFileToken
                , new Action<GenericMessage<string>>(OnOpenFileRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.SaveFileToken
                , new Action<GenericMessage<string>>(OnSaveFileRequest));
            
            PresenterBinder.MessageBus.Register(this, PresenterTokens.ExecuteSqlToken
                , new Action<GenericMessage<object>>(OnExecuteSqlRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.CancelSqlToken
                , new Action<GenericMessage<object>>(OnCancelSqlRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.DisplayDataToken
                , new Action<GenericMessage<string>>(OnDisplayDataRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.ShowSearchViewToken
                , new Action<GenericMessage<object>>(OnShowSearchViewRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.InsertTextToken
                , new Action<GenericMessage<string>>(OnInsertTextRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.SqlTabViewApplySettingToken
                , new Action<GenericMessage<object>>(OnApplySettingToSqlTabViewRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.InputViewApplySettingToken
                , new Action<GenericMessage<object>>(OnApplySettingToInputViewRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.ResultViewApplySettingToken
                , new Action<GenericMessage<object>>(OnApplySettingToResultViewRequest));
        }
        #endregion

        #region Receive messages
        void OnForceDisconnect(GenericMessage<object> message)
        {
            _sqlTaskManager.Cancel();
        }

        void OnConnectionOpen(GenericMessage<object> message)
        {
            _stateContext.ResetState();
        }

        void OnConnectionClose(GenericMessage<object> message)
        {
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
        
        void OnExecuteSqlRequest(GenericMessage<object> message)
        {
            ExecuteSql(_tabManager.CurrentGuid, View.GetInputViewText(_tabManager.CurrentGuid));
        }

        void OnCancelSqlRequest(GenericMessage<object> message)
        {
            _sqlTaskManager.Cancel();
        }

        void OnAddNewTabRequest(GenericMessage<object> message)
        {
            AddNewTab();
        }

        void OnOpenFileRequest(GenericMessage<string> message)
        {
            TryOpenTextFile(message.Content);
        }

        void OnSaveFileRequest(GenericMessage<string> message)
        {
            TrySaveSqlText(message.Content);
        }

        void OnShowSearchViewRequest(GenericMessage<object> message)
        {
            ShowSearchView();
        }

        void OnDisplayDataRequest(GenericMessage<string> message)
        {
            ExecuteSql(_tabManager.CurrentGuid, message.Content);
        }

        void OnInsertTextRequest(GenericMessage<string> message)
        {
            View.SetInputViewSelectedText(_tabManager.CurrentGuid, message.Content);
            View.SetInputViewFocus(_tabManager.CurrentGuid);
        }

        void OnApplySettingToSqlTabViewRequest(GenericMessage<object> message)
        {
            View.ApplyTabSetting();
        }
        
        void OnApplySettingToInputViewRequest(GenericMessage<object> message)
        {
            View.ApplyInputViewSetting();
        }

        void OnApplySettingToResultViewRequest(GenericMessage<object> message)
        {
            View.ApplyResultViewSetting();
        }
        #endregion

        #region Send messages
        void RequestCopyToObjectViewFilter(string text)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(text)
                , PresenterTokens.CopyToObjectViewFilterToken);
        }

        void RequestCopyToPropertyViewFilter(string text)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(text)
                , PresenterTokens.CopyToPropertyViewFilterToken);
        }

        void RequestCopyFromObjectView()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.CopyFromObjectViewToken);
        }

        void RequestShowEditResultView(string sql)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(sql)
                , PresenterTokens.ShowEditSqlResults);
        }

        void RequestShowSaveFileDialog()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.ShowSaveFileDialogToken);
        }

        void RequestSearch(SearchDirection direction)
        {
            View.Model.SearchViewRequestData.SearchDirection = direction;

            PresenterBinder.MessageBus.Send(new GenericMessage<SearchViewRequestData>(View.Model.SearchViewRequestData)
                , PresenterTokens.ExecuteSearchToken);
        }

        void SendSearchContext(SearchContext searchContext)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<SearchContext>(searchContext)
                , PresenterTokens.SearchContextPreparedToken);
        }

        void NotifySqlTaskStart()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.SqlTaskStartToken);
        }
        
        void NotifySqlTaskComplete(TaskCompleteEventArgs e)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<TaskCompleteEventArgs>(e)
                , PresenterTokens.SqlTaskCompleteToken);
        }

        void NotifySqlTaskStatusChange(ISqlTask sqlTask)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<ISqlTask>(sqlTask)
               , PresenterTokens.SqlTaskStatusChangedToken);
        }

        void NotifySelectedTabChange(ISqlTask sqlTask)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<ISqlTask>(sqlTask)
              , PresenterTokens.SelectedTabChangedToken);
        }
        #endregion

        #endregion

        #region Methods

        #region Release presenter
        void ReleasePresenter()
        {
            _sqlTaskManager.Release(this);

            _tabManager.ReleaseStream();

            SearchDialogPresenter.DisposeDialog();

            PresenterBinder.Factory.Release(this);
        }
        #endregion

        #region SearchView
        void PrepareSearchViewRequestData()
        {
            View.Model.SearchViewRequestData.Owner = View.GetWin32Window();
            View.Model.SearchViewRequestData.CallBackActions = new SearchGridCallBackActions(OnSearchContextRequest, OnSearchComplete);
        }
        
        void ShowSearchView()
        {
            SearchDialogPresenter.Dialog.Initiate(View.Model.SearchViewRequestData.Owner, View.Model.SearchViewRequestData);
        }
        
        void OnSearchContextRequest()
        {
            View.Model.SearchContext = new SearchContext();
            using (var sqlTask = _sqlTaskManager.Factory.GetSqlTask(_tabManager.CurrentGuid))
            {
                View.Model.SearchContext.SqlResults = sqlTask.BindingModel.ToSourceList();
            }
            View.Model.SearchContext.CurrentCell = View.GetResultViewCurrentCell(_tabManager.CurrentGuid);

            SendSearchContext(View.Model.SearchContext);
        }

        void OnSearchComplete(Cell cell)
        {
            View.ClearResultViewSelection(_tabManager.CurrentGuid);
            View.SetResultViewCurrentCell(_tabManager.CurrentGuid, cell);
        }
        #endregion

        #region Copy GridData
        void CopyGridData(CopyTargetType targetType)
        {
            using (var sqlTask = _sqlTaskManager.Factory.GetSqlTask(_tabManager.CurrentGuid))
            {
                SqlResultClipboardSetter.Copy(targetType, GetSelectedCellRange(), sqlTask.BindingModel.ToSourceList());
            }
        }
        
        CellRange GetSelectedCellRange()
        {
            CellRange range = new CellRange();
            range.StartCell = View.GetResultViewFirstSelectedCell(_tabManager.CurrentGuid);
            range.EndCell = View.GetResultViewLastSelectedCell(_tabManager.CurrentGuid);
            range.RowCount = View.GetResultViewSelectedRowCount(_tabManager.CurrentGuid);
            range.ColumnCount = View.GetResultViewSelectedColumnCount(_tabManager.CurrentGuid);
            return range;
        }
        #endregion

        #region AddNewTab
        Guid AddNewTab()
        {
            Guid guid = View.CreateNewTabPage();
            _tabManager.Add(guid);
            View.SetInputViewFont(guid, UserPreference.Setting.Input.Font);
            View.SetResultViewFont(guid, UserPreference.Setting.Output.Font);
            return guid;
        }
        #endregion

        #region TryCloseTab
        void TryCloseTab(object guid, bool confirmBeforeClose)
        {
            if (_stateContext.CurrentState.HasBusyProcess)
                return;

            if (confirmBeforeClose && ConfirmCloseTab() != DialogResult.OK)
                return;

            if (!_tabManager.Remove(guid))
                return;

            View.SelectTabPage(_tabManager.CurrentGuid);
            _tabManager.ReleaseStream(guid);
            _sqlTaskManager.Factory.DisposeTask(guid);
            View.RemoveTabPage(guid);
        }

        DialogResult ConfirmCloseTab()
        {
            return View.ShowMessageBox
                (
                    LocalizedTextProvider.Message.Info.CloseTab,
                    "Close",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1
                );
        }
        #endregion

        #region Clear result
        void TryClearResult()
        {
            if (ConfirmClearResult() != DialogResult.Yes)
                return; 
            
            using (var sqlTask = _sqlTaskManager.Factory.GetSqlTask(_tabManager.CurrentGuid))
            {
                sqlTask.Clear();
            }

            View.ResetResultViewDataSource(_tabManager.CurrentGuid);
            View.ClearResult(_tabManager.CurrentGuid);
        }

        DialogResult ConfirmClearResult()
        {
            return View.ShowMessageBox
                (
                    LocalizedTextProvider.Message.Info.ClearLogView,
                    "Clear",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1
                );            
        }
        #endregion

        #region ShowInputAssistant
        void ShowInputAssistant()
        {
            if (!UserPreference.Setting.Input.UseAssistant)
                return;

            if (!_stateContext.CurrentState.CanExecuteSql)
                return;

            View.ShowSqlInputAssistant(_tabManager.CurrentGuid);
        }
        #endregion

        #region ExecuteSqlByEnterKey
        void ExecuteSqlByEnterKey(KeyEventArgs e)
        {
            if (!UserPreference.Setting.Sql.RunAfterSemicolon)
                return;

            if (!_stateContext.CurrentState.CanExecuteSql)
                return;

            int index = View.GetInputViewLastIndexOf(_tabManager.CurrentGuid, Constants.CharSemicolon);

            if (index <= 0)
                return;

            if (View.IsCommented(_tabManager.CurrentGuid, index))
                return;

            e.Handled = true;
            ExecuteSql(_tabManager.CurrentGuid, View.GetInputViewText(_tabManager.CurrentGuid));
        }
        #endregion

        #region Try OpenTextFile
        void TryOpenTextFile(DragEventArgs e)
        {
            TryOpenTextFile(GetPathFromDataObject(e.Data));
        }
        
        string GetPathFromDataObject(IDataObject dataObject)
        {
            string[] fileList = (string[])dataObject.GetData(DataFormats.FileDrop, false);

            if (fileList.Length <= 0)
                return null;

            return fileList[0];
        }

        void TryOpenTextFile(string path)
        {
            if (path == null)
                return;

            Guid guid = Guid.Empty;

            try
            {
                guid = AddNewTab();

                string text = _tabManager.OpenFile(guid, path);            
                View.SetTabPageText(guid, _tabManager.GetFileName(guid));
                View.SetInputViewFilePath(guid, path);
                View.LoadInputViewText(guid, text);
            }
            catch (Exception ex)
            {
                View.ShowMessageBox(ex.Message);
                TryCloseTab(guid, false);
            }
        }
        #endregion

        #region Try SaveSqlText
        void TrySaveSqlText(string path)
        {
            try
            {
                object guid = _tabManager.CurrentGuid;
                _tabManager.SaveFile(guid, path, View.GetInputViewText(guid));
                View.SetTabPageText(guid, _tabManager.GetFileName(guid));
                View.SetInputViewFilePath(guid, path);
                View.MarkInputViewAsSaved(guid);
            }
            catch (Exception ex)
            {
                View.ShowMessageBox(ex.Message);
            }
        }
        #endregion
        
        #region Execute SQL
        async void ExecuteSql(object guid, string sql)
        {
            await _sqlTaskManager.RunSqlTask(guid, sql, true);
        }
        #endregion

        #region OnSqlTaskStart
        void OnSqlTaskStart(ISqlTask sqlTask)
        {
            View.ResetResultViewDataSource(_tabManager.CurrentGuid);
            NotifySqlTaskStart();
            _stateContext.ChangeState();
        }
        #endregion

        #region OnSqlTaskComplete
        void OnSqlTaskComplete(ISqlTask sqlTask, TaskCompleteEventArgs e)
        {
            NotifySqlTaskComplete(e);
            _stateContext.ChangeState();
        }
        #endregion

        #region Observer
        public void Update(object sender)
        {
            if (sender is ISqlTask)
                ActOnSqlTask((ISqlTask)sender, ObserverActionType.OnUpdate);
        }

        public void Complete(object sender)
        {
            if (sender is ISqlTask)
                ActOnSqlTask((ISqlTask)sender, ObserverActionType.OnComplete);
        }

        public void Error(object sender, Exception e)
        {
            if (sender is ISqlTask)
                ActOnSqlTask((ISqlTask)sender, ObserverActionType.OnError);
        }
        #endregion

        #region ActOnSqlTask
        void ActOnSqlTask(ISqlTask sqlTask, ObserverActionType actionType)
        {
            _observerContext.ChangeStrategy(sqlTask);
            _observerContext.Act(actionType);
        }

        #endregion

        #endregion
    }
}
