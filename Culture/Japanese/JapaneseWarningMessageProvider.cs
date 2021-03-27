using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class JapaneseWarningMessageProvider : IWarningMessageProvider
    {
        static JapaneseWarningMessageProvider _instance = new JapaneseWarningMessageProvider();
        static JapaneseWarningMessageProvider() { }
        private JapaneseWarningMessageProvider() { }
        public static JapaneseWarningMessageProvider Instance { get { return _instance; } }

        public string EditResultKeyColumnWarningFormat { get { return "{0} \n データの編集を続行しますか？"; } }
    }
}
