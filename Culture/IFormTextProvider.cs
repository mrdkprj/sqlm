using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    public interface IFormTextProvider
    {
        string OK { get; }
        string Cancel { get; }
        string Close { get; }
        string Reset { get; }
        string CapsLock { get; }
        string NumLock { get; }
        string AutoCommit { get; }
        string InsertMode { get; }
        string OverWriteMode { get; }
        string Connect { get; }
        string Export { get; }
        string Import { get; }
        string Search { get; }
        string Preference { get; }
        string ConnectToolTip { get; }
        string DisconnectToolTip { get; }
        string AddNewTabToolTip { get; }
        string OpenFileToolTip { get; }
        string SaveFileToolTip { get; }
        string ExecuteSqlToolTip { get; }
        string CancelSqlToolTip { get; }
        string ExportToolTip { get; }
        string ImportToolTip { get; }
        string SearchToolTip { get; }
        string PreferenceToolTip { get; }
        string DataSource { get; }
        string UserId { get; }
        string Password { get; }
        string LogOnMode { get; }
        string TableName { get; }
        string File { get; }
        string ExportOption { get; }
        string Format { get; }
        string WithoutHeader { get; }
        string WithColumnNameHeader { get; }
        string Next { get; }
        string Previous { get; }
        string SearchMode { get; }
        string SearchModeExact { get; }
        string SearchModePartial { get; }
        string SearchModePrefix { get; }
        string SearchModeSuffix { get; }
        string SearchOption { get; }
        string SearchOptionSearchHeader { get; }
        string SearchOptionCloseOnSearch { get; }
        string SearchOptionCaseSensitive { get; }
        string ApplyChangesToolTip { get; }
        string AddRowToolTip { get; }
        string DeleteRowToolTip { get; }
        string AddRowsToolTip { get; }
        string UndoToolTip { get; }
        string RedoToolTip { get; }
        string ImportOption { get; }
        string PrefTab { get; }
        string PrefFont { get; }
        string PrefShowToolStrip { get; }
        string PrefShowInputText { get; }
        string PrefShowFilePath { get; }
        string PrefInput { get; }
        string PrefShowInputSupport { get; }
        string PrefEnableTableNameSupport { get; }
        string PrefEnableColumnNameSupport { get; }
        string PrefEditor { get; }
        string PrefShowLineNumber { get; }
        string PrefShowMargin { get; }
        string PrefHighlightBracket { get; }
        string PrefEnableWordwrap { get; }
        string PrefWordwrapChar { get; }
        string PrefWordwrapWord { get; }
        string PrefWordwrapSpace { get; }
        string PrefColor { get; }
        string PrefOutput { get; }
        string PrefCopyDataFormat { get; }
        string PrefSeparator { get; }
        string PrefDisplayRowNumber { get; }
        string PrefDisplaySpace { get; }
        string PrefList { get; }
        string PrefDoubleClickInsert { get; }
        string PrefInsertObjectName { get; }
        string PrefEncloseObjectName { get; }
        string PrefInsertPropertyValue { get; }
        string PrefEnclosePropertyValue { get; }
        string PrefSql { get; }
        string PrefDisplaySqlProgress { get; }
        string PrefAllowAutoCommit { get; }
        string PrefRunSqlOnEnter { get; }
        string PrefIgnoreError { get; }
        string PrefTimeout { get; }
        string PrefTimeoutSecond { get; }
        string PrefFile { get; }
        string PrefEncoding { get; }
        string PrefFileOpenMode { get; }
        string PrefOpenWriteLock { get; }
        string PrefOpenReadLock { get; }
        string PrefExportImport { get; }
        string PrefEncloseFields { get; }
    }
}
