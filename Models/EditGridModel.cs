using MasudaManager.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MasudaManager
{
    public class EditResultModel : IModel
    {
        readonly Brush _updateCellColor = Brushes.Moccasin;
        readonly Brush _deleteRowColor = Brushes.Gray;
        readonly Brush _insertRowColor = Brushes.AliceBlue;
        
        public EditResultModel()
        {
            this.SearchContext = new SearchContext();
            this.SearchViewRequestData = new SearchViewRequestData();
            this.InsertPosition = EditInsertPositionType.CurrentRow;
            this.EditColor = new EditingColor();
            this.EditColor.UpdateColor = _updateCellColor;
            this.EditColor.DeleteColor = _deleteRowColor;
            this.EditColor.InsertColor = _insertRowColor;
            this.ApplyingCells = new Queue<Cell>();
        }

        public bool HasApplyError { get; set; }
        public string SourceSql { get; set; }
        public string TableName { get; set; }
        public KeyColumnData KeyColumnData { get; set; }
        public List<string> HeaderColumnNames { get; set; }
        public List<Type> HeaderColumnTypes { get; set; }
        public DynamicSortableBindingList<SqlResult, QueryResult> BindingList { get; set; }
        public EditInsertPositionType InsertPosition { get; set; }
        public EditingColor EditColor { get; set; }
        public SearchContext SearchContext { get; set; }
        public SearchViewRequestData SearchViewRequestData { get; set; }
        public ISqlTaskManager SqlTaskManager { get; set; }
        public Queue<Cell> ApplyingCells { get; private set; }
        public Action OnQueryCancelled { get; set; }

        public void ReleaseModel()
        {
        }
    }
}
