using MasudaManager.DataAccess;
using System.Collections.Generic;

namespace MasudaManager.Utility.DbObject
{
    public class IndexObjectStrategy : IDbObjectStrategy
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        IDbObjectStrategy _child;
        IDbCommandBuilder _builder = new DbCommandBuilder();

        static IndexObjectStrategy _instance = new IndexObjectStrategy();
        static IndexObjectStrategy() { }
        private IndexObjectStrategy()
        {
            _child =
                new GeneralPropertyStrategy(this,
                    new IndexColumnPropertyStrategy(this,
                        null));
        }
        public static IndexObjectStrategy Instance
        {
            get { return _instance; }
        }

        public IDbObjectStrategy ParentStrategy { get { return null; } }

        public IDbObjectStrategy ChildStrategy { get { return _child; } }

        public DbObjectType ObjectType { get { return DbObjectType.Index; } }

        public DbObjectPropertyType PropertyType { get { return DbObjectPropertyType.None; } }

        public bool SupportClickInsert { get { return true; } }

        public int InsertableColumnIndex { get { return (int)TableDataColumn.Name; } }

        public bool SupportFilter { get { return true; } }

        public int FilterableColumnIndex { get { return (int)TableDataColumn.Name; } }

        public bool SupportDisplayData { get { return false; } }

        public bool SupportCreateSql { get { return false; } }

        public bool SupportEditData { get { return false; } }

        public bool SupportExport { get { return false; } }

        public bool SupportImport { get { return false; } }

        public IDbCommandBuilder CommandBuilder
        {
            get
            {
                _builder.CreateCommand(_dataAccess.SqlLibrary.SqlIndexObjectInfo,
                    DbObjectDbCommandParameters.GetObjectParemeterNames(DbObjectType.Index),
                    GetParamValue());
                return _builder;
            }
        }

        public int NameColumnIndex { get { return 0; } }
        
        public int KeyColumnIndex { get { return 0; } }

        public object CurrentKeyValue { get; set; }

        object[] GetParamValue()
        {
            return new object[] { _dataAccess.CurrentConnectionData.UserId.ToUpper() };
        }

    }
}
