using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MasudaManager.Controls.Base
{
    public partial class XTabControl : TabControl
    {
        public event EventHandler TabCloseButtonClick;

        readonly int _closeButtonWidth = 14;
        readonly int _closeButtonHeight = 14;
        readonly int _closeButtonAdjustX = -20;
        readonly int _closeButtonAdjustY = 2;
        object _currentGuid = null;
        TabPage _tabMoveStartTab = null;
        Point _tabMoveStartPoint;

        public XTabControl()
        {
            InitializeComponent();
            this.ShowCloseButton = false;
            this.AllowUserTabMove = false;
        }

        public bool ShowCloseButton { get; set; }
        public bool AllowUserTabMove { get; set; }
        public XTabPage SelectedXTab
        {
            get { return this.SelectedTab as XTabPage; }
        }

        #region WndProc [DebuggerStepThrough]
        [DebuggerStepThrough]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case 15:
                    if (this.ShowCloseButton)
                        DrawTabCloseButton();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Events override
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);

            if (this.AllowUserTabMove)
                drgevent.Effect = GetTabMoveDragDropEffects(drgevent);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);

            if (this.AllowUserTabMove)
                drgevent.Effect = GetTabMoveDragDropEffects(drgevent);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            if (this.AllowUserTabMove)
                EndTabDragDrop(drgevent);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (this.AllowUserTabMove)
                BeginTabMove(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.AllowUserTabMove)
                BeginTabDragDrop(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (this.AllowUserTabMove)
                EndTabMove();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (this.ShowCloseButton && IsCloseButtonClicked(e))
                OnCloseButtonClick(e);
        }
        #endregion

        #region Tab close button
        protected void OnCloseButtonClick(EventArgs e)
        {
            if (TabCloseButtonClick != null)
                TabCloseButtonClick(_currentGuid, e);
        }

        void DrawTabCloseButton()
        {
            Graphics graphics = this.CreateGraphics();

            for (int i = 0; i < this.TabPages.Count; i++)
            {
                Rectangle rectToDraw = GetTabCloseButtonRectByIndex(i);
                ControlPaint.DrawCaptionButton(graphics, rectToDraw, CaptionButton.Close, ButtonState.Flat);
            }

            graphics = null;
        }

        bool IsCloseButtonClicked(MouseEventArgs e)
        {
            return !GetTabCloseButtonRect(e.Location).Equals(Rectangle.Empty);
        }

        Rectangle GetTabCloseButtonRect(Point point)
        {
            for (int i = 0; i < this.TabCount; i++)
            {
                Rectangle closeButtonRect = GetTabCloseButtonRectByIndex(i);
                if (closeButtonRect.Contains(point))
                {
                    _currentGuid = ((XTabPage)this.TabPages[i]).Guid; //this.TabPages[i].Tag;
                    return closeButtonRect;
                }
            }

            return Rectangle.Empty;
        }

        Rectangle GetTabCloseButtonRectByIndex(int index)
        {
            Rectangle closeButtonRect = this.GetTabRect(index);
            closeButtonRect.X = closeButtonRect.Right + _closeButtonAdjustX;
            closeButtonRect.Y = closeButtonRect.Top + _closeButtonAdjustY;
            closeButtonRect.Width = _closeButtonWidth;
            closeButtonRect.Height = _closeButtonHeight;

            return closeButtonRect;
        }
        #endregion

        #region Tab move
        DragDropEffects GetTabMoveDragDropEffects(DragEventArgs e)
        {
            if (CanTabDragMove(e))
                return DragDropEffects.Move;
            
            return DragDropEffects.None;
        }

        bool CanTabDragMove(DragEventArgs e)
        {
            if (!DraggingDataEqualsTab(e))
                return false;

            if (!CanDragTabToPoint(new Point(e.X, e.Y)))
                return false;

            return true;
        }

        bool DraggingDataEqualsTab(DragEventArgs e)
        {
            return e.Data.GetData(_tabMoveStartTab.GetType()) == _tabMoveStartTab;
        }

        bool CanDragTabToPoint(Point point)
        {
            return GetTabPageFromPoint(this.PointToClient(point)) != null;
        }

        void BeginTabMove(MouseEventArgs e)
        {
            _tabMoveStartPoint = e.Location;
            _tabMoveStartTab = GetTabPageFromPoint(e.Location);
        }

        void EndTabMove()
        {
            _tabMoveStartPoint = Point.Empty;
            _tabMoveStartTab = null;
        }

        void BeginTabDragDrop(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (_tabMoveStartTab == null)
                return;

            if (_tabMoveStartPoint == e.Location)
                return;

            this.DoDragDrop(_tabMoveStartTab, DragDropEffects.Move);
        }

        void EndTabDragDrop(DragEventArgs e)
        {
            TabPage tabMoveEndTab = GetTabPageFromPoint(this.PointToClient(new Point(e.X, e.Y)));
            int startTabIndex = GetTabIndex(_tabMoveStartTab);
            int endTabIndex = GetTabIndex(tabMoveEndTab);

            if (startTabIndex == endTabIndex)
                return;

            this.SuspendLayout();

            if (startTabIndex < endTabIndex)
                ArrangeTabOrderForward(startTabIndex, endTabIndex);
            else
                ArrangeTabOrderBackward(startTabIndex, endTabIndex);

            this.TabPages[endTabIndex] = _tabMoveStartTab;
            this.SelectedTab = _tabMoveStartTab;

            this.ResumeLayout();
        }

        void ArrangeTabOrderForward(int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                this.TabPages[i] = this.TabPages[i + 1];
            }
        }

        void ArrangeTabOrderBackward(int startIndex, int endIndex)
        {
            for (int i = startIndex; endIndex < i; i--)
            {
                this.TabPages[i] = this.TabPages[i - 1];
            }
        }
        #endregion

        #region GetTabFromPoint
        protected TabPage GetTabPageFromPoint(Point point)
        {
            for (int i = 0; i < this.TabCount; i++)
            {
                if (this.GetTabRect(i).Contains(point))
                    return this.TabPages[i];
            }

            return null;
        }
        #endregion

        #region GetTabIndex
        protected int GetTabIndex(TabPage tabPage)
        {
            for (int i = 0; i < this.TabCount; i++)
            {
                if (this.TabPages[i] == tabPage)
                    return i;
            }

            return -1;
        }
        #endregion
    }
}
