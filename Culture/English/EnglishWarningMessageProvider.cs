using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    class EnglishWarningMessageProvider : IWarningMessageProvider
    {
        static EnglishWarningMessageProvider _instance = new EnglishWarningMessageProvider();
        static EnglishWarningMessageProvider() { }
        private EnglishWarningMessageProvider() { }
        public static EnglishWarningMessageProvider Instance { get { return _instance; } }

        public string EditResultKeyColumnWarningFormat { get { return "{0} \n Do you continue editing the data?"; } }
    }
}
