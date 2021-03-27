using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.DataAccess.OracleDatabase
{
    class OracleSqlLibrary : ISqlLibrary
    {
        public string FormatSelectAllFromTable(string tablename)
        {
            return String.Format("SELECT * FROM {0}", tablename);
        }

        public string FormatSelectTableNameFromSchema(string schemaName)
        {
            return String.Format("select TABLE_NAME as \"Name\" from all_tables where OWNER = '{0}' order by TABLE_NAME", schemaName);
        }

        public string FormatSelectCountAllFromTable(string tableName)
        {
            return String.Format("select count(*) from {0}", tableName);
        }

        public string SqlTableObjectInfo
        {
            get { return "select t.TABLE_NAME as \"Name\", c.COMMENTS as \"Comment\" from all_tables t join all_tab_comments c on (t.TABLE_NAME = c.TABLE_NAME and t.OWNER = c.OWNER) where t.OWNER = :SCHEMA_NAME order by t.TABLE_NAME"; }
        }

        public string SqlViewObjectInfo
        {
            get { return "select a.view_name as \"Name\", c.COMMENTS as \"Comment\" from ALL_VIEWS a join all_tab_comments c on (a.owner = c.owner and a.view_name = c.table_name) where a.OWNER = :SCHEMA_NAME order by a.view_name"; }
        }

        public string SqlIndexObjectInfo
        {
            get { return "select index_name as \"Name\", ''  as \"Description\"from all_indexes t where owner = :SCHEMA_NAME order by index_name"; }
        }

        public string SqlPackageObjectInfo
        {
            get { return "select distinct p.OBJECT_NAME, o.STATUS, o.CREATED, o.LAST_DDL_TIME from all_procedures p join all_objects o on (p.OWNER = o.OWNER and p.OBJECT_NAME = o.OBJECT_NAME) where p.OWNER = :SCHEMA_NAME and p.OBJECT_TYPE = 'PACKAGE' order by p.OBJECT_NAME"; }
        }

        public string SqlPackageBodyObjectInfo
        {
            get { return "select distinct p.OBJECT_NAME, o.STATUS, o.CREATED, o.LAST_DDL_TIME from all_procedures p join all_objects o on (p.OWNER = o.OWNER and p.OBJECT_NAME = o.OBJECT_NAME) where p.OWNER = :SCHEMA_NAME and p.OBJECT_TYPE = 'PACKAGE' order by p.OBJECT_NAME"; }
        }

        public string SqlProcedureObjectInfo
        {
            get { return "select distinct p.OBJECT_NAME, o.STATUS, o.CREATED, o.LAST_DDL_TIME from all_procedures p join all_objects o on (p.OWNER = o.OWNER and p.OBJECT_NAME = o.OBJECT_NAME) where p.OWNER = :SCHEMA_NAME and p.OBJECT_TYPE = 'PROCEDURE' order by p.OBJECT_NAME"; }
        }

        public string SqlFunctionObjectInfo
        {
            get { return "select distinct p.OBJECT_NAME, o.STATUS, o.CREATED, o.LAST_DDL_TIME from all_procedures p join all_objects o on (p.OWNER = o.OWNER and p.OBJECT_NAME = o.OBJECT_NAME) where p.OWNER = :SCHEMA_NAME and p.OBJECT_TYPE = 'FUNCTION' order by p.OBJECT_NAME"; }
        }

        public string SqlTypeObjectInfo
        {
            get { return "select p.TYPE_NAME, o.STATUS, o.CREATED, o.LAST_DDL_TIME from all_types p join all_objects o on (p.OWNER = o.OWNER and p.TYPE_NAME = o.OBJECT_NAME) where p.OWNER = :SCHEMA_NAME and o.OBJECT_TYPE = 'TYPE' order by p.TYPE_NAME"; }
        }

        public string SqlTriggerObjectInfo
        {
            get { return "select p.TRIGGER_NAME, o.STATUS, o.CREATED, o.LAST_DDL_TIME from all_triggers p join all_objects o on (p.OWNER = o.OWNER and p.TRIGGER_NAME = o.OBJECT_NAME) where p.OWNER = :SCHEMA_NAME and o.OBJECT_TYPE = 'TRIGGER' order by p.TRIGGER_NAME"; }
        }

        public string SqlSynonymObjectInfo
        {
            get { return "select p.SYNONYM_NAME, o.STATUS, o.CREATED, o.LAST_DDL_TIME from all_synonyms p join all_objects o on (p.OWNER = o.OWNER and p.SYNONYM_NAME = o.OBJECT_NAME) where p.OWNER = :SCHEMA_NAME and o.OBJECT_TYPE = 'SYNONYM' order by p.SYNONYM_NAME"; }
        }

        public string SqlSequenceObjectInfo
        {
            get { return "select p.SEQUENCE_NAME, o.STATUS, o.CREATED, o.LAST_DDL_TIME from all_sequences p join all_objects o on (p.SEQUENCE_OWNER = o.OWNER and p.SEQUENCE_NAME = o.OBJECT_NAME) where p.SEQUENCE_OWNER = :SCHEMA_NAME and o.OBJECT_TYPE = 'SEQUENCE' order by p.SEQUENCE_NAME"; }
        }

        // property
         public string SqlTableColumnPropertyInfo
        {
            get { return "select t.COLUMN_ID as \"ID\", t.COLUMN_NAME as \"Name\", c.COMMENTS as \"Comment\", t.DATA_TYPE as \"Type\", decode(t.CHAR_USED, null, t.DATA_PRECISION, t.CHAR_LENGTH), t.NULLABLE from all_tab_columns t join all_col_comments c on (t.TABLE_NAME = c.TABLE_NAME and t.COLUMN_NAME = c.COLUMN_NAME) where t.OWNER = :SCHEMA_NAME and t.TABLE_NAME = :OBJECT_NAME order by t.COLUMN_ID"; }
        }
        
        public string SqlTableIndexPropertyInfo
        {
            get
            {
                return "SELECT INDEX_NAME as \"Name\", COLUMN_POSITION as \"No\", COLUMN_NAME  as \"Colmn Name\" FROM ALL_IND_COLUMNS WHERE TABLE_OWNER = :SCHEMA_NAME and TABLE_NAME = :OBJECT_NAME ORDER BY INDEX_NAME, COLUMN_POSITION";
            }
        }

        public string SqlTableConstraintPropertyInfo
        {
            get
            {
                return "SELECT OWNER, CONSTRAINT_NAME as \"Name\", CONSTRAINT_TYPE  as \"Type\" FROM ALL_CONSTRAINTS WHERE OWNER = :SCHEMA_NAME and TABLE_NAME = :OBJECT_NAME ORDER BY CONSTRAINT_NAME";
            }
        }

        public string SqlTablePropertyInfo
        {
            get
            {
                return "select * from all_tables where owner = :SCHEMA_NAME and table_name = :OBJECT_NAME";
            }
        }

        public string SqlViewPropertyInfo
        {
            get
            {
                return "select owner, view_name from ALL_VIEWS where OWNER = :SCHEMA_NAME and VIEW_NAME = :OBJECT_NAME";
            }
        }

        public string SqlIndexPropertyInfo
        {
            get { return "select * from all_indexes t where owner = :SCHEMA_NAME and index_name = :OBJECT_NAME"; }
        }

        public string SqlIndexColumnPropertyInfo
        {
            get { return "select column_position, column_name from all_ind_columns t where index_owner = :SCHEMA_NAME and index_name = :OBJECT_NAME order by column_position"; }
        }

        public string SqlPackagePropertyInfo
        {
            get { return "select * from all_procedures p where p.OWNER = :SCHEMA_NAME and p.OBJECT_NAME = :OBJECT_NAME and p.OBJECT_TYPE = 'PACKAGE'"; }
        }

        public string SqlPackageBodyPropertyInfo
        {
            get { return "select * from all_procedures p where p.OWNER = :SCHEMA_NAME and p.OBJECT_NAME = :OBJECT_NAME and p.OBJECT_TYPE = 'PACKAGE'"; }
        }

        public string SqlProcedurePropertyInfo
        {
            get { return "select * from all_procedures p where p.OWNER = :SCHEMA_NAME and p.OBJECT_NAME = :OBJECT_NAME and p.OBJECT_TYPE = 'PROCEDURE'"; }
        }

        public string SqlFunctionPropertyInfo
        {
            get { return "select * from all_procedures p where p.OWNER = :SCHEMA_NAME and p.OBJECT_NAME = :OBJECT_NAME and p.OBJECT_TYPE = 'FUNCTION'"; }
        }

        public string SqlTypePropertyInfo
        {
            get { return "select * from all_types t where t.OWNER = :SCHEMA_NAME and t.TYPE_NAME = :OBJECT_NAME"; }
        }

        public string SqlTriggerPropertyInfo
        {
            get { return "select * from all_triggers t where t.OWNER = :SCHEMA_NAME and t.TRIGGER_NAME = :OBJECT_NAME"; }
        }

        public string SqlSynonymPropertyInfo
        {
            get { return "select * from all_synonyms s where s.OWNER = :SCHEMA_NAME and s.SYNONYM_NAME = :OBJECT_NAME"; }
        }

        public string SqlSequencePropertyInfo
        {
            get { return "select SEQUENCE_OWNER, MIN_VALUE, MAX_VALUE, INCREMENT_BY, CYCLE_FLAG, ORDER_FLAG, CACHE_SIZE, LAST_NUMBER from ALL_SEQUENCES where SEQUENCE_OWNER = :SCHEMA_NAME and SEQUENCE_NAME = :OBJECT_NAME order by SEQUENCE_NAME"; }
        }

        public string SqlRollback
        {
            get
            {
                return "Rollback";
            }
        }

        public string SqlCommit
        {
            get
            {
                return "Commit";
            }
        }

        public string SqlTableSource(string typename, string objectname)
        {
            return "select dbms_metadata.get_ddl('" + typename.ToUpper() + "','" + objectname.ToUpper() + "') from dual";
        }

        public string SqlUserHelpTableInfo
        {
            get
            {
                return "select OBJECT_NAME as \"Name\", OBJECT_TYPE as \"Type\" from ALL_OBJECTS where OWNER = :SCHEMA_NAME order by OBJECT_NAME";
            }
        }

        public string SqlUserHelpColumnInfo
        {
            get
            {
                return "select COLUMN_NAME as \"Name\", DATA_TYPE || '(' || decode(CHAR_USED, null, DATA_PRECISION, CHAR_LENGTH) || ')' as \"Size\" from all_tab_columns where OWNER in ('SYS', :SCHEMA_NAME) and TABLE_NAME = :TABLE_NAME order by COLUMN_ID";
            }
        }
    }
}
