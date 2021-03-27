using System.Collections.Generic;
using System.Linq;

namespace MasudaManager.Utility
{
    public class QueryResult : IEnumerable<string>
    {
        readonly Dictionary<int, string> _values = new Dictionary<int, string>();
        //public List<string> ValueList { get { return _values.Values.ToList(); } }
        public IEnumerable<string> Values { get { return _values.Values; } }

        public string this[int key]
        {
            get
            {
                return _values.GetValueOrDefault(key);
            }
            set
            {
                _values[key] = value;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _values.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
