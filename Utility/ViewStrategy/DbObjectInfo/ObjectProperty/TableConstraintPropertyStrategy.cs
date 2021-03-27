using MasudaManager.DataAccess;
using System.Collections.Generic;

namespace MasudaManager.Utility.DbObject
{
    public class TableConstraintPropertyStrategy : IDbObjectStrategy
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        IDbObjectStrategy _parent = null;
        IDbObjectStrategy _child = null;
        IDbCommandBuilder _builder = new DbCommandBuilder();

        public TableConstraintPropertyStrategy(IDbObjectStrategy parent, IDbObjectStrategy child)
        {
            _parent = parent;
            _child = child;
        }

        public IDbObjectStrategy ParentStrategy { get { return _parent; } }

        public IDbObjectStrategy ChildStrategy { get { return _child; } }

        public DbObjectType ObjectType { get { return _parent.ObjectType; } }

        public DbObjectPropertyType PropertyType { get { return DbObjectPropertyType.TableConstraint; } }

        public bool SupportClickInsert { get { return false; } }

        public int InsertableColumnIndex { get { return 0; } }

        public bool SupportFilter { get { return true; } }

        public int FilterableColumnIndex { get { return (int)TableConstraintDataColumn.ConstraintName; } }

        public bool SupportDisplayData { get { return false; } }

        public bool SupportCreateSql { get { return false; } }

        public bool SupportEditData { get { return false; } }

        public bool SupportExport { get { return false; } }

        public bool SupportImport { get { return false; } }

        public IDbCommandBuilder CommandBuilder
        {
            get
            {
                _builder.CreateCommand(_dataAccess.SqlLibrary.SqlTableConstraintPropertyInfo,
                    DbObjectDbCommandParameters.GetPropertyParemeterNames(_parent.ObjectType),
                    GetParamValue()); 
                return _builder;
            }
        }

        public int NameColumnIndex { get { return 0; } }

        public int KeyColumnIndex { get { return 0; } }

        public object CurrentKeyValue { get; set; }

        object[] GetParamValue()
        {
            return new object[] { _dataAccess.CurrentConnectionData.UserId.ToUpper(), _parent.CurrentKeyValue };
        }
    }
}
