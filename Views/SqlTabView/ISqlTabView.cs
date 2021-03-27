using MasudaManager.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WinFormsMvp;

namespace MasudaManager.Views
{
    public interface ISqlTabView : IView<SqlTabModel>
    {
        event EventHandler Loaded;
        event EventHandler NewTabRequested;
        event EventHandler TabSelectionChanged;
        event EventHandler<CancelEventArgs> TabSelecting;
        event EventHandler TabCloseButtonClicked;
        event EventHandler InputViewSaveStatusChanged;
        event EventHandler<DragEventArgs> InputViewFileDropped;
        event EventHandler<KeyEventArgs> InputViewEnterKeyDown;
        event EventHandler InputViewPeriodKeyUp;
        event EventHandler InputViewFilterObjectViewClicked;
        event EventHandler InputViewFilterPropertyViewClicked;
        event EventHandler InputViewCopyFromObjectViewClicked;
        event EventHandler InputViewSaveTextRequested;
        event EventHandler ResultViewAutoResizeHeadersClicked;
        event EventHandler ResultViewCopyTextClicked;
        event EventHandler ResultViewCopyHeaderClicked;
        event EventHandler ResultViewCopyTextWithHeaderClicked;
        event EventHandler ResultViewClearResultClicked;
        event EventHandler ResultViewEditResultClicked;
        event EventHandler ResultViewSearchForwardRequested;
        event EventHandler ResultViewSearchBackwardRequested;

        string ActiveInputViewFilePath { get; }
        string ActiveInputViewText { get; }
        WriteMode ActiveInputViewWriteMode { get; }
        string ActiveInputViewZoomRatioText { get; }
        object CurrentTabPageGuid { get; }

        void ApplyTabSetting();
        void ApplyInputViewSetting();
        void ApplyResultViewSetting();
        void DisableEditResult();
        void DisableResultViewContextMenu();
        void EnableEditResult();
        void EnableResultViewContextMenu(); 
        ISynchronizeInvoke GetInvoker();
        IWin32Window GetWin32Window();
        DialogResult ShowMessageBox(string message);
        DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons button, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton);
 
        Guid CreateNewTabPage();
        void RemoveTabPage(object guid);
        void SelectTabPage(object guid);
        void SetTabPageInfoMessage(object guid, string message);
        void SetTabPageText(object guid, string text);

        int GetInputViewLastIndexOf(object guid, char target);
        string GetInputViewSelectedText(object guid);
        string GetInputViewText(object guid);
        bool InputViewFocused(object guid);
        bool IsInputViewTextSaved(object guid);
        bool IsCommented(object guid, int position);
        void LoadInputViewText(object guid, string text);
        void MarkInputViewAsSaved(object guid);
        void SetInputViewFilePath(object guid, string path);
        void SetInputViewFocus(object guid);
        void SetInputViewFont(object guid, Font font);
        void SetInputViewSelectedText(object guid, string text);
        void SetInputViewSelection(object guid, int startPosition);
        void ShowSqlInputAssistant(object guid);

        void AdjustResultViewColumnHeaderHeight(object guid);
        void AdjustResultViewColumnsWidth(object guid, DataGridViewAutoSizeColumnsMode mode);
        void AdjustResultViewRowHeaderWidth(object guid);
        void BringResultViewFront(object guid);
        void ClearResultViewSelection(object guid);
        Cell GetResultViewCurrentCell(object guid);
        object GetResultViewDataSource(object guid);
        Cell GetResultViewFirstSelectedCell(object guid);
        Cell GetResultViewLastSelectedCell(object guid);
        int GetResultViewSelectedColumnCount(object guid); 
        int GetResultViewSelectedRowCount(object guid);
        void ResetResultViewDataSource(object guid);
        void ResumeResultViewSettings(object guid);
        void SetResultViewCurrentCell(object guid, Cell cell);
        void SetResultViewDataSource(object guid, object datasource);
        void SetResultViewFocus(object guid);
        void SetResultViewFont(object guid, Font font);
        void SuspendResultViewSettings(object guid);

        void BringLogViewFront(object guid);
        void ClearResult(object guid);
        void SetLogViewFocus(object guid);
        void SetLogViewText(object guid, string text, DateTime? date, bool writeLine, string lineText = null);
    }
}
