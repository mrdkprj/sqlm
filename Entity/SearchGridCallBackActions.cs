using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasudaManager.Utility
{
    public class SearchGridCallBackActions
    {
        Action _onSearchContextRequest = null;
        Action<Cell> _onSearchComplete = null;

        public Action OnSearchContextRequest
        {
            get { return _onSearchContextRequest; }
        }

        public Action<Cell> OnSearchComplete
        {
            get { return _onSearchComplete; }
        }

        public SearchGridCallBackActions(Action onSearchContextRequest, Action<Cell> onSearchComplete)
        {
            _onSearchContextRequest = onSearchContextRequest;
            _onSearchComplete = onSearchComplete;
        }
    }
}
