using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.DataAccess
{
    public interface IDataAccessConverter
    {
        Type GetBindableType(Type sourceType);
        Type GetBindableType(DbDataReader reader, int index);
        string GetString(object value);
        string GetString(DbDataReader reader, int index);
    }
}
