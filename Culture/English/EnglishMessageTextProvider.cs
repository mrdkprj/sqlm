using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class EnglishMessageTextProvider : IMessageTextProvider
    {
        static EnglishMessageTextProvider _instance = new EnglishMessageTextProvider();
        static EnglishMessageTextProvider() { }
        private EnglishMessageTextProvider() { }
        public static EnglishMessageTextProvider Instance { get { return _instance; } }
        
        public IInfoMessageProvider Info
        {
            get { return EnglishInfoMessageProvider.Instance; }
        }

        public IErrorMessageProvider Error
        {
            get { return EnglishErrorMessageProvider.Instance; }
        }

        public IWarningMessageProvider Warning
        {
            get { return EnglishWarningMessageProvider.Instance; }
        }
    }
}
