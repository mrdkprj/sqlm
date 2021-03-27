using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    class EditCounter
    {
        int _updateCount = 0;
        int _insertCount = 0;
        int _deleteCount = 0;

        public int UpdateCount { get { return _updateCount; } }
        public int InsertCount { get { return _insertCount; } }
        public int DeleteCount { get { return _deleteCount; } }

        public void Reset()
        {
            _updateCount = 0;
            _insertCount = 0;
            _deleteCount = 0;
        }

        public void Count(IEnumerable<EditData> editDataList)
        {
            foreach (var editData in editDataList)
            {
                switch (editData.Type)
                {
                    case EditType.Update:
                        foreach (var data in editData.Children)
                        {
                            CountUpdate();
                        }
                       break;
                    case EditType.Insert:
                       CountInsert();
                       break;
                    case EditType.Delete:
                       CountDelete();
                        break;
                    default:
                        throw new Exception("Invalid edit data type");
                }
            }
        }

        public void CountUpdate()
        {
            _updateCount++;
        }

        public void CountInsert()
        {
            _insertCount++;
        }

        public void CountDelete()
        {
            _deleteCount++;
        }

        public int GetTotalCount()
        {
            return _updateCount + _insertCount + _deleteCount;
        }
    }
}
