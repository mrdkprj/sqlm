using MasudaManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasudaManager.Views
{
    public class EditResultRequisiteChecker
    {
        ParseContext _context = new ParseContext();
        SchemaTableSqlService _schemaTableService = new SchemaTableSqlService();
        QuerySqlService _queryService = new QuerySqlService();
        EditResultModel _model;
        SqlResult _keyColumnSqlResult = new SqlResult();
        SqlResult _selectedColumnSqlResult = new SqlResult();
        bool _hasValidKeyColumns = false;
        string _tableName = null;

        public EditResultRequisiteChecker(EditResultModel model)
        {
            _model = model;
        }

        public bool CheckSql()
        {

            ThrowIfMoreThanOneSql();
            ThrowIfInvalidSqlType();
            ThrowIfTableNotFound();
            ThrowIfInvalidColumnIncluded();

            return true;
        }

        public void OrganizeModel()
        {
            try
            {
                ThrowIfKeyColumnNotFound();
                ThrowIfKeyColumnNotIncluded();
                _hasValidKeyColumns = true;
            }
            catch (TableKeyNotFoundException keynotfoundException)
            {
                _hasValidKeyColumns = false;
                throw keynotfoundException;
            }
            finally
            {
                SetEditGridModel();
            }
        }

        void SetEditGridModel()
        {
            _model.TableName = _tableName;
            _model.SourceSql = _context.ParsedSql;
            _model.HeaderColumnNames = _selectedColumnSqlResult.ColumnNames;
            _model.HeaderColumnTypes = _selectedColumnSqlResult.ColumnTypes;

            CreateKeyColumnData();
        }

        void CreateKeyColumnData()
        {
            _model.KeyColumnData = new KeyColumnData();
            if (!_hasValidKeyColumns)
                _keyColumnSqlResult = _selectedColumnSqlResult;
            _model.KeyColumnData.SetColumnIndexes(GetKeyColumnIndexes());
            _model.KeyColumnData.SetColumnNames(_keyColumnSqlResult.ColumnNames);
            _model.KeyColumnData.SetColumnTypes(_keyColumnSqlResult.ColumnTypes);
        }

        IEnumerable<int> GetKeyColumnIndexes()
        {
            foreach (string name in _keyColumnSqlResult.ColumnNames)
            {
                yield return _model.HeaderColumnNames.IndexOf(name);
            }
        }

        void ThrowIfMoreThanOneSql()
        {
            List<ParseContext> contextList = SqlParser.Parse(_model.SourceSql).ToList();

            if (contextList.Count != 1)
                throw new Exception(LocalizedTextProvider.Message.Error.InvalidNumberOfSql);

            _context = contextList.FirstOrDefault();
        }

        void ThrowIfInvalidSqlType()
        {
            if (_context.SqlType != SqlType.Query)
                throw new Exception(LocalizedTextProvider.Message.Error.InvalidSql);
        }

        void ThrowIfTableNotFound()
        {
            _tableName = _schemaTableService.GetTableNameFromSql(_context.ParsedSql);

            if (String.IsNullOrEmpty(_tableName))
                throw new Exception(LocalizedTextProvider.Message.Error.TableNameNotFound);

            _tableName = _tableName.ToUpper();
        }

        void ThrowIfInvalidColumnIncluded()
        {
            List<string> allColumnNames = _schemaTableService.GetColumnInfoFromTable(_tableName).FirstOrDefault().ColumnNames;
            _selectedColumnSqlResult = _schemaTableService.GetColumnInfoFromSql(_context.ParsedSql).FirstOrDefault();

            if (_selectedColumnSqlResult.ColumnNames.Except(allColumnNames).Any())
                throw new Exception(LocalizedTextProvider.Message.Error.InvalidColumnIncluded);
        }

        void ThrowIfKeyColumnNotFound()
        {
            _keyColumnSqlResult = _schemaTableService.GetKeyColumnInfoFromTable(_tableName).FirstOrDefault();

            if (_keyColumnSqlResult.ColumnNames.Count <= 0)
                throw new TableKeyNotFoundException(LocalizedTextProvider.Message.Error.PrimaryKeyNotFound);       
        }

        void ThrowIfKeyColumnNotIncluded()
        {
            if (_keyColumnSqlResult.ColumnNames.Except(_selectedColumnSqlResult.ColumnNames).Any())
                throw new TableKeyNotFoundException(LocalizedTextProvider.Message.Error.PrimaryKeyNotIncluded);
        }
    }
}
