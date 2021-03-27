using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasudaManager.Utility
{
    public class SearchViewRequestData
    {
        public IWin32Window Owner { get; set; }
        public SearchDirection SearchDirection { get; set; }
        public SearchGridCallBackActions CallBackActions { get; set; }
    }
}
