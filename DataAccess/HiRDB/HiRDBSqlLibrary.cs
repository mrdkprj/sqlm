using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.DataAccess.HiRDB
{
    public class HiRDBSqlLibrary : ISqlLibrary
    {
        #region Object
        public string SqlTableObjectInfo
        {
            get
            {
                return "select TABLE_NAME, TABLE_COMMENT from MASTER.SQL_TABLES where TABLE_SCHEMA = ? and TABLE_TYPE = 'BASE TABLE' order by TABLE_NAME";
            }
        }

        public string SqlViewObjectInfo
        {
            get
            {
                return "select TABLE_NAME, TABLE_COMMENT from MASTER.SQL_TABLES where TABLE_SCHEMA = ? and TABLE_TYPE like '%VIEW%' order by TABLE_NAME";
            }
        }

        public string SqlIndexObjectInfo
        {
            get
            {
                return "select INDEX_NAME, '' from MASTER.SQL_INDEXES where TABLE_SCHEMA = ? order by INDEX_NAME";
            }
        }

        public string SqlPackageObjectInfo
        {
            get
            {
                return "select ROUTINE_NAME, case when ROUTINE_VALID = 'Y' then 'VALID' when ROUTINE_VALID = 'N' then 'INVALID' end, CREATE_TIME, ALTER_TIME from master.sql_routines where ROUTINE_SCHEMA = ? and ROUTINE_TYPE = 'P' order by ROUTINE_NAME";
            }
        }

        public string SqlPackageBodyObjectInfo
        {
            get
            {
                return "select ROUTINE_NAME, case when ROUTINE_VALID = 'Y' then 'VALID' when ROUTINE_VALID = 'N' then 'INVALID' end, CREATE_TIME, ALTER_TIME from master.sql_routines where ROUTINE_SCHEMA = ? and ROUTINE_TYPE = 'P' order by ROUTINE_NAME";
            }
        }

        public string SqlProcedureObjectInfo
        {
            get
            {
                return "select ROUTINE_NAME, case when ROUTINE_VALID = 'Y' then 'VALID' when ROUTINE_VALID = 'N' then 'INVALID' end, CREATE_TIME, ALTER_TIME from master.sql_routines where ROUTINE_SCHEMA = ? and ROUTINE_TYPE = 'P' order by ROUTINE_NAME";
            }
        }

        public string SqlFunctionObjectInfo
        {
            get
            {
                return "select ROUTINE_NAME, case when ROUTINE_VALID = 'Y' then 'VALID' when ROUTINE_VALID = 'N' then 'INVALID' end, CREATE_TIME, ALTER_TIME from master.sql_routines where ROUTINE_SCHEMA = ? and ROUTINE_TYPE = 'F' order by ROUTINE_NAME";
            }
        }

        public string SqlTypeObjectInfo
        {
            get
            {
                return "select TYPE_NAME, TYPE_ID, CREATE_TIME, '' from master.sql_datatypes where TYPE_SCHEMA = ? order by TYPE_NAME";
            }
        }

        public string SqlTriggerObjectInfo
        {
            get
            {
                return "select TRIGGER_NAME, case when TRIGGER_VALID = 'Y' then 'VALID' when TRIGGER_VALID = 'N' then 'INVALID' end, CREATE_TIME, ALTER_TIME from master.sql_triggers where TRIGGER_SCHEMA = ? order by TRIGGER_NAME";
            }
        }

        public string SqlSynonymObjectInfo
        {
            get
            {
                return "select ALIAS_NAME, ALIAS_TYPE, '', '' from master.sql_aliases where ALIAS_SCHEMA = ? order by ALIAS_NAME";
            }
        }
  
        public string SqlSequenceObjectInfo
        {
            get
            {
                return "select SEQUENCE_NAME, SEQUENCE_ID, CREATE_TIME, ALTER_TIME from master.sql_sequences where SEQUENCE_SCHEMA = ? order by SEQUENCE_NAME";
            }
        }
        #endregion

        #region Property
        public string SqlTableColumnPropertyInfo
        {
            get
            {
                return "select COLUMN_ID, COLUMN_NAME, COLUMN_COMMENT, rtrim(DATA_TYPE), DATA_LENGTH, case IS_NULLABLE when 'YES' then 'Yes' else 'No' end from MASTER.SQL_COLUMNS where TABLE_SCHEMA = ? and TABLE_NAME = ? order by COLUMN_ID";
            }
        }

        public string SqlTableIndexPropertyInfo
        {
            get
            {
                return "select INDEX_NAME,INDEX_ORDER,COLUMN_NAME from MASTER.SQL_INDEX_COLINF where TABLE_SCHEMA = ? and TABLE_NAME = ? order by INDEX_NAME, INDEX_ORDER";
            }
        }

        public string SqlTableConstraintPropertyInfo
        {
            get
            {
                return "select CONSTRAINT_SCHEMA, CONSTRAINT_NAME, CONSTRAINT_TYPE from MASTER.SQL_TABLE_CONSTRAINTS where TABLE_NAME = ? order by CONSTRAINT_NAME";
            }
        }

        public string SqlTablePropertyInfo
        {
            get
            {
                return "select * from MASTER.SQL_TABLES T WHERE T.TABLE_SCHEMA = ? AND T.TABLE_NAME = ?";
            }
        }
        public string SqlViewPropertyInfo
        {
            get
            {
                return "select BASE_OWNER, BASE_TABLE_NAME from MASTER.SQL_VIEW_TABLE_USAGE where VIEW_SCHEMA = ? and VIEW_NAME = ?";
            }
        }

        public string SqlIndexPropertyInfo
        {
            get
            {
                return "select TABLE_NAME, UNIQUE_TYPE, PRIMARY_KEY, CREATE_TIME, RDAREA_NAME from MASTER.SQL_INDEXES where TABLE_SCHEMA = ? and INDEX_NAME = ?";
            }
        }

        public string SqlIndexColumnPropertyInfo
        {
            get
            {
                return "select INDEX_ORDER, COLUMN_NAME from MASTER.SQL_INDEX_COLINF where TABLE_SCHEMA = ? and INDEX_NAME = ? order by INDEX_ORDER";
            }
        }

        public string SqlPackagePropertyInfo
        {
            get
            {
                return "select * from master.sql_routines where ROUTINE_SCHEMA = ? and ROUTINE_NAME = ? and ROUTINE_TYPE = 'P'";
            }
        }

        public string SqlPackageBodyPropertyInfo
        {
            get
            {
                return "select * from master.sql_routines where ROUTINE_SCHEMA = ? and ROUTINE_NAME = ? and ROUTINE_TYPE = 'P'";
            }
        }

        public string SqlProcedurePropertyInfo
        {
            get
            {
                return "select * from master.sql_routines where ROUTINE_SCHEMA = ? and ROUTINE_NAME = ? and ROUTINE_TYPE = 'P'";
            }
        }

        public string SqlFunctionPropertyInfo
        {
            get
            {
                return "select * from master.sql_routines where ROUTINE_SCHEMA = ? and ROUTINE_NAME = ? and ROUTINE_TYPE = 'F'";
            }
        }

        public string SqlTypePropertyInfo
        {
            get
            {
                return "select * from master.sql_datatypes where TYPE_SCHEMA = ? and TYPE_NAME = ?";
            }
        }

        public string SqlTriggerPropertyInfo
        {
            get
            {
                return "select * from master.sql_triggers where TRIGGER_SCHEMA = ? and TRIGGER_NAME = ?";
            }
        }

        public string SqlSynonymPropertyInfo
        {
            get
            {
                return "select * from master.sql_aliases where ALIAS_SCHEMA = ? and ALIAS_NAME = ?";
            }
        }

        public string SqlSequencePropertyInfo
        {
            get
            {
                return "select * from master.sql_sequences where SEQUENCE_SCHEMA = ? and SEQUENCE_NAME = ?";
            }
        }
        #endregion

        #region DCL
        public string SqlRollback
        {
            get { return "Rollback"; }
        }

        public string SqlCommit
        {
            get { return "Commit"; }
        }
        #endregion

        public string FormatSelectAllFromTable(string tablename)
        {
            return String.Format("SELECT * FROM {0}", tablename);
        }

        public string FormatSelectTableNameFromSchema(string schemaName)
        {
            return String.Format("select TABLE_NAME from MASTER.SQL_TABLES where TABLE_SCHEMA = '{0}' order by TABLE_NAME", schemaName); 
        }

        public string FormatSelectCountAllFromTable(string tableName)
        {
            return String.Format("select count(*) from {0}", tableName);
        }

        public string SqlTableSource(string typename, string objectname)
        {
            return string.Empty;
        }

        public string SqlUserHelpTableInfo
        {
            get
            {
                return "select TABLE_NAME as \"Name\", TABLE_TYPE as \"Type\" from MASTER.SQL_TABLES where TABLE_SCHEMA = ? order by TABLE_NAME";
            }
        }

        public string SqlUserHelpColumnInfo
        {
            get
            {
                return "select COLUMN_NAME as \"Name\", rtrim(DATA_TYPE) || DATA_LENGTH as \"Size\" from MASTER.SQL_COLUMNS where TABLE_SCHEMA in ('MASTER', ?) and TABLE_NAME = ? order by COLUMN_ID";
            }
        }
    }
}
