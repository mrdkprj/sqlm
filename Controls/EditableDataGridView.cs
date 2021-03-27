using MasudaManager.Utility;
using System;
using System.Windows.Forms;

namespace MasudaManager.Controls
{
    public partial class EditableDataGridView : XDataGridView
    {       
        DataGridViewPaintParts _paintParts;
        IGridViewEditingCoordinator _coordinator = new GridViewEditingCoordinator();
        bool _bindComplete = false;

        public EditableDataGridView()
        {
            InitializeComponent();
            this.Disposed += EditableDataGridView_Disposed;
        }

        void EditableDataGridView_Disposed(object sender, EventArgs e)
        {
            _coordinator.Release();
        }

        public IGridViewEditingCoordinator EditingCoordinator { get { return _coordinator; } }  

        #region Events override
        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);

            if (!_bindComplete)
            {
                _coordinator.SetDataSource(this.DataSource as QueryResultBindingList);
                _bindComplete = true;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (this.IsCurrentCellDirty)
                this.EndEdit();
        }

        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            if (!IsCellEditable(e.ColumnIndex, e.RowIndex))
            {
                e.Cancel = true;
                return;
            }

            base.OnCellBeginEdit(e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (!_coordinator.IsDataBound)
                return; 
            
            if (IsTopLeftCellClicked(e))
                _coordinator.SelectAllCells();
        }

        protected override void OnCurrentCellChanged(EventArgs e)
        {
            base.OnCurrentCellChanged(e);

            if (!_coordinator.IsDataBound)
                return;

            if (this.CurrentCell == null)
                _coordinator.SetCurrentCell(Cell.Empty);
            else
                _coordinator.SetCurrentCell(new Cell(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            if (e.KeyData == (Keys.Control | Keys.A))
            {
                if (!_coordinator.IsAllSelected)
                    _coordinator.SelectAllCells();
            }
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);

            if (!_coordinator.IsDataBound)
                return;

            if (_coordinator.SuppressCellSelectionChange)
                return;

            if (this.AreAllCellsSelected(false))
                return;

            if (this.SelectedRows.Count > 0)
                _coordinator.SelectCellRange(this.FirstSelectedCell, GetLastEnteredCell());
            else
                _coordinator.SelectCellRange(this.FirstSelectedCell, this.LastSelectedCell);
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);

            if (PaintRequired(e))
                PaintCellBackColor(e);
        }
        #endregion

        #region Methods
        public void FocusOnCurrentCel()
        {
            if (!this[_coordinator.CurrentCell.ColumnIndex, _coordinator.CurrentCell.RowIndex].Displayed)
                ScrollToCurrentRow(_coordinator.CurrentCell);
        }

        void ScrollToCurrentRow(Cell cell)
        {
            if (cell.RowIndex > this.FirstDisplayedCell.RowIndex)
                this.FirstDisplayedScrollingRowIndex = cell.RowIndex - this.DisplayedRowCount(false) + 1;
            else
                this.FirstDisplayedScrollingRowIndex = cell.RowIndex;
        }

        bool IsTopLeftCellClicked(EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            if (this.HitTest(args.X, args.Y).Type == DataGridViewHitTestType.TopLeftHeader)
                return true;

            return false;
        }

        bool IsCellEditable(int columnIndex, int rowIndex)
        {
            if (!_coordinator.IsDataBound)
                return false;

            return _coordinator.CanEditCell(columnIndex, rowIndex);
        }

        Cell GetLastEnteredCell()
        {
            Cell endCell = this.GetMouseEnterCell();
            endCell.ColumnIndex = _coordinator.ColumnCount - 1;

            return endCell;
        }

        bool PaintRequired(DataGridViewCellPaintingEventArgs e)
        {
            if (!_coordinator.IsDataBound)
                return false;

            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return false;

            if (!e.State.HasFlag(DataGridViewElementStates.Displayed))
                return false; 
            
            if (!e.PaintParts.HasFlag(DataGridViewPaintParts.Background))
                return false;
            
            if (e.State.HasFlag(DataGridViewElementStates.Selected))
                return false;

            return true;
        }

        void PaintCellBackColor(DataGridViewCellPaintingEventArgs e)
        {
            if (!_coordinator.BrushExists(e.ColumnIndex, e.RowIndex))
                return;

            e.Graphics.FillRectangle(_coordinator.GetBrush(e.ColumnIndex, e.RowIndex), e.CellBounds);
            _paintParts = e.PaintParts & ~DataGridViewPaintParts.Background;
            e.Paint(e.ClipBounds, _paintParts);
            e.Handled = true;
        }
        #endregion
    }
}
