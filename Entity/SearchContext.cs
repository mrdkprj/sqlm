using MasudaManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class SearchContext
    {
        public IEnumerable<SqlResult> SqlResults { get; set; }
        public Cell CurrentCell { get; set; }
    }
}
