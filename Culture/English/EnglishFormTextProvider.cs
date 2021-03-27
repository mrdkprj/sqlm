using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class EnglishFormTextProvider : IFormTextProvider
    {
        static EnglishFormTextProvider _instance = new EnglishFormTextProvider();
        static EnglishFormTextProvider() { }
        private EnglishFormTextProvider() { }
        public static EnglishFormTextProvider Instance { get { return _instance; } }

        public string OK { get { return "OK"; } }
        public string Cancel { get { return "Cancel"; } }
        public string Close { get { return "Close"; } }
        public string Reset { get { return "Reset"; } }
        public string CapsLock { get { return "CAPS"; } }
        public string NumLock { get { return "NUM"; } }
        public string InsertMode { get { return "INSERT"; } }
        public string OverWriteMode { get { return "OVERWRITE"; } }
        public string AutoCommit { get { return "AUTO COMMIT"; } }
        public string Connect { get { return "Connect"; } }
        public string Export { get { return "Export"; } }
        public string Import { get { return "Import"; } }
        public string Search { get { return "Search"; } }
        public string Preference { get { return "Preference"; } }
        public string ConnectToolTip { get { return "Connect (Ctrl + D)"; } }
        public string DisconnectToolTip { get { return "Disconnect"; } }
        public string AddNewTabToolTip { get { return "Add new tab (Ctrl + N)"; } }
        public string OpenFileToolTip { get { return "Open file (Ctrl + O)"; } }
        public string SaveFileToolTip { get { return "Save file (Ctrl + S)"; } }
        public string ExecuteSqlToolTip { get { return "Execute SQL (Ctrl + R)"; } }
        public string CancelSqlToolTip { get { return "Cancel (Esc)"; } }
        public string ExportToolTip { get { return "Export"; } }
        public string ImportToolTip { get { return "Import"; } }
        public string SearchToolTip { get { return "Search (Ctrl + F)"; } }
        public string PreferenceToolTip { get { return "Preference"; } }
        public string DataSource { get { return "Data Source : "; } }
        public string UserId { get { return "User ID : "; } }
        public string Password { get { return "Password : "; } }
        public string LogOnMode { get { return "Mode : "; } }
        public string TableName { get { return "Table : "; } }
        public string File { get { return "File : "; } }
        public string ExportOption { get { return "Export option"; } }
        public string Format { get { return "Format : "; } }
        public string WithoutHeader { get { return "No header"; } }
        public string WithColumnNameHeader { get { return "Column name header"; } }
        public string Next { get { return "Next"; } }
        public string Previous { get { return "Prev"; } }
        public string SearchMode { get { return "Mode"; } }
        public string SearchModeExact { get { return "Exact"; } }
        public string SearchModePartial { get { return "Partial"; } }
        public string SearchModePrefix { get { return "Prefix"; } }
        public string SearchModeSuffix { get { return "Suffix"; } }
        public string SearchOption { get { return "Search option"; } }
        public string SearchOptionSearchHeader { get { return "Search header"; } }
        public string SearchOptionCloseOnSearch { get { return "Close after search"; } }
        public string SearchOptionCaseSensitive { get { return "Case-sensitive"; } }
        public string ApplyChangesToolTip { get { return "Apply changes (Ctrl + S)"; } }
        public string AddRowToolTip { get { return "Add new row (Ctrl + N)"; } }
        public string DeleteRowToolTip { get { return "Delete row (Ctrl + D)"; } }
        public string AddRowsToolTip { get { return "Add new rows (Ctrl + N)"; } }
        public string UndoToolTip { get { return "Undo (Ctrl + Z)"; } }
        public string RedoToolTip { get { return "Redo (Ctrl + Y)"; } }
        public string ImportOption { get { return "Import option"; } }
        public string PrefTab { get { return "Tab"; } }
        public string PrefFont { get { return "Font"; } }
        public string PrefShowToolStrip { get { return "Show toolstrip text"; } }
        public string PrefShowInputText { get { return "Show input text"; } }
        public string PrefShowFilePath { get { return "Show file path"; } }
        public string PrefInput { get { return "Input"; } }
        public string PrefShowInputSupport { get { return "Show input support after period"; } }
        public string PrefEnableTableNameSupport { get { return "Enable table name support"; } }
        public string PrefEnableColumnNameSupport { get { return "Enable column name support"; } }
        public string PrefEditor { get { return "Editor"; } }
        public string PrefShowLineNumber { get { return "Show line number"; } }
        public string PrefShowMargin { get { return "Show selection margin"; } }
        public string PrefHighlightBracket { get { return "Highlight matching brackets"; } }
        public string PrefEnableWordwrap { get { return "Enable wordwrap"; } }
        public string PrefWordwrapChar { get { return "Char"; } }
        public string PrefWordwrapWord { get { return "Word"; } }
        public string PrefWordwrapSpace { get { return "Space"; } }
        public string PrefColor { get { return "Color"; } }
        public string PrefOutput { get { return "Output"; } }
        public string PrefCopyDataFormat { get { return "Copy data format"; } }
        public string PrefSeparator { get { return "Separator : "; } }
        public string PrefDisplayRowNumber { get { return "Display row number on row header"; } }
        public string PrefDisplaySpace { get { return "Display space character"; } }
        public string PrefList { get { return "List"; } }
        public string PrefDoubleClickInsert { get { return "Insert item name"; } }
        public string PrefInsertObjectName { get { return "Insert object name by double click"; } }
        public string PrefEncloseObjectName { get { return "Enclose object name with double quotations"; } }
        public string PrefInsertPropertyValue { get { return "Insert property value by double click"; } }
        public string PrefEnclosePropertyValue { get { return "Enclose property value with double quotations"; } }
        public string PrefSql { get { return "SQL"; } }
        public string PrefDisplaySqlProgress { get { return "Display real-time result of a query"; } }
        public string PrefAllowAutoCommit { get { return "Automatically commit transaction"; } }
        public string PrefRunSqlOnEnter { get { return "Execute SQL by Enter Key after semicolon"; } }
        public string PrefIgnoreError { get { return "Continue after error"; } }
        public string PrefTimeout { get { return "Command timeout"; } }
        public string PrefTimeoutSecond { get { return "sec."; } }
        public string PrefFile { get { return "File"; } }
        public string PrefEncoding { get { return "Encoding"; } }
        public string PrefFileOpenMode { get { return "File open mode"; } }
        public string PrefOpenWriteLock { get { return "Open with WRITE lock"; } }
        public string PrefOpenReadLock { get { return "Open with READ lock"; } }
        public string PrefExportImport { get { return "Export/Import option"; } }
        public string PrefEncloseFields { get { return "Enclose field values"; } }
    }
}
