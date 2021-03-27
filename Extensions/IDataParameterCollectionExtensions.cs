using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MasudaManager.Utility
{
    public static class IDataParameterCollectionExtensions
    {
        public static void AddRange(this IDataParameterCollection collection, IEnumerable<IDataParameter> paremeters)
        {
            foreach (var paremeter in paremeters)
            {
                collection.Add(paremeter);
            }
        }
    }
}
