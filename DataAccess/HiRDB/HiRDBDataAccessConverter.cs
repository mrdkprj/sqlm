using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.Odbc;

namespace MasudaManager.DataAccess.HiRDB
{
    class HiRDBDataAccessConverter : IDataAccessConverter
    {
        static HiRDBDataAccessConverter _instance = null;
        private HiRDBDataAccessConverter() { }
        public static HiRDBDataAccessConverter GetInstance()
        {
            if (_instance == null)
                _instance = new HiRDBDataAccessConverter();
            return _instance;
        }

        public Type GetBindableType(Type sourceType)
        {
            return sourceType;
        }

        public string GetString(object value)
        {
            return value.ToStringOrNull();
        }

        public Type GetBindableType(DbDataReader reader, int index)
        {
            OdbcDataReader odbcReader = reader as OdbcDataReader;

            return odbcReader.GetFieldType(index);
        }

        public string GetString(DbDataReader reader, int index)
        {
            OdbcDataReader odbcReader = reader as OdbcDataReader;

            return odbcReader.GetValue(index).ToStringOrNull();
        }
    }
}