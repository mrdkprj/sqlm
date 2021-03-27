using MasudaManager.Utility;
using MasudaManager.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MasudaManager.Controls
{
    public class GridViewEditingCoordinator : IGridViewEditingCoordinator
    {
        Cell _currentCell = Cell.Empty;
        Cell _restoringCell = Cell.Empty;
        Cell _cell = Cell.Empty;
        QueryResultBindingList _bindingList;

        Dictionary<Cell, object> _oldValues = new Dictionary<Cell, object>();
        List<Cell> _selectedCells = null;
        List<RowStyle> _styles = null;

        HashSet<QueryResult> _insertingResults = new HashSet<QueryResult>();
        HashSet<RowStyle> _insertingStyles = new HashSet<RowStyle>();
        HashSet<RowStyle> _removingStyles = new HashSet<RowStyle>();
        HashSet<QueryResult> _removingResults = new HashSet<QueryResult>();

        EditingColor _editColor = new EditingColor();
        bool _isDataBound = false;
        int _cellCount = 0;
        int _columnCount = 0;
        int _insertingPosition = 0;

        #region Property
        public int CellCount { get { return _cellCount; } }
        public int ColumnCount { get { return _columnCount; } }
        public Cell CurrentCell { get { return _currentCell; } }
        public Brush CurrentCellColor
        {
            get
            {
                if (BrushExists(_currentCell))
                    return GetBrush(_currentCell);
                else
                    return this.EditColor.DefaultColor;
            }
        }
        public object CurrentCellValue { get { return _bindingList.GetFieldValue(_currentCell); } }
        public int RowCount { get { return _bindingList == null ? 0 : _bindingList.Count; } }
        public int SelectedCellsCount { get { return _selectedCells.Count; } }

        public bool IsAllSelected { get; set; }
        public bool IsDataBound { get { return _isDataBound; } }
        public bool SuppressCellSelectionChange { get; set; }
        public EditingColor EditColor { get; set; }
        #endregion

        #region Release
        public void Release()
        {
            _bindingList = null;
            _oldValues = null;
            _selectedCells = null;
            _styles = null;
            _insertingResults = null;
            _insertingStyles = null;
            _removingStyles = null;
            _removingResults = null;
        }
        #endregion

        #region DataSource
        public void SetDataSource(QueryResultBindingList source)
        {
            _bindingList = source;
            PrepareCounters();
            PrepareCollections();

            _isDataBound = true;
        }

        void PrepareCounters()
        {
            _cellCount = _bindingList.Count * _columnCount;
            _columnCount = _bindingList.PropertyNames.Count;
        }

        void PrepareCollections()
        {
            _selectedCells = new List<Cell>(_cellCount);
            _styles = new List<RowStyle>(_cellCount);
            CreateRowStyles();
        }

        void CreateRowStyles()
        {
            for (int rowIndex = 0; rowIndex < _bindingList.Count; rowIndex++)
            {
                _styles.Add(new RowStyle(rowIndex, _columnCount));
            }
        }
        #endregion
        
        #region Brush
        public bool BrushExists(Cell cell)
        {
            return BrushExists(cell.ColumnIndex, cell.RowIndex);
        }

        public bool BrushExists(int columnIndex, int rowIndex)
        {
            return _styles[rowIndex].HasCellStyle(columnIndex);
        }

        public Brush GetBrush(Cell cell)
        {
            return GetBrush(cell.ColumnIndex, cell.RowIndex);
        }

        public Brush GetBrush(int columnIndex, int rowIndex)
        {
            return _styles[rowIndex].GetColor(columnIndex);
        }
        #endregion

        #region Color
        public Brush GetCellColor(Cell cell)
        {
            if (BrushExists(cell))
                return GetBrush(cell);

            return this.EditColor.DefaultColor;
        }

        public Brush GetCurrentRowColor()
        {
            if (BrushExists(_currentCell))
                return GetBrush(_currentCell);

            return this.EditColor.DefaultColor;
        }

        public Brush GetRowColor(int rowIndex)
        {
            if (BrushExists(GetCell(0, rowIndex)))
                return GetBrush(GetCell(0, rowIndex));

            return this.EditColor.DefaultColor;
        }

        public void SetCellColor(Cell cell, Brush brush)
        {
            if (brush == this.EditColor.DefaultColor)
                ResetCellBrush(cell);
            else
                SetCellBrush(cell, brush);
        }

        void SetCellBrush(Cell cell, Brush brush)
        {
            _styles[cell.RowIndex].SetColor(cell.ColumnIndex, brush);
        }

        void ResetCellBrush(Cell cell)
        {
            _styles[cell.RowIndex].ResetColor(cell.ColumnIndex);
        }

        public void SetRowColor(int rowIndex, Brush brush)
        {
            _styles[rowIndex].SetColor(brush);
        }

        public void ResetRowColor(int rowIndex)
        {
            _styles[rowIndex].ResetColor();
        }
        #endregion

        #region Cell
        public object GetCellOriginalValue(Cell cell)
        {
            return _oldValues.GetValueOrDefault(cell);
        }

        public object GetCellValue(Cell cell)
        {
            if (_bindingList.Count > 0)
                return _bindingList.GetFieldValue(cell);

            return null;
        }
        
        public bool OriginalValueExists(Cell cell)
        {
            return _oldValues.ContainsKey(cell);
        }

        public void SaveOriginalCellValue(Cell cell)
        {
            if (_oldValues.ContainsKey(cell))
                return;

            _oldValues[cell] = _bindingList.GetFieldValue(cell);
        }

        public void SetCellValue(Cell cell, object value)
        {
            _bindingList.SetFieldValue(cell, value.ToStringOrNull());
        }
        #endregion

        #region Current cell
        public void SetCurrentCell(Cell cell)
        {
            _currentCell = cell;
        }

        public void SetRestoringCurrentCell(Cell cell)
        {
            _restoringCell = cell;
        }
        
        public void RestoreCurrentCell()
        {
            ResetSelection();
            _currentCell = _restoringCell;
            _selectedCells.Add(_restoringCell);
        }
        #endregion

        #region Insert/Remove
        public void PrepareInsert(int rowIndex, object values)
        {
            if (_insertingResults.Count <= 0)
                _insertingPosition = rowIndex;

            _insertingStyles.Add(new RowStyle(rowIndex, _columnCount));
            _insertingResults.Add(_bindingList.CreateResultWithValues(values as IEnumerable<object>));
        }

        public void PrepareRemove(int rowIndex)
        {
            _removingStyles.Add(_styles[rowIndex]);
            _removingResults.Add(_bindingList[rowIndex]);
        }

        public void ExecuteInsert(int rowIndex, object values)
        {
            _bindingList.Insert(rowIndex, _bindingList.CreateResultWithValues(values as IEnumerable<object>));
            _styles.Insert(rowIndex, new RowStyle(rowIndex, _columnCount));
            SetRowColor(rowIndex, this.EditColor.InsertColor);
            RefreshStylesRowIndex();
        }

        public void ExecutePreparedInsert()
        {
            if (_insertingResults.Count <= 0)
                return;

            _bindingList.InsertRange(_insertingPosition, _insertingResults);
            _styles.InsertRange(_insertingPosition, _insertingStyles);

            foreach (var style in _insertingStyles)
            {
                SetRowColor(style.RowIndex, this.EditColor.InsertColor);
            }

            RefreshStylesRowIndex();

            _insertingResults.Clear();
            _insertingStyles.Clear();
        }

        public void ExecutePreparedRemove()
        {
            if (_removingResults.Count <= 0)
                return;

            _bindingList.RemoveAll(s => _removingResults.Contains(s));
            _styles.RemoveAll(s => _removingStyles.Contains(s));

            RefreshStylesRowIndex();

            _removingStyles.Clear();
            _removingResults.Clear();
        }

        void RefreshStylesRowIndex()
        {
            for (int rowIndex = 0; rowIndex < _styles.Count; rowIndex++)
            {
                _styles[rowIndex].RowIndex = rowIndex;
            }
        }
        #endregion

        #region Cells selection
       
        public IEnumerable<Cell> GetSelectedCells()
        {
            return _selectedCells.OrderBy(cell => cell.RowIndex)
                                 .ThenBy(cell => cell.ColumnIndex);
        }

        public IEnumerable<Cell> GetSelectedCellsInDescendingOrder()
        {
            return _selectedCells.OrderByDescending(cell => cell.RowIndex)
                                 .ThenByDescending(cell => cell.ColumnIndex);
        }

        public IEnumerable<Cell> GetSelectedRows()
        {
            return _selectedCells.GroupBy(cell => cell.RowIndex)
                                 .Select(group => group.First())
                                 .OrderBy(cell => cell.RowIndex)
                                 .ThenBy(cell => cell.ColumnIndex);
        }

        public IEnumerable<Cell> GetSelectedRowsInDescendingOrder()
        {
            return _selectedCells.GroupBy(cell => cell.RowIndex)
                                 .Select(group => group.First())
                                 .OrderByDescending(cell => cell.RowIndex)
                                 .ThenBy(cell => cell.ColumnIndex);
        }

        public void ResetSelection()
        {
            this.IsAllSelected = false;
            _selectedCells.Clear();
        }
        
        public void SelectCell(int columnIndex, int rowIndex)
        {
            if (this.IsAllSelected)
                ResetSelection();

            _selectedCells.Add(GetCell(columnIndex, rowIndex));
        }

        public void SelectCells(IEnumerable<Cell> cells)
        {
            ResetSelection();
            _selectedCells.AddRange(cells);
        }

        public void SelectCellRange(Cell startCell, Cell endCell)
        {
            ResetSelection();

            for (int rowIndex = startCell.RowIndex; rowIndex <= endCell.RowIndex; rowIndex++)
            {
                _cell.RowIndex = rowIndex;

                for (int columnIndex = startCell.ColumnIndex; columnIndex <= endCell.ColumnIndex; columnIndex++)
                {
                    _cell.ColumnIndex = columnIndex;
                    _selectedCells.Add(_cell);
                }
            }
        }

        public void SelectAllCells()
        {
            this.IsAllSelected = true;
            _selectedCells.Clear();

            for (int rowIndex = 0; rowIndex < _bindingList.Count; rowIndex++)
            {
                _cell.RowIndex = rowIndex;

                for (int columnIndex = 0; columnIndex < _columnCount; columnIndex++)
                {
                    _cell.ColumnIndex = columnIndex;
                    _selectedCells.Add(_cell);
                }
            }
        }      
        #endregion

        #region CanEditCell
        public bool CanEditCell(Cell cell)
        {
            return CanEditCell(cell.ColumnIndex, cell.RowIndex);
        }

        public bool CanEditCell(int columnIndex, int rowIndex)
        {
            if (!BrushExists(columnIndex, rowIndex))
                return true;

            if (GetBrush(columnIndex, rowIndex) == this.EditColor.DeleteColor)
                return false;

            return true;
        }
        #endregion

        #region GetEditedData
        public IEnumerable<EditData> GetEditData(Func<Brush, EditType> brushTypeConverter)
        {
            foreach(var style in _styles.Where(s => s.HasStyle))
            {
                foreach (var pair in style.GetCellBrushes())
                {
                    EditData editData = new EditData();
                    editData.Type = brushTypeConverter(pair.Value);
                    editData.Cell = pair.Key;
                    yield return editData;
                }
            }
        }
        #endregion

        #region GetCell
        Cell GetCell(int columnIndex, int rowIndex)
        {
            _cell.ColumnIndex = columnIndex;
            _cell.RowIndex = rowIndex;
            return _cell;
        }
        #endregion

    }
}
