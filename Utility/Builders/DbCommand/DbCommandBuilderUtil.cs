using MasudaManager.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MasudaManager.Utility
{
    public class DbCommandBuilderUtil
    {
        readonly string _typeConvertErrorFormat = "Data type convert error: Value={0}, Type={1}";
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();

        public IDbCommand GetIDbCommand(string sql)
        {
            var command = _dataAccess.DbCommand;
            command.CommandText = sql;
            return command;
        }

        public IDbCommand GetIDbCommand(ParseContext context)
        {
            return GetIDbCommand(context.ParsedSql);
        }

        public SqlBaseInfo MakeSqlInfo(string sql)
        {
            return MakeSqlInfo(SqlParser.ParseSqlType(sql));
        }

        public SqlBaseInfo MakeSqlInfo(ParseContext context)
        {
            SqlBaseInfo sqlInfo = new SqlBaseInfo();
            sqlInfo.InputSql = context.RawSql;
            sqlInfo.ExecutedSql = context.ParsedSql;
            sqlInfo.SqlType = context.SqlType;
            sqlInfo.SqlException = context.ParseError;
            sqlInfo.SqlCommandToken = context.SqlCommandToken;
            return sqlInfo;
        }

        public DbCommandSet MakeDbCommandSetFromSql(string sql)
        {
            DbCommandSet commandSet = new DbCommandSet();
            commandSet.DbCommand = GetIDbCommand(sql);
            commandSet.SqlInfo = MakeSqlInfo(sql);
            return commandSet;
        }

        public DbCommandSet MakeDbCommandSetFromParseContext(ParseContext context)
        {
            DbCommandSet commandSet = new DbCommandSet();
            commandSet.DbCommand = GetIDbCommand(context);
            commandSet.SqlInfo = MakeSqlInfo(context);
            return commandSet;
        }

        public IDataParameter GetDataParameter(IDbCommand dbCommand, string name, object value, string prefix = null)
        {
            var dataParameter = dbCommand.CreateParameter();
            dataParameter.ParameterName = prefix + name;
            dataParameter.Value = value;
            return dataParameter;
        }

        public IEnumerable<IDataParameter> GetDataParameters(IDbCommand dbCommand, SqlResult sqlResult, string prefix = null)
        {
            foreach (var pair in GetNameValuePairs(sqlResult))
            {
                var dataParameter = dbCommand.CreateParameter();
                dataParameter.ParameterName = prefix + pair.Key;
                dataParameter.Value = pair.Value;
                yield return dataParameter;
            }
        }

        public IEnumerable<IDataParameter> GetDataParameters(IDbCommand dbCommand, IEnumerable<string> names, IEnumerable<object> values, string prefix = null)
        {
            foreach (var pair in GetNameValuePairs(names, values))
            {
                var dataParameter = dbCommand.CreateParameter();
                dataParameter.ParameterName = prefix + pair.Key;
                dataParameter.Value = pair.Value;
                yield return dataParameter;
            }
        }

        public IDictionary<string, object> GetNameValuePairs(SqlResult sqlResult)
        {
            return sqlResult.ColumnNames
                            .Select((Name, Id) => new { Name, Id })
                            .ToDictionary
                            (
                                s => s.Name,
                                s => GetValueOrDBNull(sqlResult.RowValues[s.Id], sqlResult.ColumnTypes[s.Id])
                            );

        }

        public IDictionary<string, object> GetNameValuePairs(IEnumerable<string> names, IEnumerable<object> values)
        {
            return names.Zip(values, (n, v) => new { Name = n, Value = v }).ToDictionary(p => p.Name, p => p.Value);
        }

        public object GetValueOrDBNull(string value, Type valueType)
        {
            if (value == null)
                return DBNull.Value;
            
            if (Convert.IsDBNull(value))
                return DBNull.Value;

            try
            {
                return Convert.ChangeType(value, valueType);
            }
            catch
            {
                throw new Exception(String.Format(_typeConvertErrorFormat, value, valueType.FullName));
            }
        }
    }
}
