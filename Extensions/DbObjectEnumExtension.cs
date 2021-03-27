using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public static class DbObjectEnumExtension
    {
        readonly static string[] _tableDataColumnNames = { "Name", "Comment" };
        public static string DisplayName(this TableDataColumn dataName)
        {
            return _tableDataColumnNames[(int)dataName];
        }

        readonly static string[] _tableColumnDataColumnNames = { "ID", "Name", "Comment", "Type", "Length", "Nullable" };
        public static string DisplayName(this TableColumnDataColumn dataName)
        {
            return _tableColumnDataColumnNames[(int)dataName];
        }

        readonly static string[] _tableIndexDataColumnNames = { "Name", "No", "Column" };
        public static string DisplayName(this TableIndexDataColumn dataName)
        {
            return _tableIndexDataColumnNames[(int)dataName];
        }

        readonly static string[] _indexColumnDataColumnNames = { "No", "Column" };
        public static string DisplayName(this IndexColumnDataColumn dataName)
        {
            return _indexColumnDataColumnNames[(int)dataName];
        }

        readonly static string[] _tableConstraintDataColumnNames = { "Schema", "Name", "Type" };
        public static string DisplayName(this TableConstraintDataColumn dataName)
        {
            return _tableConstraintDataColumnNames[(int)dataName];
        }

        readonly static string[] _generalPropertyDataColumnNames = { "Property", "Value" };
        public static string DisplayName(this GeneralPropertyDataColumn dataName)
        {
            return _generalPropertyDataColumnNames[(int)dataName];
        }
    }
}
