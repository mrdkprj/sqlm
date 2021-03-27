using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MasudaManager.Utility
{
    public interface IGridViewEditingCoordinator
    {
        bool IsAllSelected { get; set; }
        bool IsDataBound { get; }
        int CellCount { get; }
        int ColumnCount { get; }
        Cell CurrentCell { get; }
        Brush CurrentCellColor { get; }
        object CurrentCellValue { get; }
        EditingColor EditColor { get; set; }
        int RowCount { get; }
        int SelectedCellsCount { get; }
        bool SuppressCellSelectionChange { get; set; }

        bool BrushExists(Cell cell);
        bool BrushExists(int columnIndex, int rowIndex);
        bool CanEditCell(Cell cell);
        bool CanEditCell(int columnIndex, int rowIndex);
        void ExecuteInsert(int rowIndex, object values);
        void ExecutePreparedInsert();
        void ExecutePreparedRemove();
        Brush GetBrush(Cell cell);
        Brush GetBrush(int columnIndex, int rowIndex);
        Brush GetCellColor(Cell cell);
        object GetCellOriginalValue(Cell cell);
        object GetCellValue(Cell cell);
        Brush GetCurrentRowColor();
        IEnumerable<EditData> GetEditData(Func<Brush, EditType> brushTypeConverter);
        Brush GetRowColor(int rowIndex);
        IEnumerable<Cell> GetSelectedCells();
        IEnumerable<Cell> GetSelectedCellsInDescendingOrder();
        IEnumerable<Cell> GetSelectedRows();
        IEnumerable<Cell> GetSelectedRowsInDescendingOrder();
        bool OriginalValueExists(Cell cell);
        void PrepareInsert(int rowIndex, object values);
        void PrepareRemove(int rowIndex);
        void Release();
        void ResetSelection();
        void RestoreCurrentCell();
        void SaveOriginalCellValue(Cell cell);
        void SelectAllCells();
        void SelectCell(int columnIndex, int rowIndex);
        void SelectCells(IEnumerable<Cell> cells);
        void SelectCellRange(Cell startCell, Cell endCell);
        void SetCellColor(Cell cell, Brush color);
        void SetCellValue(Cell cell, object value);
        void SetCurrentCell(Cell cell);
        void SetDataSource(QueryResultBindingList list);
        void SetRestoringCurrentCell(Cell cell);
        void SetRowColor(int rowIndex, Brush color);
    }
}
