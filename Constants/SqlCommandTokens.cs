using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class SqlCommandToken
    {
        public const string Alter = "Alter";
        public const string Analyze = "Analyze";
        public const string AssociateStatistics = "AssociateStatistics";
        public const string Audit = "Audit";
        public const string Call = "Call";
        public const string Comment = "Comment";
        public const string Commit = "Commit";
        public const string Create = "Create";
        public const string Delete = "Delete";
        public const string DisassociateStatistics = "DisassociateStatistics";
        public const string Drop = "Drop";
        public const string ExplainPlan = "ExplainPlan";
        public const string Flashback = "Flashback";
        public const string Grant = "Grant";
        public const string Insert = "Insert";
        public const string LockTable = "LockTable";
        public const string Merge = "Merge";
        public const string Noaudit = "Noaudit";
        public const string Purge = "Purge";
        public const string Rename = "Rename";
        public const string Revoke = "Revoke";
        public const string Rollback = "Rollback";
        public const string Savepoint = "Savepoint";
        public const string Select = "Select";
        public const string Truncate = "Truncate";
        public const string Update = "Update";

        public static readonly string[] Tokens = new string[]
        {
            Alter, Analyze, AssociateStatistics, Audit, Call,
            Comment, Commit, Create, Delete, DisassociateStatistics,
            Drop, ExplainPlan, Flashback, Grant, Insert, LockTable,
            Merge, Noaudit, Purge, Rename, Revoke, Rollback, Savepoint,
            Select, Truncate, Update
        };

        public static readonly string[] QueryCommandTokens = new string[]
        {
            Select
        };

        public static readonly string[] DmlCommandTokens = new string[]
        {
            Call, Delete, ExplainPlan, Insert, LockTable,
            Merge, Update
        };

        public static readonly string[] DdlCommandTokens = new string[]
        {
            Alter, Analyze, AssociateStatistics,
            Audit, Comment, Create, DisassociateStatistics,
            Drop, Flashback, Grant, Noaudit,
            Purge, Rename, Revoke, Truncate
        };

        public static readonly string[] TclCommandTokens = new string[]
        {
            Commit, Rollback, Savepoint
        };
    }
}
