using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MasudaManager.Utility
{
    public class NullQueryResultBindingList : DynamicSortableBindingList<SqlResult, QueryResult>
    {
        SqlResult _result = new SqlResult();
        List<SqlResult> _list = new List<SqlResult>();

        static NullQueryResultBindingList _instance = new NullQueryResultBindingList(null);
        static NullQueryResultBindingList() { }
        private NullQueryResultBindingList(ISynchronizeInvoke syncObject)
            : base(syncObject)
        {
        }
        public static NullQueryResultBindingList Instance { get { return _instance; } }

        public override int RowCount { get { return 0; } }

        public override void AddSource(SqlResult source) { }

        public override void AddSourceRange(IEnumerable<SqlResult> sourceItems) { }

        public override QueryResult CreateEmptyResult() { return null; }

        public override QueryResult CreateResultWithValues(IEnumerable<object> orderedValues) { return null; }

        public override void Filter(int columnIndex, string filterString) { }

        public override string GetFieldValue(Cell cell) { return null; }

        public override string GetFieldValue(int rowIndex, int columnIndex) { return null; }

        public override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors) { return PropertyDescriptorCollection.Empty; }

        public override string GetListName(System.ComponentModel.PropertyDescriptor[] listAccessors) { return null; }

        public override SqlResult GetSourceAt(int index) { return _result; }

        public override void SetFieldValue(Cell cell, string value) { }

        public override void SetFieldValue(int rowIndex, int columnIndex, string value) { }

        public override List<SqlResult> ToSourceList() { return _list; }

        public override List<SqlResult> ToSavedSourceList() { return _list; }
    }
}
