using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.Utility;
using MasudaManager.Utility.DbObject;

namespace MasudaManager.Utility
{
    public static class SqlResultFormatter
    {
        public static IEnumerable<SqlResult> Convert(DbObjectPropertyType propertyType, IEnumerable<SqlResult> sourceList)
        {
            return GetFormatter(propertyType).Format(sourceList);
        }

        static ISqlResultFormatter GetFormatter(DbObjectPropertyType propertyType)
        {
            switch (propertyType)
            {
                case DbObjectPropertyType.None:
                    return TableDataFormatter.Instance;
                case DbObjectPropertyType.TableColumn:
                    return TableColumnDataFormatter.Instance;
                case DbObjectPropertyType.TableIndex:
                    return TableIndexDataFormatter.Instance;
                case DbObjectPropertyType.TableConstraint:
                    return TableConstraintDataFormatter.Instance;
                case DbObjectPropertyType.IndexColumn:
                    return IndexColumnDataFormatter.Instance;
                case DbObjectPropertyType.GeneralProperty:
                    return GeneralPropertyDataFormatter.Instance;
            }

            throw new Exception("Cannot convert " + propertyType.ToString());
        }
    }
}
