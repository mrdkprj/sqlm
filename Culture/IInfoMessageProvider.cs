using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    public interface IInfoMessageProvider
    {
        string EditResultAwaitDialogProcessing { get; }
        string EditResultAwaitDialogCancellingMessage { get; }
        string EditResultDiscardChanges { get; }
        string EditResultApplyChanges { get; }
        string EditResultRecordCountFormat { get; }
        string EditResultApplyComplete { get; }
        string ExportStatusTextFormat { get; }
        string ImportStatusTextFormat { get; }
        string Preparing { get; }
        string Cancelling { get; }
        string MainMenuRecordCountFormat { get; }
        string MainMenuInvalidSql { get; }
        string CloseTab { get; }
        string ClearLogView { get; }
    }
}
