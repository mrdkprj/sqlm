using MasudaManager.Utility;
using MasudaManager.Utility.Preference;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MasudaManager.Controls
{
    [ToolboxItem(false)]
    public partial class SqlInputAssistant : XDataGridView, IMsdControl
    {
        public event EventHandler BackSpaceKeyEnter;
        public event EventHandler EscapeKeyEnter;
        public event EventHandler ItemSelected;

        readonly int _adjustFontSize = -1;
        readonly int _defaultUserHelpHeight = 150;
        readonly int _adjustUserHelpY = 65;
        readonly int _adjustUserHelpX = 15;
        readonly int _adjustUserHelpWidth = 20;
        SqlInputView _ownerView;
        int _adjustedWidth = 0;

        public SqlInputAssistant()
        {
            InitializeComponent();

            this.Font = new Font(UserPreference.Setting.Input.Font.FontFamily, UserPreference.Setting.Input.Font.Size + _adjustFontSize);
            this.RowHeadersVisible = false;
            this.ColumnHeadersVisible = false;
            this.AllowDrop = false;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.Visible = false;
            this.TabStop = false;
        }

        #region Events override
        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {          
            base.OnDataBindingComplete(e);

            if (this.Visible)
                PrepareDisplay();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible)
                PrepareDisplay();
            else
                this.Height = _defaultUserHelpHeight;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            OnItemSelected(this, EventArgs.Empty);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                case Keys.Tab:
                    e.SuppressKeyPress = true;
                    OnItemSelected(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                case Keys.Back:
                    e.SuppressKeyPress = true;
                    OnBackSpaceKeyEnter(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
                case Keys.Escape:
                    e.SuppressKeyPress = true;
                    OnEscapeKeyEnter(this, EventArgs.Empty);
                    e.Handled = true;
                    return;
            }

            base.OnKeyDown(e);
        }
        #endregion

        #region Events
        void OnItemSelected(object sender, EventArgs e)
        {
            if (this.ItemSelected != null)
                this.ItemSelected(sender, e);
        }

        void OnBackSpaceKeyEnter(object sender, EventArgs e)
        {
            if (this.BackSpaceKeyEnter != null)
                this.BackSpaceKeyEnter(sender, e);
        }

        void OnEscapeKeyEnter(object sender, EventArgs e)
        {
            if (this.EscapeKeyEnter != null)
                this.EscapeKeyEnter(sender, e);
        }
        #endregion

        #region SetOwnerView
        public void SetOwnerView(SqlInputView ownerView)
        {
            _ownerView = ownerView;
        }
        #endregion

        #region PrepareDisplay
        void PrepareDisplay()
        {
            if (_ownerView == null)
                return;

            this.SuspendLayout();
            AdjustLocation();
            AdjustSize();
            this.ResumeLayout();

            this.Height = this.Rows.GetRowsHeight(DataGridViewElementStates.Displayed);
        }

        void AdjustLocation()
        {
            this.Left = _ownerView.PointXFromPosition(_ownerView.CurrentPosition) + _adjustUserHelpX;
            this.Top = _ownerView.PointYFromPosition(_ownerView.CurrentPosition) + _adjustUserHelpY;
        }

        void AdjustSize()
        {
            _adjustedWidth = 0;
            _adjustedWidth += this.Columns[(int)TableDataColumn.Name].Width;
            _adjustedWidth += this.Columns[(int)TableDataColumn.Comment].Width;
            _adjustedWidth += _adjustUserHelpWidth;
            this.Width = _adjustedWidth;

            this.Height = _defaultUserHelpHeight;
        }
        #endregion

        #region ApplyPreference
        public void ApplyPreference()
        {
            if (!this.Font.Equals(UserPreference.Setting.Input.Font))
                this.Font = UserPreference.Setting.Input.Font;
        }
        #endregion
    }
}
