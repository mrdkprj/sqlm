using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public class ItemInsertedEventArgs : EventArgs
    {
        public ItemInsertedEventArgs(int rowIndex)
        {
            this.RowIndex = rowIndex;
        }

        public int RowIndex { get; set; }
    }
}
