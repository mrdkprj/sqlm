using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class SearchBaseInfo
    {
        public string SearchKey { get; set; }
        public SearchContext Context { get; set; }
        public SearchDirection Direction { get; set; }
        public SearchMode Mode { get; set; }
        public SearchOptionFlags Options { get; set; }
    }
}
