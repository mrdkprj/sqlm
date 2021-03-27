using MasudaManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class ShowSearchGridEventArgs : EventArgs
    {
        public ShowSearchGridEventArgs(SearchViewRequestData searchViewRequestData)
        {
            this.SearchViewRequestData = searchViewRequestData;
        }

        public SearchViewRequestData SearchViewRequestData { get; set; }
    }
}
