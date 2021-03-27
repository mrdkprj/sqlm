using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility.DbObject
{
    class NullPropertyStrategy : IDbObjectStrategy
    {
        static NullPropertyStrategy _instance = new NullPropertyStrategy();
        static NullPropertyStrategy() { }
        private NullPropertyStrategy() { }
        public static NullPropertyStrategy Instance
        {
            get { return _instance; }
        }

        public IDbObjectStrategy ParentStrategy { get { return NullObjectStrategy.Instance; } }

        public IDbObjectStrategy ChildStrategy { get { return null; } }

        public DbObjectType ObjectType { get { return DbObjectType.Empty; } }

        public DbObjectPropertyType PropertyType { get { return DbObjectPropertyType.GeneralProperty; } }

        public IDbCommandBuilder CommandBuilder { get { return null; } }

        public int NameColumnIndex { get { return 0; } }

        public int KeyColumnIndex { get { return 0; } }

        public object CurrentKeyValue { get; set; }

        public bool SupportClickInsert { get { return false; } }

        public int InsertableColumnIndex { get { return 0; } }

        public bool SupportFilter { get { return false; } }

        public int FilterableColumnIndex { get { return 0; } }

        public bool SupportDisplayData { get { return false; } }

        public bool SupportCreateSql { get { return false; } }

        public bool SupportEditData { get { return false; } }

        public bool SupportExport { get { return false; } }

        public bool SupportImport { get { return false; } }

    }
}
