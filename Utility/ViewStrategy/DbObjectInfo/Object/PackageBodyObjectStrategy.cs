using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility.DbObject
{
    class PackageBodyObjectStrategy : IDbObjectStrategy
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        IDbObjectStrategy _child;
        IDbCommandBuilder _builder = new DbCommandBuilder(); 
        
        static PackageBodyObjectStrategy _instance = new PackageBodyObjectStrategy();
        static PackageBodyObjectStrategy() { }
        private PackageBodyObjectStrategy()
        {
            _child = new GeneralPropertyStrategy(this, null);
        }

        public static PackageBodyObjectStrategy Instance
        {
            get { return _instance; }
        }

        public IDbObjectStrategy ParentStrategy { get { return null; } }

        public IDbObjectStrategy ChildStrategy { get { return _child; } }

        public DbObjectType ObjectType { get { return DbObjectType.PackageBody; } }

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
                _builder.CreateCommand(_dataAccess.SqlLibrary.SqlPackageBodyObjectInfo,
                    DbObjectDbCommandParameters.GetObjectParemeterNames(DbObjectType.PackageBody),
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
