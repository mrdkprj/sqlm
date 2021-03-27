using System.Collections.Generic;

namespace MasudaManager
{
    public class GridPasteData
    {
        public GridPasteData()
        {
            this.RowValues = new List<object>();
            this.CellPositions = new List<Cell>();
        }

        public List<object> RowValues { get; set; }
        public List<Cell> CellPositions { get; set; }
    }
}
