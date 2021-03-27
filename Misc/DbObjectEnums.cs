using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public enum TableDataColumn
    {
        Name,
        Comment
    }

    public enum TableColumnDataColumn
    {
        ColumnID,
        ColumnName,
        Comment,
        DataType,
        DataLength,
        Nullable
    }

    public enum TableIndexDataColumn
    {
        IndexName,
        IndexNo,
        ColumnName
    }


    public enum IndexColumnDataColumn
    {
        IndexNo,
        ColumnName
    }

    public enum TableConstraintDataColumn
    {
        SchemaName,
        ConstraintName,
        ConstraintType
    }

    public enum GeneralPropertyDataColumn
    {
        Name,
        Value
    }
}
