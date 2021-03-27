using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Culture
{
    public interface IMessageTextProvider
    {
        IInfoMessageProvider Info { get; }
        IErrorMessageProvider Error { get; }
        IWarningMessageProvider Warning { get; }
    }
}
