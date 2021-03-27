using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public interface ISqlResultFormatter
    {
        IEnumerable<SqlResult> Format(IEnumerable<SqlResult> sourceList);
    }
}
