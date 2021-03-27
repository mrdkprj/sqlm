using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MasudaManager.Utility;

namespace MasudaManager
{
    public class SqlTabModel : IModel
    {
        public SqlTabModel()
        {
            this.SearchContext = new SearchContext();
            this.SearchViewRequestData = new SearchViewRequestData();
        }

        public SearchContext SearchContext { get; set; }
        public SearchViewRequestData SearchViewRequestData { get; set; }

        public void ReleaseModel()
        {
        }
    }
}
