using System.Collections.Generic;
using System.Data;

namespace MasudaManager.Utility
{
    public interface IDbCommandBuilder : IEnumerable<DbCommandSet>
    {
        DbCommandSet this[int index] { get; set; }
        int Count { get; }
        void Add(DbCommandSet commandSet);
        void AddRange(IEnumerable<DbCommandSet> commandSets);
        void Clear();
        DbCommandSet Consume();

        DbCommandSet CreateCommand(string sql);
        DbCommandSet CreateCommand(string sql, string parameterName, object parameterValue);
        DbCommandSet CreateCommand(string sql, IEnumerable<string> parameterNames, IEnumerable<object> parameterValues);
        DbCommandSet CreateCommand(ParseContext context);
        DbCommandSet CreateCommand(ParseContext context, string parameterName, object parameterValue);
        DbCommandSet CreateCommand(ParseContext context, IEnumerable<string> parameterNames, IEnumerable<object> parameterValues);
        DbCommandSet CreateCommand(IDbCommand dbCommand);
    }
}
