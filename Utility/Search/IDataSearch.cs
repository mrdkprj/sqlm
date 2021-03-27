using System.Collections.Generic;

namespace MasudaManager.Utility
{
    public interface IDataSearch
    {
        Cell Search(string searchKey, SearchContext context, SearchDirection direction);
        Cell Search(string searchKey, SearchContext context, SearchDirection direction, SearchMode mode, SearchOptionFlags option = SearchOptionFlags.None);
        Cell Search(SearchBaseInfo searchRequestInfo);
    }
}
