using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class RowStyle : IEquatable<RowStyle>
    {
        Cell _cell = Cell.Empty;
        int _rowIndex = 0;
        int _columnCount = 0;
        Dictionary<int, Brush> _columnBrushes = new Dictionary<int, Brush>();

        public RowStyle(int rowIndex, int columnCount)
        {
            _rowIndex = rowIndex;
            _columnCount = columnCount;
        }
        
        public int RowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }

        public bool HasStyle { get { return _columnBrushes.Count > 0; } }

        public bool HasCellStyle(int columnIndex)
        {
            return _columnBrushes.ContainsKey(columnIndex);
        }

        public IEnumerable<KeyValuePair<Cell, Brush>> GetCellBrushes()
        {
            return _columnBrushes.ToDictionary(s => GetCell(s.Key), s => s.Value);
        }

        public Brush GetColor()
        {
            return _columnBrushes.First().Value;
        }

        public Brush GetColor(int columnIndex)
        {
            return _columnBrushes[columnIndex];
        }

        public void SetColor(Brush brush)
        {
            for (int column = 0; column < _columnCount; column++)
            {
                _columnBrushes[column] = brush;
            }
        }

        public void SetColor(int columnIndex, Brush brush)
        {
            _columnBrushes[columnIndex] = brush;
        }

        public void ResetColor()
        {
            _columnBrushes.Clear();
        }

        public void ResetColor(int columnIndex)
        {
            _columnBrushes.Remove(columnIndex);
        }

        Cell GetCell(int columnIndex)
        {
            _cell.RowIndex = _rowIndex;
            _cell.ColumnIndex = columnIndex;
            return _cell;
        }

        public bool Equals(RowStyle other)
        {
            if (_rowIndex == other.RowIndex)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return _rowIndex;
        }
    }
}
