using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace MasudaManager
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            if (source == null || source.Count == 0)
                return default(TValue);

            TValue result;
            if (source.TryGetValue(key, out result))
                return result;
            return default(TValue);
        }
    }
}
