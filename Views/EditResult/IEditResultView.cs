using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WinFormsMvp;
using MasudaManager.Utility;

namespace MasudaManager.Views
{
    public interface IEditResultView : IView<EditResultModel>
    {
        event EventHandler<GenericEventArgs<string>> Initiated;
        event EventHandler AwaitDialogCancelButtonClicked;
        event EventHandler EditorStatusRequested;
        event EventHandler ApplyButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler InsertButtonClicked;
        event EventHandler DeleteButtonClicked;
        event EventHandler BulkInsertButtonClicked;
        event EventHandler UndoButtonClicked;
        event EventHandler RedoButtonClicked;
        event EventHandler<CancelEventArgs> CellBeginEdit;
        event EventHandler CellEndEdit;
        event EventHandler DataCopyRequested;
        event EventHandler DataPasteRequested;
        event EventHandler DeleteKeyDown;
        event EventHandler SearchViewRequested;
        event EventHandler SearchForwardRequested;
        event EventHandler SearchBackwardRequested;
        event EventHandler<CancelEventArgs> ReleaseRequested;
        event EventHandler<CancelEventArgs> ViewClosing;

        bool ApplyButtonEnabled { get; set; }
        bool BulkInsertButtonEnabled { get; set; }
        int BulkInsertRowCount { get; }
        bool CancelButtonEnabled { get; set; }
        bool ContextMenuEnabled { get; set; }
        bool DeleteButtonEnabled { get; set; }
        bool DeleteContextMenuEnabled { get; set; }
        IGridViewEditingCoordinator EditingCoordinator { get; }
        Cell FirstSelectedCell { get; }
        string FormText { get; set; }
        bool InsertButtonEnabled { get; set; }
        Cell LastSelectedCell { get; }
        bool MainGridViewEnabled { get; set; }
        bool RedoButtonEnabled { get; set; }
        int SelectedColumnCount { get; }
        int SelectedRowCount { get; }
        bool UndoButtonEnabled { get; set; }

        void ClearSelection();
        void CloseAwaitDialog();
        void CloseView();
        void DisableMainGridSettings();
        void EnableMainGridSettings();
        void FocusOnCell(Cell cell);
        IWin32Window GetParentWindow();
        IWin32Window GetWin32Window();
        void ResumeMainGridLayout();
        void ResumeCellValidation();
        void RestoreSelection(Cell cell);
        void RaiseChildViewClosed();
        void SetAwaitDialogStatusText(string text);
        void SetMainGridDataSource(object datasource);
        void ShowAwaitDialog(IWin32Window owner);
        DialogResult ShowMessage(string message);
        DialogResult ShowMessage(string message, string caption, MessageBoxButtons button, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton);
        void ShowModeless();
        void SuspendCellValidation();
        void SuspendMainGridLayout();
        void UpdateStatusLabelText(string text);
        bool ValidateCellValue(Cell cell, object value);
    }
}
