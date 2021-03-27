using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class AddTabEventArgs : EventArgs
    {
        public AddTabEventArgs() { }

        public AddTabEventArgs(Guid guid)
        {
            this.Guid = guid;
        }

        public AddTabEventArgs(object tag)
        {
            this.Tag = tag;
        }

        public Guid Guid { get; set; }
        public object Tag { get; set; }
    }
}

