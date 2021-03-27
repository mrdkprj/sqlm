using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class JapaneseContextMenuTextProvider : IContextMenuTextProvider
    {
        static JapaneseContextMenuTextProvider _instance = new JapaneseContextMenuTextProvider();
        static JapaneseContextMenuTextProvider() { }
        private JapaneseContextMenuTextProvider() { }
        public static JapaneseContextMenuTextProvider Instance { get { return _instance; } }

        public string Cut { get { return "切り取り"; } }
        public string Copy { get { return "コピー"; } }
        public string Paste { get { return "貼り付け"; } }
        public string CopyFromObjectView { get { return "選択オブジェクト名を挿入"; } }
        public string CopyToObjectViewFilter { get { return "オブジェクト名フィルターへコピー"; } }
        public string CopyToPropertyViewFilter { get { return "プロパティ値フィルターへコピー"; } }
        public string ZoomIn { get { return "縮小"; } }
        public string ZoomOut { get { return "拡大"; } }
        public string ResetZoom { get { return "リセット"; } }
        public string AdjustHeaderWidth { get { return "列幅を調整"; } }
        public string CopyText { get { return "コピー（データ）"; } }
        public string CopyHeader { get { return "コピー（列名）"; } }
        public string CopyTextWithHeader { get { return "コピー（列名とデータ）"; } }
        public string Edit { get { return "編集"; } }
        public string ClearLog { get { return "クリア"; } }
        public string SwitchView { get { return "ビュー切り替え"; } }
        public string DisplayData { get { return "データを表示"; } }
        public string CreateSql { get { return "SQL作成"; } }
        public string Export { get { return "エクスポート"; } }
        public string Import { get { return "インポート"; } }
        public string AddRow { get { return "行を追加"; } }
        public string AddRows { get { return "複数行を追加"; } }
        public string DeleteRow { get { return "行を削除"; } }
        public string CloseTab { get { return "タブを閉じる"; } }
    }
}
