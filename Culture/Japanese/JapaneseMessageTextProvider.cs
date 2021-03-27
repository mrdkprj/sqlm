using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class JapaneseMessageTextProvider : IMessageTextProvider
    {
        static JapaneseMessageTextProvider _instance = new JapaneseMessageTextProvider();
        static JapaneseMessageTextProvider() { }
        private JapaneseMessageTextProvider() { }
        public static JapaneseMessageTextProvider Instance { get { return _instance; } }
        
        public IInfoMessageProvider Info
        {
            get { return JapaneseInfoMessageProvider.Instance; }
        }

        public IErrorMessageProvider Error
        {
            get { return JapaneseErrorMessageProvider.Instance; }
        }

        public IWarningMessageProvider Warning
        {
            get { return JapaneseWarningMessageProvider.Instance; }
        }
    }
}
