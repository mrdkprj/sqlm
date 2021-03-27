using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Extensions
{
    public static class LookupExtensions
    {
            public static TValue GetValueOrDefault<TKey, TValue>(this ILookup<TKey, TValue> source, TKey key)
            {
                if (source == null || source.Count == 0)
                    return default(TValue);

                if (source.Contains(key))
                    return source[key].First();

                return default(TValue);
            }
        
    }
}
