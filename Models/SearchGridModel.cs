using MasudaManager.Utility;
using System.Collections.Generic;

namespace MasudaManager
{
    public class SearchGridModel : IModel
    {
        public SearchGridModel()
        {
            this.SearchContext = new SearchContext();
        }

        public SearchContext SearchContext { get; set; }

        public void ReleaseModel()
        {
        }
    }
}
