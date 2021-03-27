using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public static class ListExtensions
    {
        public static List<T> LazyInitialize<T>(this List<T> list)
        {
            if (list == null)
                list = new List<T>();
            return list;
        }
    }
}
