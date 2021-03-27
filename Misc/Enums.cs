using System;

namespace MasudaManager
{
    public enum RdbmsType
    {
        HiRDB,
        Orace
    }

    public enum WriteMode
    {
        Insert,
        Overwrite
    }

    public enum EditType
    {
        Delete,
        Insert,
        BulkInsert,
        Update,
        Remove,
        Edit
    }

    [Flags]
    public enum SqlType
    {
        Empty = 0,
        Query = 1,
        DDL = 2,
        DML = 4,
        TCL = 8,
        Invalid = 16
    }

    public enum SqlCommandType
    {
        Empty,
        Query,
        NonQuery,
        Invalid
    }

    public enum ObserverActionType
    {
        OnUpdate,
        OnComplete,
        OnError
    }

    public enum SearchDirection
    {
        Forward,
        Backward
    }

    public enum SearchMode
    {
        Partial,
        Prefix,
        Suffix,
        Exact
    }

    [Flags]
    public enum SearchOptionFlags
    {
        None = 0,
        SearchHeader = 1,
        CaseSensitive = 2,
        CloseDialog = 4
    }

    public enum CopyTargetType
    {
        Text,
        Header,
        TextWithHeader
    }

    public enum ExportImportFormat
    {
        CSV,
        SQL
    }

    public enum CsvHeaderOption
    {
        NoHeader,
        ColumnNameHeader
    }

    public enum DbObjectType
    {
        Empty,
        Table,
        View,
        Index,
        Package,
        PackageBody,
        Procedure,
        Function,
        Type,
        Trigger,
        Synonym,
        Sequence
    }

    [Flags]
    public enum DbObjectPropertyType
    {
        None = 0,
        TableColumn = 1,
        TableIndex = 2,
        TableConstraint = 4,
        IndexColumn = 8, 
        GeneralProperty = 16
    }

    public enum TabToolStripMode
    {
        None,
        InputText,
        Path
    }

    public enum SqlInputAssistantComplementMode
    {
        None,
        ObjectName,
        ColumnName,
        InlineViewColumn
    }

    public enum SqlTaskResultType
    {
        Nothing,
        DataFetched,
        DataAltered,
        DefinitionAltered,
    }

    public enum SqlTaskStatus
    {
        None,
        Initiated,
        Running,
        Complete,
        Error,
        Cancelled
    }

    public enum SqlTaskOnErrorActionType
    {
        None,
        Cancel,
        Continue,
        ComplyWithPreference
    }

    public enum EditInsertPositionType
    {
        AboveCurrentRow,
        CurrentRow,
        BelowCurrentRow
    }

    public enum CreateSqlStatementType
    {
        Select,
        SelectCount,
        Insert,
        Delete
    }

    public enum FileStatus
    {
        None,
        Loading,
        Changed,
        Saved
    }
}
