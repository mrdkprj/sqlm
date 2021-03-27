using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Drawing;
using System;

namespace MasudaManager.Controls
{
    public class CaretCreator : NativeWindow
    {
        //http://www.codeproject.com/Questions/63019/Custom-Caret-for-WinForms-richtextbox
        [DllImport("user32.dll")]
        static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);

        const int WM_USER = 0x0400;
        const int WM_NOTIFY = 0x004E;
        const int WM_REFLECT = WM_USER + 0x1C00;
        const int WM_PAINT = 0xF;
        const int WM_SETFOCUS = 0x0007;
        const int WM_KILLFOCUS = 0x0008;

        readonly TextFormatFlags _formatFlags = TextFormatFlags.NoPadding | TextFormatFlags.Left | TextFormatFlags.PreserveGraphicsClipping;

        Bitmap _currentCaret = null;
        Bitmap _insertCaret = null;
        Bitmap _overwriteCaret = null;
        XRichTextBox _richText = null;
        bool _focused = false;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_SETFOCUS)
                _focused = true;
            else if (m.Msg == WM_KILLFOCUS)
                _focused = false;

            if (_focused)
            {
                if ((m.Msg == (WM_REFLECT + WM_NOTIFY)) || (m.Msg == WM_PAINT) || (m.Msg == WM_SETFOCUS))
                {
                    ShowCurrentCaret();
                }
            }
        }

        public CaretCreator(XRichTextBox richtext)
        {
            this.AssignHandle(richtext.Handle);
            _richText = richtext;
            CreateInsertCaret();
        }

        void ShowCurrentCaret()
        {
            if (_richText.IsOverwriteMode)
                CreateOverwriteCaret();
            else
                CreateInsertCaret();

            CreateCaret(this.Handle, _currentCaret.GetHbitmap(), 0, 0);
            ShowCaret(this.Handle);
        }

        void CreateInsertCaret()
        {
            Size fontSize = GetFontSize();
            _insertCaret = new Bitmap(1, fontSize.Height);
            _currentCaret = _insertCaret;
        }

        void CreateOverwriteCaret()
        {
            Size fontSize = GetFontSize();
            _overwriteCaret = new Bitmap(fontSize.Width, fontSize.Height);
            _currentCaret = _overwriteCaret;
        }


        string GetTargetString()
        {
            int startPosition = _richText.SelectionStart;
            int endPosition = _richText.Text.Length;

            if (endPosition == 0 || startPosition >= endPosition)
                return null;
            else
                return _richText.Text.Substring(startPosition, 1);
        }

        Size GetFontSize()
        {
            Graphics g = Graphics.FromImage(new Bitmap(10,10));

            SizeF measuredSize = TextRenderer.MeasureText(
                                                            g, GetTargetString(),
                                                            _richText.Font,
                                                            Size.Round(g.ClipBounds.Size),
                                                            _formatFlags
                                                        );
            g.Dispose();

            if (measuredSize.Width == 0 || measuredSize.Height == 0)
                measuredSize = new SizeF(1, _richText.Font.Height);

            return Size.Round(measuredSize);
        }
    }
}
