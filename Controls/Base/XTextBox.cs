using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasudaManager.Controls
{
    public partial class XTextBox : TextBox
    {
        bool _alreadyFocused = false;

        public XTextBox()
        {
            InitializeComponent();
            this.AllowAutoSelectAll = false;
            //JoinEvents(false);
        }

        public bool AllowAutoSelectAll { get; set; }

        #region Events override
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            
            if (this.AllowAutoSelectAll)
                SelectAllOnFocused();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            if (this.AllowAutoSelectAll)
                SelectAllOnMouseUp();
        }
        
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (this.AllowAutoSelectAll)
                DeSelectAllText();

        }
        #endregion

        #region Methods
        void SelectAllOnFocused()
        {
            if (MouseButtons != MouseButtons.None)
                return;

            SelectAllText();
        }

        void SelectAllOnMouseUp()
        {
            if (_alreadyFocused || this.SelectionLength > 0)
                return;

            SelectAllText();
        }

        void SelectAllText()
        {
            _alreadyFocused = true;
            this.SelectAll();
        }

        void DeSelectAllText()
        {
            _alreadyFocused = false;
        }
        #endregion

        #region watermark

        //private Font oldFont = null;
        //private Boolean waterMarkTextEnabled = false;

        //private Color _waterMarkColor = Color.Gray;

        //public Color WaterMarkColor
        //{
        //    get { return _waterMarkColor; }
        //    set { _waterMarkColor = value; Invalidate();/*thanks to Bernhard Elbl
        //                                                    for Invalidate()*/ }
        //}

        //private string _waterMarkText = "Water Mark";
        //public string WaterMarkText
        //{
        //    get { return _waterMarkText; }
        //    set { _waterMarkText = value; Invalidate(); }
        //}

        ////Override OnCreateControl ... thanks to  "lpgray .. codeproject guy"
        //protected override void OnCreateControl() 
        //{ 
        //    base.OnCreateControl();
        //    WaterMark_Toggel(null, null); 
        //}
        
        ////Override OnPaint
        //protected override void OnPaint(PaintEventArgs args)
        //{
        //    // Use the same font that was defined in base class
        //    System.Drawing.Font drawFont = new System.Drawing.Font(Font.FontFamily,
        //        Font.Size, Font.Style, Font.Unit);
        //    //Create new brush with gray color or 
        //    SolidBrush drawBrush = new SolidBrush(WaterMarkColor);//use Water mark color
        //    //Draw Text or WaterMark
        //    args.Graphics.DrawString((waterMarkTextEnabled ? WaterMarkText : Text),
        //        drawFont, drawBrush, new PointF(0.0F, 0.0F));
        //    base.OnPaint(args);
        //}

        //private void JoinEvents(Boolean join)
        //{
        //    if (join)
        //    {
        //        this.TextChanged += new System.EventHandler(this.WaterMark_Toggel);
        //        this.LostFocus += new System.EventHandler(this.WaterMark_Toggel);
        //        this.FontChanged += new System.EventHandler(this.WaterMark_FontChanged);
        //        //No one of the above events will start immeddiatlly 
        //        //TextBox control still in constructing, so,
        //        //Font object (for example) couldn't be catched from within
        //        //WaterMark_Toggle
        //        //So, call WaterMark_Toggel through OnCreateControl after TextBox
        //        //is totally created
        //        //No doupt, it will be only one time call
                
        //        //Old solution uses Timer.Tick event to check Create property
        //    }
        //}

        //private void WaterMark_Toggel(object sender, EventArgs args )
        //{
        //    if (this.Text.Length <= 0)
        //        EnableWaterMark();
        //    else
        //        DisbaleWaterMark();
        //}

        //private void EnableWaterMark()
        //{
        //    //Save current font until returning the UserPaint style to false (NOTE:
        //    //It is a try and error advice)
        //    oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style,
        //       Font.Unit);
        //    //Enable OnPaint event handler
        //    this.SetStyle(ControlStyles.UserPaint, true);
        //    this.waterMarkTextEnabled = true;
        //    //Triger OnPaint immediatly
        //    Refresh();
        //}

        //private void DisbaleWaterMark()
        //{
        //    //Disbale OnPaint event handler
        //    this.waterMarkTextEnabled = false;
        //    this.SetStyle(ControlStyles.UserPaint, false);
        //    //Return back oldFont if existed
        //    if(oldFont != null)
        //        this.Font = new System.Drawing.Font(oldFont.FontFamily, oldFont.Size,
        //            oldFont.Style, oldFont.Unit);
        //}

        //private void WaterMark_FontChanged(object sender, EventArgs args)
        //{
        //    if (waterMarkTextEnabled)
        //    {
        //        oldFont = new System.Drawing.Font(Font.FontFamily,Font.Size,Font.Style,
        //            Font.Unit);
        //        Refresh();
        //    }
        //}
    
        #endregion
    }
}
