using MasudaManager.Utility;
using MasudaManager.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMvp;
using WinFormsMvp.Binder;
using WinFormsMvp.Messaging;

namespace MasudaManager.Presenters
{
    public class EditResultPresenter : Presenter<IEditResultView>, IObserver
    {
        EditCounter _counter = new EditCounter();
        EditResultEditor _editor = new EditResultEditor();
        EditResultDbCommandBuilder _commandBuilder = new EditResultDbCommandBuilder();
        EditResultObserverContext _observerContext = new EditResultObserverContext();
        CancellationTokenSource _tokenSource = new CancellationTokenSource();
        Cell _defaultCell = new Cell(0, 0);
        Guid _queryGuid = Guid.NewGuid();
        Guid _nonQueryTuid = Guid.NewGuid();

        readonly string _messageCaption = "EditSqlResult";
        readonly string _editCountFormat = "{0} :{1}";

        #region Constructor
        public EditResultPresenter(IEditResultView view)
            : base(view)
        {
            View.Model = new EditResultModel();
            
            View.Model.OnQueryCancelled = View.RaiseChildViewClosed;
            View.Model.SqlTaskManager = new SqlTaskManager(this, null);
            View.Model.SqlTaskManager.TaskCompleteAction = OnTaskComplete;
            View.Model.SqlTaskManager.DefaultErrorAction = SqlTaskOnErrorActionType.Cancel;

            _observerContext.RegisterCallBack(PrepareView);
            _observerContext.View = view;

            RegisterHandlers();
            PrepareSearchViewRequestData();
        }
        #endregion

        #region EventHandlers

        #region RegisterHandlers
        void RegisterHandlers()
        {
            View.Initiated += View_Initiated;
            View.AwaitDialogCancelButtonClicked += View_AwaitDialogCancelButtonClicked;
            View.EditorStatusRequested += View_EditorStatusRequested;
            View.ApplyButtonClicked += View_ApplyButtonClicked;
            View.CancelButtonClicked += View_CancelButtonClicked;
            View.InsertButtonClicked += View_InsertButtonClicked;
            View.DeleteButtonClicked += View_DeleteButtonClicked;
            View.BulkInsertButtonClicked += View_BulkInsertButtonClicked;
            View.UndoButtonClicked += View_UndoButtonClicked;
            View.RedoButtonClicked += View_RedoButtonClicked;
            View.CellBeginEdit += View_CellBeginEdit;
            View.CellEndEdit += View_CellEndEdit;
            View.DataCopyRequested += View_DataCopyRequested;
            View.DataPasteRequested += View_DataPasteRequested;
            View.DeleteKeyDown += View_DeleteKeyDown;
            View.ViewClosing += View_ViewClosing;
            View.SearchViewRequested += View_SearchViewRequested;
            View.SearchForwardRequested += View_SearchForwardRequested;
            View.SearchBackwardRequested += View_SearchBackwardRequested;
            View.ReleaseRequested += View_ReleaseRequested;
        }
        #endregion

        #region Initiated
        void View_Initiated(object sender, GenericEventArgs<string> e)
        {
            View.Model.SourceSql = e.Data;

            if (!ValidateCondition())
                View.RaiseChildViewClosed();
            else
                ShowAwaitDialog();
        }
        #endregion

        #region AwaitDialog Cancel button click
        void View_AwaitDialogCancelButtonClicked(object sender, EventArgs e)
        {
            View.SetAwaitDialogStatusText(LocalizedTextProvider.Message.Info.EditResultAwaitDialogCancellingMessage);
            View.Model.SqlTaskManager.Cancel();
        }
        #endregion

        #region ViewClosing
        void View_ViewClosing(object sender, CancelEventArgs e)
        {
            OnViewClosing(e);
        }
        #endregion

        #region Release requested
        void View_ReleaseRequested(object sender, CancelEventArgs e)
        {
            OnViewClosing(e);
        }
        #endregion

        #region EditorStatusRequested
        void View_EditorStatusRequested(object sender, EventArgs e)
        {
            View.UndoButtonEnabled = _editor.CanUndo;
            View.RedoButtonEnabled = _editor.CanRedo;

            View.DeleteButtonEnabled = View.EditingCoordinator.RowCount > 0;
            View.DeleteContextMenuEnabled = View.EditingCoordinator.RowCount > 0;
        }
        #endregion

        #region Apply button click
        void View_ApplyButtonClicked(object sender, EventArgs e)
        {
            if (ConfirmSave() == DialogResult.Yes)
                ApplyChanges();
        }
        #endregion

        #region Cancel button click
        void View_CancelButtonClicked(object sender, EventArgs e)
        {
            Cancel();
        }
        #endregion

        #region Insert button click
        void View_InsertButtonClicked(object sender, EventArgs e)
        {
            _editor.Insert();
        }
        #endregion

        #region Delete button click
        void View_DeleteButtonClicked(object sender, EventArgs e)
        {
            _editor.Delete(View.EditingCoordinator.GetSelectedRows(), View.Model.HeaderColumnNames.Count);
        }
        #endregion
        
        #region Bulk insert button click
        void View_BulkInsertButtonClicked(object sender, EventArgs e)
        {
            if (View.BulkInsertRowCount > 0)
                _editor.BulkInsert(View.BulkInsertRowCount);
        }
        #endregion

        #region Redo button click
        void View_RedoButtonClicked(object sender, EventArgs e)
        {
            _editor.Redo();
        }
        #endregion

        #region Undo button click
        void View_UndoButtonClicked(object sender, EventArgs e)
        {
            _editor.Undo();
        }
        #endregion

        #region DataCopyRequested
        void View_DataCopyRequested(object sender, EventArgs e)
        {
            if (View.EditingCoordinator.RowCount <= 0)
                return;

            QueryResultClipboardSetter.Copy(GetSelectedCellRange(), View.Model.BindingList);
        }
        #endregion

        #region DataPasteRequested
        void View_DataPasteRequested(object sender, EventArgs e)
        {           
            if (View.EditingCoordinator.RowCount <= 0)
                return; 
            
            View.SuspendCellValidation();

            _editor.Paste(GetPasteDataCellRange());

            View.ResumeCellValidation();
        }
        #endregion

        #region CellBeginEdit
        void View_CellBeginEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _editor.BeginUpdate();
        }
        #endregion

        #region CellEndEdit
        void View_CellEndEdit(object sender, EventArgs e)
        {
            _editor.EndUpdate();
        }
        #endregion

        #region Delete key down
        void View_DeleteKeyDown(object sender, EventArgs e)
        {
            _editor.Erase(View.EditingCoordinator.GetSelectedCells());
        }
        #endregion

        #region Search
        void View_SearchViewRequested(object sender, EventArgs e)
        {
            ShowSearchView();
        }

        void View_SearchForwardRequested(object sender, EventArgs e)
        {
            RequestSearch(SearchDirection.Forward);
        }

        void View_SearchBackwardRequested(object sender, EventArgs e)
        {
            RequestSearch(SearchDirection.Backward);
        }
        #endregion

        #endregion

        #region Messages
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
        #endregion

        #region Methods

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
            View.Model.SearchContext.SqlResults = View.Model.BindingList.ToSourceList();
            View.Model.SearchContext.CurrentCell = View.EditingCoordinator.CurrentCell;

            SendSearchContext(View.Model.SearchContext);
        }

        void OnSearchComplete(Cell cell)
        {
            View.ClearSelection();
            View.FocusOnCell(cell);
        }
        #endregion

        #region ShowAwaitDialog
        void ShowAwaitDialog()
        {
            View.SetAwaitDialogStatusText(LocalizedTextProvider.Message.Info.EditResultAwaitDialogProcessing);
            View.ShowAwaitDialog(View.GetParentWindow());

            ExecuteSql(_queryGuid, new DbCommandBuilder(View.Model.SourceSql), true);
        }
        #endregion

        #region ReleasePresenter
        void ReleasePresenter()
        {
            View.Model.SqlTaskManager.Release(this);
            _commandBuilder.Release();
            _editor.Release();
            View.Model.ReleaseModel();

            PresenterBinder.Factory.Release(this);
        }
        #endregion

        #region OnViewClosing
        void OnViewClosing(CancelEventArgs e)
        {
            if (View.Model.SqlTaskManager.IsProcessBusy())
            {
                View.Model.SqlTaskManager.Cancel();
                e.Cancel = true;
                return;
            }

            if (ConfirmDiscard() != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            ReleasePresenter();
        }
        #endregion

        #region ValidateCondition
        bool ValidateCondition()
        {
            if (String.IsNullOrEmpty(View.Model.SourceSql))
                return false;

            EditResultRequisiteChecker requisiteChecker = new EditResultRequisiteChecker(View.Model);
            
            try
            {
                requisiteChecker.CheckSql();
                requisiteChecker.OrganizeModel();
            }
            catch (TableKeyNotFoundException keynotfound)
            {
                return ConfirmContinueWithoutPrimaryKey(keynotfound) == DialogResult.Yes;
            }
            catch (Exception ex)
            {
                View.ShowMessage(ex.Message);
                return false;
            }

            return true;
        }
        #endregion

        #region Execute SQL
        async void ExecuteSql(Guid guid, IDbCommandBuilder dbCommands, bool observe)
        {
            await View.Model.SqlTaskManager.RunSqlTask(guid, dbCommands, observe);
        }
        #endregion

        #region PrepareView
        void PrepareView(ISqlTask sqlTask)
        {
            sqlTask.BindingModel.SupportSort = false;
            View.Model.BindingList = sqlTask.BindingModel;

            View.CloseAwaitDialog();
            View.ShowModeless();
            View.FormText = View.Model.TableName;

            PrepareEditor();
            BindDataSource(View.Model.BindingList);
            ReleaseView();

            View.UpdateStatusLabelText(String.Format(LocalizedTextProvider.Message.Info.EditResultRecordCountFormat, View.EditingCoordinator.RowCount));
        }
        #endregion

        #region Bind DataSource
        void BindDataSource(DynamicSortableBindingList<SqlResult, QueryResult> bindingList)
        {
            View.DisableMainGridSettings();
            View.SetMainGridDataSource(bindingList);
            View.EnableMainGridSettings();

            if(View.EditingCoordinator.RowCount > 0)
            {
                View.ClearSelection();
                View.FocusOnCell(_defaultCell);
            }
        }
        #endregion

        #region PrepareEditor
        void PrepareEditor()
        {
            _editor.Coordinator = View.EditingCoordinator;
            _editor.EditCompleteAction = UpdateDisplay;
            _editor.InsertPosition = View.Model.InsertPosition;
            _editor.PrepareUndoRedoStack(GetFieldCount());
            _editor.EditColor = View.Model.EditColor;
            View.EditingCoordinator.EditColor = View.Model.EditColor;
        }
        
        int GetFieldCount()
        {
            if (View.Model.BindingList.Count <= 0)
                return 0;

            return View.Model.BindingList.Count * View.Model.BindingList.PropertyNames.Count;
        }
        #endregion

        #region Lock/Release view
        void LockView()
        {
            View.ApplyButtonEnabled = false;
            View.CancelButtonEnabled = true;
            View.InsertButtonEnabled = false;
            View.DeleteButtonEnabled = false;
            View.BulkInsertButtonEnabled = false;
            View.UndoButtonEnabled = false;
            View.RedoButtonEnabled = false;
            View.MainGridViewEnabled = false;
            View.ContextMenuEnabled = false;
        }

        void ReleaseView()
        {
            View.ApplyButtonEnabled = true;
            View.CancelButtonEnabled = false;
            View.InsertButtonEnabled = true;
            View.DeleteButtonEnabled = true;
            View.BulkInsertButtonEnabled = true;
            View.UndoButtonEnabled = true;
            View.RedoButtonEnabled = true;
            View.MainGridViewEnabled = true;
            View.ContextMenuEnabled = true;
        }

        #endregion

        #region ApplyChanges
        async void ApplyChanges()
        {
            LockView();

            _tokenSource = new CancellationTokenSource();

            try
            {
                await Task.Run(() => CreateDmlCommands(_tokenSource.Token))
                    .ContinueWith(antecedent =>
                    {
                        View.Model.HasApplyError = false;
                        ExecuteSql(_nonQueryTuid, antecedent.Result, true);
                    });
            }
            catch (AggregateException agge)
            {
                View.ShowMessage(agge.InnerException.Message);
                View.FocusOnCell(View.Model.ApplyingCells.Peek());
                ReleaseView();
            }
            catch (Exception e)
            {
                View.ShowMessage(e.Message);
                ReleaseView();
            }
        }
        #endregion

        #region CreateDmlCommands
        IDbCommandBuilder CreateDmlCommands(CancellationToken token)
        {
            _commandBuilder.SetModel(View.Model);
            _commandBuilder.Clear();
            View.Model.ApplyingCells.Clear();

            foreach (var editData in _editor.ApplicableEdition)
            {
                token.ThrowIfCancellationRequested();

                View.Model.ApplyingCells.Enqueue(editData.Cell);
                View.Model.KeyColumnData.SetValues(GetKeyColumnValues(editData));
                _commandBuilder.CreateDbCommands(editData);
            }

            return _commandBuilder;
        }
        #endregion

        #region Get KeyColumn values
        IEnumerable<object> GetKeyColumnValues(EditData editData)
        {
            Cell cell = new Cell(0, editData.Cell.RowIndex);
            foreach (int columnIndex in View.Model.KeyColumnData.ColumnIndices)
            {
                cell.ColumnIndex = columnIndex;

                if (View.EditingCoordinator.OriginalValueExists(cell))
                    yield return View.EditingCoordinator.GetCellOriginalValue(cell);
                else
                    yield return View.EditingCoordinator.GetCellValue(cell);
            }
        }
        #endregion

        #region Cancel
        void Cancel()
        {
            if (!_tokenSource.IsCancellationRequested)
                _tokenSource.Cancel();

            View.Model.SqlTaskManager.Cancel();
        }
        #endregion

        #region GetSelectedCellRange
        CellRange GetSelectedCellRange()
        {
            CellRange range = new CellRange();
            range.StartCell = View.FirstSelectedCell;
            range.EndCell = View.LastSelectedCell;
            range.ColumnCount = View.SelectedColumnCount;
            range.RowCount = View.SelectedRowCount;
            return range;
        }        
        #endregion

        #region GetPasteDataCellRange
        CellRange GetPasteDataCellRange()
        {
            CellRange range = new CellRange();
            range.StartCell = View.FirstSelectedCell;
            range.EndCell = View.LastSelectedCell;
            range.ColumnCount = View.EditingCoordinator.ColumnCount;
            range.RowCount = View.EditingCoordinator.RowCount;
            return range;
        }
        #endregion

        #region PreUpdateDisplay
        void PreUpdateDisplay()
        {
            View.SuspendMainGridLayout();
        }
        #endregion

        #region Update display
        void UpdateDisplay()
        {
            PreUpdateDisplay();

            foreach (var editData in _editor.CurrentEdition)
            {
                switch (editData.Type)
                {
                    case EditType.Edit:
                    case EditType.Update:
                        UpdateCell(editData);
                        break;
                    case EditType.Remove:
                        RemoveRow(editData);
                        break;
                    case EditType.BulkInsert:
                        BulkInsert(editData);
                        break;
                    case EditType.Insert:
                        InsertRow(editData);
                        break;
                    case EditType.Delete:
                        DeleteRow(editData);
                        break;
                }

                View.EditingCoordinator.SetRestoringCurrentCell(editData.Cell);
            }

            PostUpdateDisplay();
        }

        void UpdateCell(EditData editData)
        {
            if (View.Model.KeyColumnData.ColumnIndices.Contains(editData.Cell.ColumnIndex))
                View.EditingCoordinator.SaveOriginalCellValue(editData.Cell);

            View.EditingCoordinator.SetCellValue(editData.Cell, editData.Value.ToStringOrNull());
            View.EditingCoordinator.SetCellColor(editData.Cell, editData.BackColor);
        }

        void RemoveRow(EditData editData)
        {
            View.EditingCoordinator.PrepareRemove(editData.Cell.RowIndex);
        }

        void BulkInsert(EditData editData)
        {
            View.EditingCoordinator.PrepareInsert(editData.Cell.RowIndex, editData.Value);
        }

        void InsertRow(EditData editData)
        {
            View.EditingCoordinator.ExecuteInsert(editData.Cell.RowIndex, editData.Value);
        }

        void DeleteRow(EditData editData)
        {
            View.EditingCoordinator.SetRowColor(editData.Cell.RowIndex, editData.BackColor);
        }
        #endregion

        #region PostUpdateDisplay
        void PostUpdateDisplay()
        {
            View.EditingCoordinator.ExecutePreparedInsert();
            View.EditingCoordinator.ExecutePreparedRemove();

            View.ResumeMainGridLayout();

            View.EditingCoordinator.SuppressCellSelectionChange = false;

            RestoreCellSelection();

            View.UpdateStatusLabelText(String.Format(LocalizedTextProvider.Message.Info.EditResultRecordCountFormat, View.EditingCoordinator.RowCount));
        }

        void RestoreCellSelection()
        {
            if (!CanRestoreCell())
                return;

            View.EditingCoordinator.RestoreCurrentCell();
            View.RestoreSelection(GetRestorableCell());
        }

        bool CanRestoreCell()
        {
            if(View.EditingCoordinator.RowCount <= 0)
                return false;

            if (View.EditingCoordinator.IsAllSelected)
                return false;

            if (View.EditingCoordinator.SelectedCellsCount > 1)
                return false;

            return true;
        }

        Cell GetRestorableCell()
        {
            if (View.EditingCoordinator.CurrentCell.RowIndex <= View.EditingCoordinator.RowCount)
                return View.EditingCoordinator.CurrentCell;

            return _defaultCell;
        }
        #endregion

        #region Confirm to continue without primary key
        DialogResult ConfirmContinueWithoutPrimaryKey(TableKeyNotFoundException e)
        {
            return View.ShowMessage
                (
                    String.Format(LocalizedTextProvider.Message.Warning.EditResultKeyColumnWarningFormat, e.Message),
                    _messageCaption,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );
        }
        #endregion

        #region Confirm discard
        DialogResult ConfirmDiscard()
        {
            if (!_editor.CanUndo)
                return DialogResult.Yes;

            return View.ShowMessage
                (
                    LocalizedTextProvider.Message.Info.EditResultDiscardChanges,
                    _messageCaption,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1
                );
        }
        #endregion

        #region Confirm save
        DialogResult ConfirmSave()
        {
            if (!_editor.CanUndo)
                return DialogResult.Ignore;

            _counter.Reset();
            _counter.Count(_editor.RebuildEditions());
            
            return View.ShowMessage
                (
                    GetSaveMessage(),
                    _messageCaption,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1
                );
        }

        string GetSaveMessage()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GetEditCountMessage());
            builder.AppendLine();
            builder.Append(LocalizedTextProvider.Message.Info.EditResultApplyChanges);

            return builder.ToString();
        }

        string GetEditCountMessage()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(String.Format(_editCountFormat, "Update", _counter.UpdateCount));
            builder.AppendLine();
            builder.Append(String.Format(_editCountFormat, "Insert", _counter.InsertCount));
            builder.AppendLine();
            builder.Append(String.Format(_editCountFormat, "Delete", _counter.DeleteCount));
            builder.AppendLine();

            return builder.ToString();
        }
        #endregion     

        #region OnTaskComplete
        void OnTaskComplete(ISqlTask sqlTask, TaskCompleteEventArgs e)
        {
            if (sqlTask.Guid.Equals(_queryGuid))
                return;

            if(View.Model.HasApplyError)
                OnApplyFailed();
            else
                OnApplyComplete();
        }

        void OnApplyComplete()
        {
            View.Model.SqlTaskManager.CurrentTask.Commit();
            _editor.Release();
            View.ShowMessage(LocalizedTextProvider.Message.Info.EditResultApplyComplete);
            View.CloseView();
        }

        void OnApplyFailed()
        {
            View.Model.SqlTaskManager.CurrentTask.Rollback();
        }
        #endregion

        #region Observer
        public void Update(object sender)
        {
        }

        public void Complete(object sender)
        {
            ActOnObserver(ObserverActionType.OnComplete, (ISqlTask)sender);
        }

        public void Error(object sender, Exception e)
        {
            ActOnObserver(ObserverActionType.OnError, (ISqlTask)sender);
        }

        void ActOnObserver(ObserverActionType actionType, ISqlTask sqlTask)
        {
            _observerContext.ChangeStrategy(sqlTask);
            _observerContext.Act(actionType);
        }
        #endregion

        #endregion
    }
}
