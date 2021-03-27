using MasudaManager.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MasudaManager.Views
{
    public class EditResultDbCommandBuilder : DbCommandBuilder
    {
        EditResultModel _model;
        UpdatePreparedDbCommandBuilder _updateCommandBuilder = new UpdatePreparedDbCommandBuilder();
        DeleteDbCommandBuilder _deleteCommandBuilder = new DeleteDbCommandBuilder();
        InsertDbCommandBuilder _insertCommandBuilder = new InsertDbCommandBuilder();

        public void SetModel(EditResultModel model)
        {
            _model = model;
        }

        public void Release()
        {
            _updateCommandBuilder.Clear();
            _deleteCommandBuilder.Clear();
            _insertCommandBuilder.Clear();
            this.Clear();
        }

        public void CreateDbCommands(EditData editData)
        {
            switch(editData.Type)
            {
                case EditType.Update:
                    CreateUpdateCommand(editData);
                    return;
                case EditType.Delete:
                    CreateDeleteCommand();
                    return;
                case EditType.Insert:
                    CreateInsertCommand(editData);
                    return;
            }

            throw new InvalidOperationException();
        }

        void CreateUpdateCommand(EditData editData)
        {
            var command = _updateCommandBuilder.CreateCommand(_model.TableName, MakeSqlResultForUpdate(editData), _model.KeyColumnData.ToSqlResult());
            Add(command);
        }

        void CreateDeleteCommand()
        {
            var command = _deleteCommandBuilder.CreateCommand(_model.TableName, _model.KeyColumnData.ToSqlResult());
            Add(command);
        }
       
        void CreateInsertCommand(EditData editData)
        {
            var command = _insertCommandBuilder.CreateCommand(_model.TableName, MakeSqlResultForInsert(editData.Cell.RowIndex));
            Add(command);
        }

        SqlResult MakeSqlResultForUpdate(EditData editData)
        {
            SqlResult result = new SqlResult();
            foreach (var data in editData.Children)
            {
                result.ColumnNames.Add(_model.HeaderColumnNames[data.Cell.ColumnIndex]);
                result.ColumnTypes.Add(_model.HeaderColumnTypes[data.Cell.ColumnIndex]);
                result.RowValues.Add(GetRowValueForUpdate(data.Cell));
            }
            return result;
        }

        string GetRowValueForUpdate(Cell cell)
        {
            return _model.BindingList.GetFieldValue(cell);
        }

        SqlResult MakeSqlResultForInsert(int rowIndex)
        {
            SqlResult result = new SqlResult();
            result.ColumnNames = _model.HeaderColumnNames;
            result.ColumnTypes = _model.HeaderColumnTypes;
            result.RowValues = GetRowValuesForInsert(rowIndex).ToList();
            return result;
        }

        IEnumerable<string> GetRowValuesForInsert(int rowIndex)
        {
            return _model.BindingList[rowIndex].Values;
        }
    }
}
