using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasudaManager.DataAccess;

namespace MasudaManager.Utility.DbObject
{
    public static class DbObjectDbCommandParameters
    {
        static IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        readonly static string _objectParameterName = "SCHEMA_NAME";
        readonly static string[] _objectParemeterNames = { _objectParameterName };

        readonly static string _tablePropertyParemeterName = "OBJECT_NAME";
        readonly static string[] _tablePropertyParemeterNames = { _objectParameterName, _tablePropertyParemeterName };

        readonly static string _indexPropertyParemeterName = "INDEX_ID";
        readonly static string[] _indexPropertyParemeterNames = { _objectParameterName, _indexPropertyParemeterName };

        readonly static string _generalPropertyParemeterName = "OBJECT_NAME";
        readonly static string[] _generalPropertyParemeterNames = { _objectParameterName, _generalPropertyParemeterName };
        
        public static IEnumerable<string> GetObjectParemeterNames(DbObjectType objectType)
        {
            switch (objectType)
            {
                case DbObjectType.Table:
                case DbObjectType.View:
                case DbObjectType.Index:
                case DbObjectType.Package:
                case DbObjectType.PackageBody:
                case DbObjectType.Procedure:
                case DbObjectType.Function:
                case DbObjectType.Type:
                case DbObjectType.Trigger:
                case DbObjectType.Synonym:
                case DbObjectType.Sequence:
                    return _objectParemeterNames;
            }

            throw new ArgumentException("No matched DbObjectType");
        }

        public static IEnumerable<string> GetPropertyParemeterNames(DbObjectType objectType)
        {
            switch (objectType)
            {
                case DbObjectType.Table:
                case DbObjectType.View:
                    return _tablePropertyParemeterNames;
                case DbObjectType.Index:
                    return _indexPropertyParemeterNames;
                case DbObjectType.Package:
                case DbObjectType.PackageBody:
                case DbObjectType.Procedure:
                case DbObjectType.Function:
                case DbObjectType.Type:
                case DbObjectType.Trigger:
                case DbObjectType.Synonym:
                case DbObjectType.Sequence:
                    return _generalPropertyParemeterNames;
            }

            throw new ArgumentException("No matched DbObjectType");
        }

        public static string GetGeneralPropertySql(DbObjectType objectType)
        {
            switch (objectType)
            {
                case DbObjectType.Table:
                    return _dataAccess.SqlLibrary.SqlTablePropertyInfo;
                case DbObjectType.View:
                    return _dataAccess.SqlLibrary.SqlViewPropertyInfo;
                case DbObjectType.Index:
                    return _dataAccess.SqlLibrary.SqlIndexPropertyInfo;
                case DbObjectType.Package:
                    return _dataAccess.SqlLibrary.SqlPackagePropertyInfo;
                case DbObjectType.PackageBody:
                    return _dataAccess.SqlLibrary.SqlPackageBodyPropertyInfo;
                case DbObjectType.Procedure:
                    return _dataAccess.SqlLibrary.SqlProcedurePropertyInfo;
                case DbObjectType.Function:
                    return _dataAccess.SqlLibrary.SqlFunctionPropertyInfo;
                case DbObjectType.Type:
                    return _dataAccess.SqlLibrary.SqlTypePropertyInfo;
                case DbObjectType.Trigger:
                    return _dataAccess.SqlLibrary.SqlTriggerPropertyInfo;
                case DbObjectType.Synonym:
                    return _dataAccess.SqlLibrary.SqlSynonymPropertyInfo;
                case DbObjectType.Sequence:
                    return _dataAccess.SqlLibrary.SqlSequencePropertyInfo;
            }

            throw new ArgumentException("No matched DbObjectType");
        }
    }
}
