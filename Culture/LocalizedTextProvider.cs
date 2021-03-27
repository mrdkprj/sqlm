using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Culture;

namespace MasudaManager
{
    public static class LocalizedTextProvider
    {
        static ITextProvider _provider;

        static LocalizedTextProvider()
        {
            string language = System.Configuration.ConfigurationManager.AppSettings["Language"];
            switch (language)
            {
                case Constants.Language.Japanese:
                    _provider = JapaneseTextProvider.Instance;
                   break;
                case Constants.Language.English:
                    _provider = EnglishTextProvider.Instance;
                    break;
                default:
                    _provider = EnglishTextProvider.Instance;
                    break;
            }
        }

        public static string Language
        {
            get { return _provider.Language; }
        }

        public static IContextMenuTextProvider ContextMenu
        {
            get { return _provider.ContextMenu; }
        }

        public static IFormTextProvider Form
        {
            get { return _provider.Form; }
        }

        public static IMessageTextProvider Message
        {
            get { return _provider.Message; }
        }
    }
}
