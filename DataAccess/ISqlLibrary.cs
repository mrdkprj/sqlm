
namespace MasudaManager.DataAccess
{
    public interface ISqlLibrary
    {
        string SqlTableObjectInfo { get; }
        string SqlViewObjectInfo { get; }
        string SqlIndexObjectInfo { get; }
        string SqlPackageObjectInfo { get; }
        string SqlPackageBodyObjectInfo { get; }
        string SqlProcedureObjectInfo { get; }
        string SqlFunctionObjectInfo { get; }
        string SqlTypeObjectInfo { get; }
        string SqlTriggerObjectInfo { get; }
        string SqlSynonymObjectInfo { get; }
        string SqlSequenceObjectInfo { get; }
        string SqlTableColumnPropertyInfo { get; }
        string SqlTableIndexPropertyInfo { get; }
        string SqlTableConstraintPropertyInfo { get; }
        string SqlTablePropertyInfo { get; }
        string SqlViewPropertyInfo { get; }
        string SqlIndexPropertyInfo { get; }
        string SqlIndexColumnPropertyInfo { get; }
        string SqlPackagePropertyInfo { get; }
        string SqlPackageBodyPropertyInfo { get; }
        string SqlProcedurePropertyInfo { get; }
        string SqlFunctionPropertyInfo { get; }
        string SqlTypePropertyInfo { get; }
        string SqlTriggerPropertyInfo { get; }
        string SqlSynonymPropertyInfo { get; }
        string SqlSequencePropertyInfo { get; }
        string SqlRollback { get; }
        string SqlCommit { get; }

        string FormatSelectAllFromTable(string tableName);

        string FormatSelectTableNameFromSchema(string schemaName);

        string FormatSelectCountAllFromTable(string tableName);

        string SqlTableSource(string typename, string objectName);

        string SqlUserHelpTableInfo { get; }

        string SqlUserHelpColumnInfo { get; }
    }
}
