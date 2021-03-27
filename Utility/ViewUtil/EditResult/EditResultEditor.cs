using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MasudaManager.Utility
{
    class EditResultEditor
    {
        IGridViewEditingCoordinator _coordinator;
        Action _callbackMethod;
        EditData _currentData = new EditData();
        EditData _undoData = new EditData();
        EditingColor _colors = new EditingColor();
        EditResultHistory _editHistory = new EditResultHistory(0);
        EditResultEdition _currentEdition = new EditResultEdition();
        IEnumerable<EditData> _applicableEdition;
        EditResultPasteDataConverter _pasteDateConverter = new EditResultPasteDataConverter();
        IEnumerable<EditData> _invalidRowEditData;
        int _undoInsertCount = 0;  

        #region Property
        public IEnumerable<EditData> CurrentEdition { get { return _currentEdition; } }
        public IEnumerable<EditData> ApplicableEdition { get { return _applicableEdition; } }
        public bool CanRedo { get { return _editHistory.CanRedo; } }
        public bool CanUndo { get { return _editHistory.CanUndo; } }
        public EditInsertPositionType InsertPosition { get; set; }
        public IGridViewEditingCoordinator Coordinator
        {
            get { return _coordinator; }
            set { _coordinator = value; }
        }
        public EditingColor EditColor
        {
            get { return _colors; }
            set { _colors = value; }
        }
        public Action EditCompleteAction
        {
            get { return _callbackMethod; }
            set { _callbackMethod = value; }
        }
        public Brush UpdateCellColor
        {
            get { return _colors.UpdateColor; }
            set { _colors.UpdateColor = value; }
        }
        public Brush DeletedRowColor
        {
            get { return _colors.DeleteColor; }
            set { _colors.DeleteColor = value; }
        }
        public Brush InsertedRowColor
        {
            get { return _colors.InsertColor; }
            set { _colors.InsertColor = value; }
        }
        #endregion

        #region PrepareUndoRedoStack
        public void PrepareUndoRedoStack(int capacity)
        {
            _editHistory = new EditResultHistory(capacity);
        }
        #endregion

        #region Release
        public void Release()
        {
            if (_editHistory != null)
                _editHistory.Clear();

            _currentEdition.Clear();
            _applicableEdition = null;
            _pasteDateConverter.Release();
            _invalidRowEditData = null;
        }
        #endregion

        #region RebuildEditions
        public IEnumerable<EditData> RebuildEditions()
        {
            _applicableEdition = _currentEdition.Reassemble(_coordinator.GetEditData(_colors.GetEditType));

            return _applicableEdition;
        }
        #endregion

        #region Undo/Redo
        public void Redo()
        {
            if (!_editHistory.CanRedo)
                return;

            BeginEdit();
            
            _editHistory.Redo(_currentEdition);

            DoCallBack();
        }

        public void Undo()
        {
            if (!_editHistory.CanUndo)
                return;

            BeginEdit();

            _editHistory.Undo(_currentEdition);

            DoCallBack();
        }
        #endregion

        #region Paste
        public void Paste(CellRange range)
        {
            BeginEdit();

            foreach (GridPasteData pasteData in GetPasteDataList(range))
            {
                PasteRow(pasteData);
            }

            CompleteEdit();
        }

        IEnumerable<GridPasteData> GetPasteDataList(CellRange range)
        {
            _pasteDateConverter.Prepare(ClipboardExtensions.GetTextOrNull());
            return _pasteDateConverter.Convert(range);
        }
      
        void PasteRow(GridPasteData pastedRowData)
        {
            for (int i = 0; i < pastedRowData.RowValues.Count; i++)
            {
                UpdateCell(pastedRowData.CellPositions[i], pastedRowData.RowValues[i]);
            }
        }
        #endregion

        #region Erase
        public void Erase(IEnumerable<Cell> cells)
        {
            BeginEdit();

            foreach (var cell in cells)
            {
                UpdateCell(cell, null);
            }

            CompleteEdit();
        }
        #endregion

        #region UpdateCell
        void UpdateCell(Cell updateCell, object updateValue)
        {
            if (_coordinator.GetCellColor(updateCell) == this.DeletedRowColor)
                return;

            object currentValue = _coordinator.GetCellValue(updateCell);

            if (Object.Equals(currentValue.ToStringOrNull(), updateValue.ToStringOrNull()))
                return;

            _currentData = MakeEditData(GetEditModeForUpdate(updateCell), updateCell, updateValue);
            _currentData.UndoEditData = MakeEditData(EditType.Edit, updateCell, currentValue);

            _currentEdition.Add(_currentData);
            _editHistory.Do(_currentEdition);
        }
        #endregion

        #region Update
        public void BeginUpdate()
        {
            BeginEdit();

            _undoData = MakeEditData(EditType.Edit, _coordinator.CurrentCell, _coordinator.CurrentCellValue);
        }

        public void EndUpdate()
        {
            if (Object.Equals(_coordinator.CurrentCellValue.ToStringOrNull(), _undoData.Value.ToStringOrNull()))
                CompleteEdit();
            else
                DoEndUpdate();
        }

        void DoEndUpdate()
        {
            _currentData = MakeEditData(GetEditModeForUpdate(_coordinator.CurrentCell), _coordinator.CurrentCell, _coordinator.CurrentCellValue);
            _currentData.UndoEditData = _undoData;

            _currentEdition.Add(_currentData);
            _editHistory.Do(_currentEdition);

            CompleteEdit();
        }
        #endregion

        #region GetEditModeForUpdate
        EditType GetEditModeForUpdate(Cell cell)
        {
            if (_coordinator.GetCellColor(cell) == this.InsertedRowColor)
                return EditType.Edit;

            return EditType.Update;
        }
        #endregion

        #region Insert
        public void Insert()
        {
            BeginEdit();

            Cell cell = _coordinator.CurrentCell;

            if (cell == Cell.Empty)
                cell = new Cell(0, 0);

            _currentData = MakeEditData(EditType.Insert, cell, null);
            _currentData.UndoEditData = MakeEditData(EditType.Remove, cell, null);

            _currentEdition.Add(_currentData);
            _editHistory.Do(_currentEdition);

            CompleteEdit();
        }

        public void BulkInsert(int rowCount)
        {
            BeginEdit();

            Cell cell = _coordinator.CurrentCell;

            if (cell == Cell.Empty)
                cell = new Cell(0, 0);

            for (int i = 0; i < rowCount; i++)
            {
                _currentData = MakeEditData(EditType.BulkInsert, cell, null);
                _currentData.UndoEditData = MakeEditData(EditType.Remove, cell, null);

                _currentEdition.Add(_currentData);
                _editHistory.Do(_currentEdition);

                cell.RowIndex++;
            }

            CompleteEdit();
        }
        #endregion

        #region Delete
        public void Delete(IEnumerable<Cell> cells, int columnCount)
        {
            BeginEdit();

            _undoInsertCount = 0;
            _invalidRowEditData = GetInvalidatingRowEditData(cells, columnCount);

            foreach(var editData in _invalidRowEditData.OrderBy(edit => edit.Type))
            {
                _currentEdition.Add(editData);
                _editHistory.Do(_currentEdition);
            }

            CompleteEdit();
        }

        IEnumerable<EditData> GetInvalidatingRowEditData(IEnumerable<Cell> cells, int columnCount)
        {
            foreach (var cell in cells)
            {
                if (_coordinator.GetCellColor(cell) != this.DeletedRowColor)
                {
                    if (_coordinator.GetCellColor(cell) == this.InsertedRowColor)
                        yield return GetRemovingRowEditData(cell, columnCount);
                    else
                        yield return GetDeletingRowEditData(cell, columnCount);
                }
            }
        }

        EditData GetRemovingRowEditData(Cell cell, int columnCount)
        {
            _currentData = MakeEditData(EditType.Remove, cell, null);
            _undoData = MakeEditData(EditType.Insert, GetUndoInsertableCell(cell), GetCurrentRowValues(cell.RowIndex, columnCount));
            _currentData.UndoEditData = _undoData;

            return _currentData;
        }

        EditData GetDeletingRowEditData(Cell cell, int columnCount)
        {
            _currentData = MakeEditData(EditType.Delete, cell, null);
            _undoData = new EditData();
            _undoData.AddRange(GetEditDataWithReversedRowValues(cell.RowIndex, columnCount));
            _currentData.UndoEditData = _undoData;

            return _currentData;
        }

        Cell GetUndoInsertableCell(Cell cell)
        {
            int rowIndex = cell.RowIndex - _undoInsertCount;
            _undoInsertCount++;

            return new Cell(cell.ColumnIndex, rowIndex);
        }
        
        List<object> GetCurrentRowValues(int rowIndex, int columnCount)
        {
            List<object> values = new List<object>();
            Cell cell = new Cell(0, rowIndex);
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                cell.ColumnIndex = columnIndex;
                values.Add(_coordinator.GetCellValue(cell));
            }
            return values;
        }

        IEnumerable<EditData> GetEditDataWithRowValues(int rowIndex, int columnCount)
        {
            Cell cell = new Cell(0, rowIndex);
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                cell.ColumnIndex = columnIndex;
                yield return MakeEditData(EditType.Edit, cell, _coordinator.GetCellValue(cell));
            }
        }

        IEnumerable<EditData> GetEditDataWithReversedRowValues(int rowIndex, int columnCount)
        {
            Cell cell = new Cell(0, rowIndex);
            for (int columnIndex = columnCount - 1; columnIndex >= 0; columnIndex--)
            {
                cell.ColumnIndex = columnIndex;
                yield return MakeEditData(EditType.Edit, cell, _coordinator.GetCellValue(cell));
            }
        }
        #endregion

        #region MakeEditData
        EditData MakeEditData(EditType editType, Cell cell, object cellValue)
        {
            switch (editType)
            {
                case EditType.Edit:
                    return new EditData(editType, cell, _coordinator.GetCellColor(cell), cellValue);
                case EditType.Update:
                    return new EditData(editType, cell, this.UpdateCellColor, cellValue);
                case EditType.Remove:
                case EditType.BulkInsert:
                case EditType.Insert:
                    return new EditData(editType, GetInsertingCell(cell), this.InsertedRowColor, cellValue);
                case EditType.Delete:
                    return new EditData(editType, cell, this.DeletedRowColor, cellValue);
            }

            throw new ArgumentException("Invalid EditMode");
        }
        #endregion

        #region GetInsertingCell
        Cell GetInsertingCell(Cell currentCell)
        {
            switch (this.InsertPosition)
            {
                case EditInsertPositionType.AboveCurrentRow:
                    if(currentCell.RowIndex > 0)
                        currentCell.RowIndex--;
                    break;
                case EditInsertPositionType.BelowCurrentRow:
                    currentCell.RowIndex++;
                    break;
            }

            return currentCell;
        }
        #endregion

        #region Begin/Complete edit
        void BeginEdit()
        {
            _currentEdition.Clear();
            _editHistory.BeginDo();
        }

        void CompleteEdit()
        {
            if (_currentEdition.Any())
            {
                _editHistory.EndDo();
                DoCallBack();
            }
            else
            {
                _editHistory.CancelDo();
            }
        }
        #endregion

        #region Callback
        void DoCallBack()
        {
            if (_callbackMethod != null)
                _callbackMethod.Invoke();
        }
        #endregion
    }
}
