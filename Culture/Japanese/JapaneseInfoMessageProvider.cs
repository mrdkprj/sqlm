using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class JapaneseInfoMessageProvider : IInfoMessageProvider
    {
        static JapaneseInfoMessageProvider _instance = new JapaneseInfoMessageProvider();
        static JapaneseInfoMessageProvider() { }
        private JapaneseInfoMessageProvider() { }
        public static JapaneseInfoMessageProvider Instance { get { return _instance; } }

        public string EditResultAwaitDialogProcessing { get { return "データを取得しています..."; } }
        public string EditResultAwaitDialogCancellingMessage { get { return "キャンセルしています..."; } }
        public string EditResultDiscardChanges { get { return "変更を破棄しますか？"; } }
        public string EditResultApplyChanges { get { return "変更を保存しますか？"; } }
        public string EditResultRecordCountFormat { get { return "{0}件"; } }
        public string EditResultApplyComplete { get { return "完了しました"; } }
        public string ExportStatusTextFormat { get { return "{0}件エクスポートしました"; } }
        public string ImportStatusTextFormat { get { return "{0}件インポートしました"; } }
        public string Preparing { get { return "準備しています..."; } }
        public string Cancelling { get { return "キャンセルしています..."; } }
        public string MainMenuRecordCountFormat { get { return "レコード件数：{0}件"; } }
        public string MainMenuInvalidSql { get { return "エラー"; } }
        public string CloseTab { get { return "タブを閉じますか？"; } }
        public string ClearLogView { get { return "SQL結果とログを削除しますか？?"; } }
    }
}
