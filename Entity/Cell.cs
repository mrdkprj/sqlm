using System;

namespace MasudaManager
{
    public struct Cell : IEquatable<Cell>, IComparable<Cell>
    {
        public static readonly Cell Empty = new Cell(-1, -1);
        public int ColumnIndex;
        public int RowIndex;

        public Cell(int columnIndex, int rowIndex)
        {
            this.ColumnIndex = columnIndex;
            this.RowIndex = rowIndex;
        }

        public bool Equals(Cell other)
        {
            if (this.ColumnIndex == other.ColumnIndex && this.RowIndex == other.RowIndex)
                return true;

            return false;
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(Cell left, Cell right)
        {
            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, null))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            return this.Equals((Cell)obj);
        }

        public override int GetHashCode()
        {
            //return this.ColumnIndex ^ this.RowIndex;
            //int hash = 17;
            //hash = hash * 23 + this.ColumnIndex.GetHashCode();
            //hash = hash * 23 + this.RowIndex.GetHashCode();
            //return hash;
            return base.GetHashCode();
        }

        public int CompareTo(Cell other)
        {
            if (this.RowIndex > other.RowIndex)
                return 1;

            if (this.RowIndex < other.RowIndex)
                return -1;

            if (this.ColumnIndex > other.ColumnIndex)
                return 1;

            if (this.ColumnIndex < other.ColumnIndex)
                return -1;

            return 0;
        }
    
        public override string ToString()
        {
            return "Column:" + this.ColumnIndex + ", Row:" + this.RowIndex;
        }
    }
}
