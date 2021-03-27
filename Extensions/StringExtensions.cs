using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public static class StringExtensions
    {
        public static bool Contains(this String str, String substring, StringComparison comp)
        {
            if (substring == null)
                throw new ArgumentNullException();
            else if (!Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException("Argument not a member of StringComparison");

            return str.IndexOf(substring, comp) >= 0;
        }
        
        public static string ToStringOrEmpty(this object value)
        {
            return (value ?? string.Empty).ToString();
        }

        public static string ToStringOrNull(this object value)
        {
            return IsNullOrDBNull(value) ? null : value.ToString();
        }

        static bool IsNullOrDBNull(object value)
        {
            if (value == null)
                return true;

            if (value == DBNull.Value)
                return true;

            //if (value is string && String.IsNullOrEmpty(value.ToString()))
            //    return true;

            return false;
        }
    }
}
