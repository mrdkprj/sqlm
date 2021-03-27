using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public interface IDbObjectStrategy
    {
        IDbObjectStrategy ParentStrategy { get; }

        IDbObjectStrategy ChildStrategy { get; }

        DbObjectType ObjectType { get; }

        DbObjectPropertyType PropertyType { get; }

        bool SupportClickInsert { get; }

        int InsertableColumnIndex { get; }

        bool SupportFilter { get; }

        int FilterableColumnIndex { get; }

        bool SupportDisplayData { get; }

        bool SupportCreateSql { get; }

        bool SupportEditData { get; }

        bool SupportExport { get; }

        bool SupportImport { get; }
        
        IDbCommandBuilder CommandBuilder { get; }

        int NameColumnIndex { get; }

        int KeyColumnIndex { get; }

        object CurrentKeyValue { get; set; }
    }
}
