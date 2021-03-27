using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MasudaManager.Utility.Preference;
using System.Collections;
using MasudaManager.Utility;
using System.Reflection;

namespace MasudaManager.Controls
{
    public partial class XDataGridView : DataGridView
    {
        SqlResultViewTextDrawer _drawer = new SqlResultViewTextDrawer();
        Rectangle _rowNumberRectangle = new Rectangle();
        FieldInfo _anchorCellField = null;
        FieldInfo _mouseEnterCellField = null;
        Point _cellPoint = new Point(0, 0);

        const string PT_ANCHOR_CELL = "ptAnchorCell";
        const string PT_MOUSE_ENTERED_CELL = "ptMouseEnteredCell";
        readonly Graphics _graphicsForMeasureString = Graphics.FromImage(new Bitmap(2, 2));
        readonly int _rowHeaderIconWidth = 17;
        readonly int _columnWidthPadding = -5;
        bool _columnsWidthAdjustedToAllCells = false;
        bool _columnsWidthResizing = false;
        bool _suppressResizeColumnsWidth = false;

        #region Constructor
        public XDataGridView()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.DisplayRowNumber = false;
            this.DisplaySpaceCharacter = false;
            this.AllowRightClickCellSelect = false;
            this.ForcePlainTextCopy = true;
            this.ThrowOnDataError = false;
            this.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            this.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.DefaultColumnWidth = 100;
            _anchorCellField = typeof(DataGridView).GetField(PT_ANCHOR_CELL, BindingFlags.NonPublic | BindingFlags.Instance);
            _mouseEnterCellField = typeof(DataGridView).GetField(PT_MOUSE_ENTERED_CELL, BindingFlags.NonPublic | BindingFlags.Instance);
        }
        #endregion

        #region Property
        public bool DisplayRowNumber { get; set; }
        public bool DisplaySpaceCharacter { get; set; }
        public bool AllowRightClickCellSelect { get; set; }
        public bool ForcePlainTextCopy { get; set; }
        public int DefaultColumnWidth { get; set; }
        public bool ThrowOnDataError { get; set; }
        public Cell FirstSelectedCell
        {
            get
            {
                if (this.GetCellCount(DataGridViewElementStates.Selected) > 0)
                    return GetAnchorCell();
                else
                    return Cell.Empty;
            }
        }

        public Cell LastSelectedCell
        {
            get
            {
                if (this.AreAllCellsSelected(false))
                    return GetRightBottomCell();

                return GetCurrentCell();
            }
        }
        public int SelectedRowCount { get { return GetSelectedRowCount(); } }
        public int SelectedColumnCount { get { return GetSelectedColumnCount(); } }
        #endregion

        #region Events override
        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);

            if (e.ListChangedType == ListChangedType.Reset)
                _columnsWidthAdjustedToAllCells = false;
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            if (this.ThrowOnDataError)
            {
                e.Cancel = true;
                e.ThrowException = true;
            }
            else
            {
                base.OnDataError(displayErrorDialogIfNoHandler, e);
            }
        }

        protected override void OnColumnDividerDoubleClick(DataGridViewColumnDividerDoubleClickEventArgs e)
        {
            if (this.AreAllCellsSelected(true))
                ResizeAllColumnsWidth();
            else
                base.OnColumnDividerDoubleClick(e);
        }
        
        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            if (this.AreAllCellsSelected(true) && !_columnsWidthResizing)
                ResizeAllColumnsWidthByFixedSize(e.Column.Width);
            else
                base.OnColumnWidthChanged(e);
        }

        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseDown(e);

            if (this.AllowRightClickCellSelect && e.Button == System.Windows.Forms.MouseButtons.Right)
                SetRightClickCurrentCell(new Cell(e.ColumnIndex, e.RowIndex));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            switch (e.KeyData)
            {
                case Keys.Control | Keys.C:
                    if (this.ForcePlainTextCopy)
                    {
                        CopyPlainText();
                        e.Handled = true;
                    }
                    break;
            }
        }

        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            base.OnRowPostPaint(e);
            
            if (this.DisplayRowNumber && e.State.HasFlag(DataGridViewElementStates.Displayed))
                PaintRowNumber(e);
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);

            if (this.DisplaySpaceCharacter)
                _drawer.DrawSpaceCharacter(IsPropotionalFont(this.Font), e);
        }

        #endregion

        #region Cell
        public Cell GetCurrentCell()
        {
            return new Cell(this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex);
        }

        protected Cell GetAnchorCell()
        {
            if (this.GetCellCount(DataGridViewElementStates.Selected) <= 1)
                return GetCurrentCell();

            _cellPoint = (Point)_anchorCellField.GetValue(this);
            return new Cell(_cellPoint.X, _cellPoint.Y);
        }

        protected Cell GetRightBottomCell()
        {
            return new Cell(this.ColumnCount - 1, this.RowCount - 1);
        }

        protected Cell GetMouseEnterCell()
        {
            if (this.GetCellCount(DataGridViewElementStates.Selected) <= 1)
                return GetCurrentCell();

            _cellPoint = (Point)_mouseEnterCellField.GetValue(this);
            return new Cell(_cellPoint.X, _cellPoint.Y);
        }

        int GetSelectedRowCount()
        {
            return (this.LastSelectedCell.RowIndex - this.FirstSelectedCell.RowIndex) + 1;
        }

        int GetSelectedColumnCount()
        {
            return (this.LastSelectedCell.ColumnIndex - this.FirstSelectedCell.ColumnIndex) + 1;
        }
        #endregion

        #region CopyPlainText
        public void CopyPlainText()
        {
            try
            {
                Clipboard.SetDataObject(this.GetClipboardContent());
                Clipboard.SetDataObject(Clipboard.GetData(DataFormats.UnicodeText));
            }
            catch
            {
            }
        }
        #endregion

        #region Force AutoResizeColumnsWidth
        public void ForceAutoResizeColumnsWidth(DataGridViewAutoSizeColumnMode autoSizeMode)
        {
            _suppressResizeColumnsWidth = true;

            if (CanResizeColumnWidthByAutoSizeMode(autoSizeMode))
                ResizeAllColumnsWidthByAutoSizeMode(autoSizeMode);
            else
                ResizeAllColumnsWidthByDividedSize();

            _suppressResizeColumnsWidth = false;
        }

        bool CanResizeColumnWidthByAutoSizeMode(DataGridViewAutoSizeColumnMode autoSizeMode)
        {
            switch (autoSizeMode)
            {
                case DataGridViewAutoSizeColumnMode.Fill:
                case DataGridViewAutoSizeColumnMode.None:
                case DataGridViewAutoSizeColumnMode.NotSet:
                    return false;
                default:
                    return true;
            }
        }

        public void ResizeAllColumnsWidth()
        {
            if (_suppressResizeColumnsWidth)
                return;

            _columnsWidthResizing = true;

            this.AutoResizeColumns(GetAutoSizeColumnsMode());

            _columnsWidthResizing = false;
        }

        DataGridViewAutoSizeColumnsMode GetAutoSizeColumnsMode()
        {
            if (_columnsWidthAdjustedToAllCells)
            {
                _columnsWidthAdjustedToAllCells = false;
                return DataGridViewAutoSizeColumnsMode.ColumnHeader;
            }
            else
            {
                _columnsWidthAdjustedToAllCells = true;
                return DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            }
        }

        void ResizeAllColumnsWidthByFixedSize(int width)
        {

            if (_suppressResizeColumnsWidth || this.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.None)
                return;

            _columnsWidthResizing = true;

            foreach (DataGridViewColumn column in this.Columns)
            {
                column.Width = width;
            }

            this.Invalidate();

            _columnsWidthResizing = false;
        }

        void ResizeAllColumnsWidthByAutoSizeMode(DataGridViewAutoSizeColumnMode autoSizeMode)
        {
            for (int columnIndex = 0; columnIndex < this.Columns.Count; columnIndex++)
            {
                this.Columns[columnIndex].Width = GetOptimalWidth(columnIndex, autoSizeMode);
            }
        }

        int GetOptimalWidth(int columnIndex, DataGridViewAutoSizeColumnMode autoSizeMode)
        {
            int preferedWidth = this.Columns[columnIndex].GetPreferredWidth(autoSizeMode, false);

            if (preferedWidth < this.DefaultColumnWidth)
                return preferedWidth;
            else
                return this.DefaultColumnWidth;
        }
        
        void ResizeAllColumnsWidthByDividedSize()
        {
            for (int columnIndex = 0; columnIndex < this.Columns.Count; columnIndex++)
            {
                this.Columns[columnIndex].Width = GetEquallyDividedColumnWidth();
            }

            this.Refresh();
        }

        int GetEquallyDividedColumnWidth()
        {
            return (this.Width / this.Columns.Count) + (_columnWidthPadding * this.Columns.Count);
        }

        #endregion 

        #region Set right-clicked current cell
        void SetRightClickCurrentCell(Cell cell)
        {
            if (cell.ColumnIndex < 0 || cell.RowIndex < 0)
                return;

            this.CurrentCell = this[cell.ColumnIndex, cell.RowIndex];
            this.ClearSelection();
            this[cell.ColumnIndex, cell.RowIndex].Selected = true;
        }
        #endregion

        #region Validate cell value
        public bool ValidateCellValue(int columnindex, int rowindex, object value)
        {
            try
            {
                Convert.ChangeType(value, this[columnindex, rowindex].ValueType);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Paint row number
        void PaintRowNumber(DataGridViewRowPostPaintEventArgs e)
        {
            //// Add 4 dots margin to the right end
            _rowNumberRectangle = new Rectangle
                (
                    e.RowBounds.Location.X,
                    e.RowBounds.Location.Y,
                    this.RowHeadersWidth - 4,
                    e.RowBounds.Height
                );

            TextRenderer.DrawText
                (
                    e.Graphics,
                    (e.RowIndex + 1).ToString(),
                    this.RowHeadersDefaultCellStyle.Font,
                    _rowNumberRectangle,
                    this.RowHeadersDefaultCellStyle.ForeColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right
                );
        }
        #endregion

        #region Adjust RowHeadersWidth to row number
        public void AdjustRowHeadersWidthToRowNumber()
        {
            Size rowHeaderTextSize = TextRenderer.MeasureText(this.Rows.GetLastRow(DataGridViewElementStates.None).ToString(), this.RowHeadersDefaultCellStyle.Font);
            if (this.RowHeadersWidth < rowHeaderTextSize.Width + _rowHeaderIconWidth)
                this.RowHeadersWidth = rowHeaderTextSize.Width + _rowHeaderIconWidth;
        }
        #endregion

        #region Is font propotional
        bool IsPropotionalFont(Font font)
        {
            if (_graphicsForMeasureString.MeasureString("ai", font) == _graphicsForMeasureString.MeasureString("am", font))
                return false;

            return true;
        }
        #endregion
    }
}

