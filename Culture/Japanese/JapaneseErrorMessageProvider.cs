using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class JapaneseErrorMessageProvider : IErrorMessageProvider
    {
        static JapaneseErrorMessageProvider _instance = new JapaneseErrorMessageProvider();
        static JapaneseErrorMessageProvider() { }
        private JapaneseErrorMessageProvider() { }
        public static JapaneseErrorMessageProvider Instance { get { return _instance; } }

        public string ExportFileNotSpecified { get { return "ファイルを選択して下さい"; } }
        public string InvalidFilePath { get { return "無効なパスです"; } }
        public string ImportFileNotSpecified { get { return "インポートするファイルを選択して下さい"; } }
        public string ImportFileNotFound { get { return "インポートするファイルが存在しません"; } }
        public string NoDataToImport { get { return "インポートするデータがありません"; } }
        public string ImportColumnCountNotMatched { get { return "CSVの列数とテーブルの列数が異なります"; } }
        public string InvalidNumberOfSql { get { return "２つ以上のSQLがあります"; } }
        public string InvalidSql { get { return "無効なSQLです"; } }
        public string TableNameNotFound { get { return "テーブル名が取得できません"; } }
        public string InvalidColumnIncluded { get { return "無効な列が含まれています"; } }
        public string PrimaryKeyNotFound { get { return "主キー情報が取得できません"; } }
        public string PrimaryKeyNotIncluded { get { return "主キー列が含まれていません"; } }
    }
}
