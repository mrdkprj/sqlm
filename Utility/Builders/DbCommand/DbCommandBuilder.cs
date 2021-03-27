using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MasudaManager.DataAccess;
using MasudaManager.Utility;

namespace MasudaManager.Utility
{
    public class DbCommandBuilder : IDbCommandBuilder
    {
        Queue<DbCommandSet> _commandList = new Queue<DbCommandSet>();
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        DbCommandBuilderUtil _builderUtil = new DbCommandBuilderUtil();

        public DbCommandBuilder()
        {
        }

        public DbCommandBuilder(string sql)
        {
            CreateCommand(sql);
        }

        public DbCommandBuilder(IEnumerable<DbCommandSet> commandSets)
        {
            _commandList = new Queue<DbCommandSet>(commandSets);
        }

        protected Queue<DbCommandSet> CommandList
        {
            get { return _commandList; }
            set { _commandList = value; }
        }

        protected IDataAccess DataAccess { get { return _dataAccess; } }

        public DbCommandSet this[int index]
        {
            get { return _commandList.ToList()[index]; }
            set { _commandList.ToList()[index] = value; }
        }

        public int Count { get { return _commandList.Count; } }

        public void Add(DbCommandSet commandSet)
        {
            _commandList.Enqueue(commandSet);
        }

        public void AddRange(IEnumerable<DbCommandSet> commandSets)
        {
            _commandList = new Queue<DbCommandSet>(commandSets);
        }

        public void Clear()
        {
            _commandList.Clear();
        }

        public DbCommandSet Consume()
        {
            if (_commandList.Count > 0)
                return _commandList.Dequeue();
            else
                return null;
        }

        public virtual DbCommandSet CreateCommand(string sql)
        {
            var commandSet = MakeDbCommandSetFromSql(sql);
            _commandList.Enqueue(commandSet);
            return commandSet;
        }

        public DbCommandSet CreateCommand(string sql, string parameterName, object parameterValue)
        {
            var commandSet = MakeDbCommandSetFromSql(sql);
            var parameter = GetDataParameter(commandSet.DbCommand, parameterName, parameterValue);
            commandSet.DbCommand.Parameters.Add(parameter);
            _commandList.Enqueue(commandSet);
            return commandSet;
        }

        public virtual DbCommandSet CreateCommand(string sql, IEnumerable<string> parameterNames, IEnumerable<object> parameterValues)
        {
            var commandSet = MakeDbCommandSetFromSql(sql);
            var parameters  = GetDataParameters(commandSet.DbCommand, parameterNames, parameterValues);
            commandSet.DbCommand.Parameters.AddRange(parameters);
            _commandList.Enqueue(commandSet);
            return commandSet;
        }

        public virtual DbCommandSet CreateCommand(ParseContext context)
        {
            var commandSet = MakeDbCommandSetFromParseContext(context);
            _commandList.Enqueue(commandSet);
            return commandSet;
        }

        public DbCommandSet CreateCommand(ParseContext context, string parameterName, object parameterValue)
        {
            var commandSet = MakeDbCommandSetFromParseContext(context);
            var parameter = GetDataParameter(commandSet.DbCommand, parameterName, parameterValue);
            commandSet.DbCommand.Parameters.Add(parameter);
            _commandList.Enqueue(commandSet);
            return commandSet;
        }
        
        public virtual DbCommandSet CreateCommand(ParseContext context, IEnumerable<string> parameterNames, IEnumerable<object> parameterValues)
        {
            var commandSet = MakeDbCommandSetFromParseContext(context);
            var parameters = GetDataParameters(commandSet.DbCommand, parameterNames, parameterValues);
            commandSet.DbCommand.Parameters.AddRange(parameters);
            _commandList.Enqueue(commandSet);
            return commandSet;
        }

        public virtual DbCommandSet CreateCommand(IDbCommand dbCommand)
        {
            DbCommandSet commandSet = new DbCommandSet();
            commandSet.DbCommand = dbCommand;
            commandSet.SqlInfo = MakeSqlInfo(dbCommand.CommandText);
            _commandList.Enqueue(commandSet);
            return commandSet;
        }

        #region Util
        protected IDbCommand GetIDbCommand(string sql)
        {
            return _builderUtil.GetIDbCommand(sql);
        }

        protected IDbCommand GetIDbCommand(ParseContext context)
        {
            return _builderUtil.GetIDbCommand(context.ParsedSql);
        }

        protected SqlBaseInfo MakeSqlInfo(string sql)
        {
            return _builderUtil.MakeSqlInfo(sql);
        }

        protected SqlBaseInfo MakeSqlInfo(ParseContext context)
        {
            return _builderUtil.MakeSqlInfo(context);
        }

        DbCommandSet MakeDbCommandSetFromSql(string sql)
        {
            return _builderUtil.MakeDbCommandSetFromSql(sql);
        }

        DbCommandSet MakeDbCommandSetFromParseContext(ParseContext context)
        {
            return _builderUtil.MakeDbCommandSetFromParseContext(context);
        }

        protected IDataParameter GetDataParameter(IDbCommand dbCommand, string name, object value, string prefix = null)
        {
            return _builderUtil.GetDataParameter(dbCommand, name, value, prefix);
        }

        protected IEnumerable<IDataParameter> GetDataParameters(IDbCommand dbCommand, SqlResult sqlResult, string prefix = null)
        {
            return _builderUtil.GetDataParameters(dbCommand, sqlResult, prefix);
        }

        protected IEnumerable<IDataParameter> GetDataParameters(IDbCommand dbCommand, IEnumerable<string> names, IEnumerable<object> values, string prefix = null)
        {
            return _builderUtil.GetDataParameters(dbCommand, names, values);
        }

        protected IDictionary<string, object> GetNameValuePairs(SqlResult sqlResult)
        {
            return _builderUtil.GetNameValuePairs(sqlResult);
        }

        protected IDictionary<string, object> GetNameValuePairs(IEnumerable<string> names, IEnumerable<object> values)
        {
            return _builderUtil.GetNameValuePairs(names, values);
        }

        protected object GetValueOrDBNull(string value, Type valueType)
        {
            return _builderUtil.GetValueOrDBNull(value, valueType);
        }
        #endregion

        #region IEnumerable
        public IEnumerator<DbCommandSet> GetEnumerator()
        {
            return _commandList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
