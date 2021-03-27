using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class GenericEventArgs<T> : EventArgs
    {
        public GenericEventArgs()
        {
            this.Data = default(T);
        }

        public GenericEventArgs(T data)
        {
            this.Data = data;
        }

        public T Data { get; protected set; }
    }
}

