using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class JapaneseFormTextProvider : IFormTextProvider
    {
        static JapaneseFormTextProvider _instance = new JapaneseFormTextProvider();
        static JapaneseFormTextProvider() { }
        private JapaneseFormTextProvider() { }
        public static JapaneseFormTextProvider Instance { get { return _instance; } }

        public string OK { get { return "OK"; } }
        public string Cancel { get { return "キャンセル"; } }
        public string Close { get { return "閉じる"; } }
        public string Reset { get { return "リセット"; } }
        public string CapsLock { get { return "CAPS"; } }
        public string NumLock { get { return "NUM"; } }
        public string InsertMode { get { return "挿入"; } }
        public string OverWriteMode { get { return "上書き"; } }
        public string AutoCommit { get { return "自動コミット"; } }
        public string Connect { get { return "接続"; } }
        public string Export { get { return "エクスポート"; } }
        public string Import { get { return "インポート"; } }
        public string Search { get { return "検索"; } }
        public string Preference { get { return "設定"; } }
        public string ConnectToolTip { get { return "接続 (Ctrl + D)"; } }
        public string DisconnectToolTip { get { return "切断"; } }
        public string AddNewTabToolTip { get { return "タブを追加 (Ctrl + N)"; } }
        public string OpenFileToolTip { get { return "ファイルを開く (Ctrl + O)"; } }
        public string SaveFileToolTip { get { return "ファイルを保存 (Ctrl + S)"; } }
        public string ExecuteSqlToolTip { get { return "SQL実行 (Ctrl + R)"; } }
        public string CancelSqlToolTip { get { return "中断 (Esc)"; } }
        public string ExportToolTip { get { return "エクスポート"; } }
        public string ImportToolTip { get { return "インポート"; } }
        public string SearchToolTip { get { return "検索 (Ctrl + F)"; } }
        public string PreferenceToolTip { get { return "設定"; } }
        public string DataSource { get { return "Data Source : "; } }
        public string UserId { get { return "User ID : "; } }
        public string Password { get { return "Password : "; } }
        public string LogOnMode { get { return "Mode : "; } }
        public string TableName { get { return "Table : "; } }
        public string File { get { return "File : "; } }
        public string ExportOption { get { return "オプション"; } }
        public string Format { get { return "　形式 : "; } }
        public string WithoutHeader { get { return "ヘッダーなし"; } }
        public string WithColumnNameHeader { get { return "ヘッダーあり（カラム名）"; } }
        public string Next { get { return "次へ"; } }
        public string Previous { get { return "前へ"; } }
        public string SearchMode { get { return "一致条件"; } }
        public string SearchModeExact { get { return "完全"; } }
        public string SearchModePartial { get { return "部分"; } }
        public string SearchModePrefix { get { return "前方"; } }
        public string SearchModeSuffix { get { return "後方"; } }
        public string SearchOption { get { return "オプション"; } }
        public string SearchOptionSearchHeader { get { return "ヘッダーを検索"; } }
        public string SearchOptionCloseOnSearch { get { return "検索後に閉じる"; } }
        public string SearchOptionCaseSensitive { get { return "大文字小文字を区別"; } }
        public string ApplyChangesToolTip { get { return "変更を適用 (Ctrl + S)"; } }
        public string AddRowToolTip { get { return "行を追加 (Ctrl + N)"; } }
        public string DeleteRowToolTip { get { return "行を削除 (Ctrl + D)"; } }
        public string AddRowsToolTip { get { return "複数行を追加 (Ctrl + N)"; } }
        public string UndoToolTip { get { return "元に戻す (Ctrl + Z)"; } }
        public string RedoToolTip { get { return "やり直し (Ctrl + Y)"; } }
        public string ImportOption { get { return "オプション"; } }
        public string PrefTab { get { return "タブ"; } }
        public string PrefFont { get { return "フォント"; } }
        public string PrefShowToolStrip { get { return "ツールストリップテキストを表示"; } }
        public string PrefShowInputText { get { return "テキスト内容を表示"; } }
        public string PrefShowFilePath { get { return "ファイルのパスを表示"; } }
        public string PrefInput { get { return "入力"; } }
        public string PrefShowInputSupport { get { return "ピリオド押下で入力候補を表示"; } }
        public string PrefEnableTableNameSupport { get { return "テーブル名の候補を表示する"; } }
        public string PrefEnableColumnNameSupport { get { return "カラム名の候補を表示する"; } }
        public string PrefEditor { get { return "エディタ"; } }
        public string PrefShowLineNumber { get { return "行番号を表示"; } }
        public string PrefShowMargin { get { return "マージンを表示"; } }
        public string PrefHighlightBracket { get { return "一致する括弧を強調表示"; } }
        public string PrefEnableWordwrap { get { return "ワードラップを有効にする"; } }
        public string PrefWordwrapChar { get { return "文字"; } }
        public string PrefWordwrapWord { get { return "単語"; } }
        public string PrefWordwrapSpace { get { return "スペース"; } }
        public string PrefColor { get { return "文字色"; } }
        public string PrefOutput { get { return "出力"; } }
        public string PrefCopyDataFormat { get { return "コピーデータ形式"; } }
        public string PrefSeparator { get { return "　区切り : "; } }
        public string PrefDisplayRowNumber { get { return "行番号を表示"; } }
        public string PrefDisplaySpace { get { return "空白文字を表示"; } }
        public string PrefList { get { return "一覧"; } }
        public string PrefDoubleClickInsert { get { return "選択項目を挿入する"; } }
        public string PrefInsertObjectName { get { return "ダブルクリックでオブジェクト名を挿入"; } }
        public string PrefEncloseObjectName { get { return "オブジェクト名を二重引用符で囲む"; } }
        public string PrefInsertPropertyValue { get { return "ダブルクリックでプロパティ値を挿入"; } }
        public string PrefEnclosePropertyValue { get { return "プロパティ値を二重引用符で囲む"; } }
        public string PrefSql { get { return "SQL"; } }
        public string PrefDisplaySqlProgress { get { return "結果をリアルタイムで表示する"; } }
        public string PrefAllowAutoCommit { get { return "自動的にコミットする"; } }
        public string PrefRunSqlOnEnter { get { return "エンターキー押下でSQLを実行する"; } }
        public string PrefIgnoreError { get { return "エラー発生時も処理を継続する"; } }
        public string PrefTimeout { get { return "タイムアウト"; } }
        public string PrefTimeoutSecond { get { return "秒"; } }
        public string PrefFile { get { return "ファイル"; } }
        public string PrefEncoding { get { return "エンコード"; } }
        public string PrefFileOpenMode { get { return "ファイルの排他制御"; } }
        public string PrefOpenWriteLock { get { return "書き込みを禁止する"; } }
        public string PrefOpenReadLock { get { return "読み込みを禁止する"; } }
        public string PrefExportImport { get { return "エクスポート・インポート"; } }
        public string PrefEncloseFields { get { return "フィールドを二重引用符で囲む"; } }
    }
}
