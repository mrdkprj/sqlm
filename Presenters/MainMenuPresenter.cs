using MasudaManager.DataAccess;
using MasudaManager.Utility;
using MasudaManager.Utility.Preference;
using MasudaManager.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using WinFormsMvp;
using WinFormsMvp.Binder;
using WinFormsMvp.Messaging;

namespace MasudaManager.Presenters
{
    public class MainMenuPresenter : Presenter<IMainMenuView>
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        IChildView _childView;
        MainMenuStateContext _stateContext;
        LogOnView _logOnView = new LogOnView();
        ExportView _exportView = new ExportView();
        ImportView _importView = new ImportView();
        PreferenceView _preferenceView = new PreferenceView();

        readonly Color _statusLabelNomarlColor = Color.SteelBlue;
        readonly Color _statusLabelErrorColor = Color.Red;
        readonly string _fileDialogFilterText = "Text files (*.txt)|*.txt|SQL files (*.sql)|*.sql";
        readonly int _fileDialogFilterIndex = 2;

        #region Constructor
        public MainMenuPresenter(IMainMenuView view)
            : base(view)
        {
            View.Model = new MainMenuModel();

            RegisterEvents();
            RegisterMessages();
        }
        #endregion

        #region EventHandlers

        #region Register events
        void RegisterEvents()
        { 
            View.ComponentInitialized += View_ComponentInitialized;
            View.ViewClosing += View_ViewClosing;
            View.ConnectButtonClicked += View_ConnectButtonClicked;
            View.DisconnectButtonClicked += View_DisconnectButtonClicked;
            View.AddTabButtonClicked += View_AddTabButtonClicked;
            View.OpenButtonClicked += View_OpenButtonClicked;
            View.SaveButtonClicked += View_SaveButtonClicked;
            View.ExportButtonClicked += View_ExportButtonClicked;
            View.ExecuteButtonClicked += View_ExecuteButtonClicked;
            View.CancelButtonClicked += View_CancelButtonClicked;
            View.SearchButtonClicked += View_SearchButtonClicked;
            View.EditResultButtonClicked += View_EditResultButtonClicked;
            View.PreferenceButtonClicked += View_PreferenceButtonClicked;
        }
        #endregion

        #region ComponentInitialized
        void View_ComponentInitialized(object sender, EventArgs e)
        {
            PrepareShowView();
        }
        #endregion

        #region ViewClosing
        private void View_ViewClosing(object sender, CancelEventArgs e)
        {
            TryClose(e);
        }
        #endregion

        #region ToolStrip button click
        void View_ConnectButtonClicked(object sender, EventArgs e)
        {
            ShowLogOnView();
        }

        void View_DisconnectButtonClicked(object sender, EventArgs e)
        {
            TryDisconnect(new CancelEventArgs());
        }

        void View_AddTabButtonClicked(object sender, EventArgs e)
        {
            RequestAddNewTab();
        }

        void View_OpenButtonClicked(object sender, EventArgs e)
        {
            TryOpenFile();
        }

        void View_SaveButtonClicked(object sender, EventArgs e)
        {
            TrySaveFile();
        }

        void View_ExecuteButtonClicked(object sender, EventArgs e)
        {
            RequestExecuteSql();
        }

        void View_CancelButtonClicked(object sender, EventArgs e)
        {
            RequestCancelSql();
        }

        void View_ExportButtonClicked(object sender, EventArgs e)
        {
            ShowExportView(View.GetActiveInputViewText(), null);
        }

        void View_SearchButtonClicked(object sender, EventArgs e)
        {
            RequestShowSearchView();
        }

        void View_EditResultButtonClicked(object sender, EventArgs e)
        {
            ShowEditSqlResults(View.GetActiveInputViewText());
        }

        void View_PreferenceButtonClicked(object sender, EventArgs e)
        {
            ShowPreferenceView();
        }
        #endregion

        #endregion

        #region Messages

        #region Register messages
        void RegisterMessages()
        {
            PresenterBinder.MessageBus.Register(this, PresenterTokens.ConnectionChangedToken
               , new Action<GenericMessage<object>>(OnConnectionChanged));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.SqlTaskStatusChangedToken
                , new Action<GenericMessage<ISqlTask>>(OnSqlTaskStatusChanged));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.SelectedTabChangedToken
                , new Action<GenericMessage<ISqlTask>>(OnSelectedTabChanged)); 
            
            PresenterBinder.MessageBus.Register(this, PresenterTokens.CopyFromObjectViewToken
               , new Action<GenericMessage<object>>(OnCopyFromObjectView));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.SqlTaskStartToken
                , new Action<GenericMessage<object>>(OnSqlTaskStart));
            
            PresenterBinder.MessageBus.Register(this, PresenterTokens.SqlTaskCompleteToken
                , new Action<GenericMessage<TaskCompleteEventArgs>>(OnSqlTaskComplete)); 

            PresenterBinder.MessageBus.Register(this, PresenterTokens.ShowExportViewToken
                , new Action<GenericMessage<string>>(OnShowExportView));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.ShowImportViewToken
                , new Action<GenericMessage<string>>(OnShowImportView));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.ShowEditSqlResults
                , new Action<GenericMessage<string>>(OnShowEditSqlResults));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.ShowSaveFileDialogToken
                , new Action<GenericMessage<object>>(OnSaveFilePathRequest));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.InputSettingToken
               , new Action<GenericMessage<object>>(OnInputSettingChanged));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.OutputSettingToken
               , new Action<GenericMessage<object>>(OnOutputSettingChanged));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.ListSettingToken
                , new Action<GenericMessage<object>>(OnListSettingChanged));

            PresenterBinder.MessageBus.Register(this, PresenterTokens.TabSettingToken
                , new Action<GenericMessage<object>>(OnTabSettingChanged));
        }
        #endregion

        #region Receive messages
        void OnConnectionChanged(GenericMessage<object> message)
        {
            _stateContext.ResetState();
            NotifyConnectionOpened();
        }

        void OnCopyFromObjectView(GenericMessage<object> message)
        {
            RequestInsertText(View.GetDbObjectInfoViewSelectedObjectName());
        }

        void OnSqlTaskStart(GenericMessage<object> message)
        {
            _stateContext.ChangeState();
        }

        void OnSqlTaskComplete(GenericMessage<TaskCompleteEventArgs> message)
        {
            _stateContext.ChangeState();

           if (message.Content.Result == SqlTaskResultType.DefinitionAltered)
                RequestRefreshObjectView();
        }

        void OnSqlTaskStatusChanged(GenericMessage<ISqlTask> message)
        {
            UpdateStatusLable(message.Content.ExcutedSqlInfo);
        }

        void OnSelectedTabChanged(GenericMessage<ISqlTask> message)
        {
            UpdateStatusLable(message.Content.ExcutedSqlInfo);
        }

        void OnShowExportView(GenericMessage<string> message)
        {
            ShowExportView(_dataAccess.SqlLibrary.FormatSelectAllFromTable(message.Content), message.Content);
        }

        void OnShowImportView(GenericMessage<string> message)
        {
            ShowImportView(message.Content);
        }
        
        void OnShowEditSqlResults(GenericMessage<string> message)
        {
            ShowEditSqlResults(message.Content);
        }

        void OnSaveFilePathRequest(GenericMessage<object> message)
        {
            TrySaveFile();
        }

        void OnInputSettingChanged(GenericMessage<object> message)
        {
            RequestApplySettingToInputView();
        }

        void OnOutputSettingChanged(GenericMessage<object> message)
        {
            RequestApplySettingToResultView();
        }

        void OnListSettingChanged(GenericMessage<object> message)
        {
            RequestApplySettingToObjectView();
        }

        void OnTabSettingChanged(GenericMessage<object> message)
        {
            RequestApplySettingToSqlTab();
        }
        #endregion
        
        #region Send messaged
        void NotifyForceDisconnect()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.ForceDisconnectToken);
        }

        void NotifyConnectionOpened()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.ConnectionOpenToken);
        }

        void NotifyConnectionClosed()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
               , PresenterTokens.ConnectionCloseToken);
        }

        void RequestDisposeDbObjectInfoViewSqlTask()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.DisposeDbObjectSqlTaskToken);
        }

        void RequestRefreshObjectView()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.RefreshObjectViewToken);
        }

        void RequestDispose(CancelEventArgs e)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<CancelEventArgs>(e)
                , PresenterTokens.RequestDisposeToken);
        }

        void RequestDisconnect(CancelEventArgs e)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<CancelEventArgs>(e)
                , PresenterTokens.RequestDisconnectToken);
        }
        
        void RequestInsertText(string text)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(text)
               , PresenterTokens.InsertTextToken);
        }

        void RequestExecuteSql()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.ExecuteSqlToken);
        }

        void RequestCancelSql()
        {
            SetStatusLabelText(LocalizedTextProvider.Message.Info.Cancelling, true);

            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.CancelSqlToken);
        }

        void RequestShowSearchView()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.ShowSearchViewToken);
        }

        void RequestAddNewTab()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                , PresenterTokens.AddSqlTabToken);
        }

        void RequestOpenFile(string path)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(path)
                    , PresenterTokens.OpenFileToken);
        }

        void RequestSaveFile(string path)
        {
            PresenterBinder.MessageBus.Send(new GenericMessage<string>(path)
                    , PresenterTokens.SaveFileToken);
        }

        void RequestApplySettingToSqlTab()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                    , PresenterTokens.SqlTabViewApplySettingToken);
        }

        void RequestApplySettingToInputView()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                    , PresenterTokens.InputViewApplySettingToken);
        }
        
        void RequestApplySettingToResultView()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                    , PresenterTokens.ResultViewApplySettingToken);
        }

        void RequestApplySettingToObjectView()
        {
            PresenterBinder.MessageBus.Send(Constants.NullGenericMessage
                    , PresenterTokens.DbObjectInfoViewApplySettingToken);
        }

        #endregion

        #endregion

        #region Methods

        #region PrepareShowView
        void PrepareShowView()
        {
            _stateContext = new MainMenuStateContext(View);
            _stateContext.ChangeState();

            NotifyConnectionStatus();
            UpdateInformationLabelText();
            InitializeViewDialog();
        }
        #endregion

        #region NotifyConnectionStatus
        void NotifyConnectionStatus()
        {
            if (_dataAccess.CurrentConnectionData.IsConnected)
                NotifyConnectionOpened();
            else
                NotifyConnectionClosed();
        }
        #endregion

        #region UpdateInformationLabel
        void UpdateInformationLabelText()
        {
            View.SetInfomationLabelText(UserPreference.Setting.Sql.AllowAutoCommit ? LocalizedTextProvider.Form.AutoCommit : null);
        }
        #endregion

        #region InitializeViewDialog
        void InitializeViewDialog()
        {
            View.InitializeOpenFileDialog(_fileDialogFilterText, _fileDialogFilterIndex);
            View.InitializeSaveFileDialog(_fileDialogFilterText, _fileDialogFilterIndex);
        }
        #endregion
        
        #region Try close
        void TryClose(CancelEventArgs e)
        {
            TryReleaseChildView(e);

            if (e.Cancel)
                return;
            
            RequestDispose(e);

            if (e.Cancel)
                return;
 
            _stateContext.SaveState();

            _dataAccess.Disconnect();
        }
        #endregion

        #region Try ReleaseChildView
        void TryReleaseChildView(CancelEventArgs e)
        {
            if (_childView == null)
                return;

            _childView.Release(this, e);
        }
        #endregion

        #region Try Disconnect
        void TryDisconnect(CancelEventArgs e)
        {
            if (_childView != null)
                return;

            RequestDisconnect(e);

            if (e.Cancel)
                return;

            _dataAccess.Disconnect();

            NotifyConnectionClosed();

            _stateContext.ChangeState();
        }
        #endregion      
        
        #region Initiate ChildView
        void InitiateChildView<T>(IChildView<T> childView, GenericEventArgs<T> args)
        {
            if (_childView != null)
                return;

            _childView = childView;
            _childView.SuppressFormClosingEvent = false;
            _childView.ViewClosedAction = OnChildViewClosed;
            ((IChildView<T>)_childView).Initiate(View.GetChildViewOwner(), args);
        }     
        #endregion

        #region OnChildViewClosed
        void OnChildViewClosed()
        {
            _childView = null;

            UpdateInformationLabelText();
        }
        #endregion

        #region Show LogOnView
        void ShowLogOnView()
        {
            InitiateChildView(_logOnView, new GenericEventArgs<object>());
        }
        #endregion

        #region Show ExportView
        void ShowExportView(string sql, string tableName)
        {
            ExportImportParameter parameter = new ExportImportParameter(sql, tableName);
            InitiateChildView(_exportView, new GenericEventArgs<ExportImportParameter>(parameter));
        }
        #endregion

        #region Show ImportView
        void ShowImportView(string tableName)
        {
            ExportImportParameter parameter = new ExportImportParameter(null, tableName);
            InitiateChildView(_importView, new GenericEventArgs<ExportImportParameter>(parameter));
        }
        #endregion

        #region Show EditSqlResults
        void ShowEditSqlResults(string sql)
        {
            //IProgressView view = new ProgressView();
            //view.CloseOnCancel = true;
            //view.Style = System.Windows.Forms.ProgressBarStyle.Blocks;
            //view.MixProgressValue = 0;
            //view.MaxProgressValue = 100;
            //view.ProgressBarValue = 0;
            //view.Start();
            //view.ShowView();
            InitiateChildView(new EditResultView(), new GenericEventArgs<string>(sql));
        }
        #endregion

        #region Show PreferenceView
        void ShowPreferenceView()
        {
            InitiateChildView(_preferenceView, new GenericEventArgs<object>());
        }
        #endregion

        #region Try open/save file
        void TryOpenFile()
        {
            string path = View.GetOpenFilePath();

            if (String.IsNullOrEmpty(path))
                return;

            try
            {
                RequestOpenFile(path);
            }
            catch (Exception ex)
            {
                View.ShowMessageBox(ex.Message);
            }
        }

        void TrySaveFile()
        {
            string path = View.GetActiveInputViewFilePath();

            if (String.IsNullOrEmpty(path))
                path = View.GetSaveFilePath();

            if (String.IsNullOrEmpty(path))
                return;

            try
            {
                RequestSaveFile(path);
            }
            catch (Exception ex)
            {
                View.ShowMessageBox(ex.Message);
            }
        }
        #endregion

        #region Update status lable
        void UpdateStatusLable(SqlBaseInfo sqlInfo)
        {
            switch (sqlInfo.SqlType)
            {
                case SqlType.Query:
                    SetStatusLabelText(GetQuerySqlStatusText(sqlInfo), HasQuerySqlError(sqlInfo));
                    break;
                case SqlType.DDL:
                case SqlType.DML:
                case SqlType.TCL:
                    SetStatusLabelText(GetNonQuerySqlStatusText(sqlInfo), sqlInfo.HasError);
                    break;
                case SqlType.Invalid:
                    SetStatusLabelText(GetInvalidSqlStatusText(sqlInfo), sqlInfo.HasError);
                    break;
                case SqlType.Empty:
                    SetStatusLabelText(null, sqlInfo.HasError);
                    break;
            }
        }

        void SetStatusLabelText(string message, bool error)
        {
            if (error)
                View.SetStatusLabelColor(_statusLabelErrorColor);
            else
                View.SetStatusLabelColor(_statusLabelNomarlColor);

            View.SetStatusLabelText(message);
        }

        string GetQuerySqlStatusText(SqlBaseInfo sqlInfo)
        {
            if (HasQuerySqlError(sqlInfo))
                return sqlInfo.SqlMessage;
            else
                return String.Format(LocalizedTextProvider.Message.Info.MainMenuRecordCountFormat, sqlInfo.RecordCount);
        }

        bool HasQuerySqlError(SqlBaseInfo sqlInfo)
        {
            if (!sqlInfo.HasError)
                return false;

            if (sqlInfo.SqlException.GetType().Equals(typeof(OperationCanceledException)))
                return false;

            return true;
        }

        string GetNonQuerySqlStatusText(SqlBaseInfo sqlInfo)
        {
            return sqlInfo.SqlMessage;
        }

        string GetInvalidSqlStatusText(SqlBaseInfo sqlInfo)
        {
            return LocalizedTextProvider.Message.Info.MainMenuInvalidSql;
        }

        #endregion

        #endregion
    }
}