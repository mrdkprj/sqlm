using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MasudaManager.Controls
{
    class SqlResultViewTextDrawer
    {
        enum SpaceType
        {
            None,
            Hankaku,
            Zenkaku,
        }

        readonly string _hankakuSpace = "\u033A";
        readonly string _zenkakuSpace = "□";
        readonly string _propotionalZenkakuSpace = "[" + "]";
        readonly string _nullString = "NULL";
        TextFormatFlags _drawingFlags = TextFormatFlags.Left | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
        TextFormatFlags _measuringFlags = TextFormatFlags.NoPadding | TextFormatFlags.Left | TextFormatFlags.PreserveGraphicsClipping;
        bool _isPropotionalFont = false;
        SpaceType _spaceType;
        string _spaceString = null;
        StringBuilder _nonSpaceStringBuilder = new StringBuilder();
        float _spaceStringWidth = 0;

        public void DrawSpaceCharacter(bool isPropotionalFont, DataGridViewCellPaintingEventArgs e)
        {
            _isPropotionalFont = isPropotionalFont;

            if (!PaintRequired(e))
                return;

            e.PaintBackground(e.ClipBounds, true);

            // 描画領域外（行ヘッダー）を描画しないようClip設定
            e.Graphics.Clip = new Region(e.Graphics.VisibleClipBounds);

            DrawEntireText(e);

            _nonSpaceStringBuilder.Clear();

            RectangleF drawingRectangle = e.CellBounds;

            for (int i = 0; i < e.Value.ToStringOrEmpty().Length; i++)
            {
                string target = e.Value.ToStringOrEmpty().Substring(i, 1);

                _spaceType = GetSpaceType(target);

                if (_spaceType == SpaceType.None)
                    AppendNonSpaceString(target);
                else
                    drawingRectangle = DrawSpace(drawingRectangle, e);
            }

            e.Handled = true;
        }

        bool PaintRequired(DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return false;

            if ((e.State & DataGridViewElementStates.Displayed) != DataGridViewElementStates.Displayed)
                return false;

            if ((e.PaintParts & DataGridViewPaintParts.Background) != DataGridViewPaintParts.Background)
                return false;

            return true;
        }

        void DrawEntireText(DataGridViewCellPaintingEventArgs e)
        {
            if(e.Value == null)
                TextRenderer.DrawText(e.Graphics, _nullString, e.CellStyle.Font, Rectangle.Round(e.CellBounds), Color.Gray, _drawingFlags);
            else
                TextRenderer.DrawText(e.Graphics, e.Value.ToStringOrEmpty(), e.CellStyle.Font, Rectangle.Round(e.CellBounds), Color.Black, _drawingFlags);
        }

        SpaceType GetSpaceType(string text)
        {
            if (text == Constants.StringSpace)
                return SpaceType.Hankaku;

            if (text == Constants.String2ByteSpace)
                return SpaceType.Zenkaku;

            return SpaceType.None;
        }

        void SetDrawSpaceSetting(SpaceType spaceType, DataGridViewCellPaintingEventArgs e)
        {
            switch (spaceType)
            {
                case SpaceType.Hankaku:
                    SetHankakuSetting(e);
                    break;
                case SpaceType.Zenkaku:
                    SetZenkakuSetting(e);
                    break;
            }
        }

        void SetHankakuSetting(DataGridViewCellPaintingEventArgs e)
        {
            _spaceString = _hankakuSpace;
            SizeF hSpaceSize = TextRenderer.MeasureText(e.Graphics, Constants.StringSpace, e.CellStyle.Font, e.CellBounds.Size, _measuringFlags);
            _spaceStringWidth = hSpaceSize.Width;
        }

        void SetZenkakuSetting(DataGridViewCellPaintingEventArgs e)
        {
            if (_isPropotionalFont)
                _spaceString = _propotionalZenkakuSpace;
            else
                _spaceString = _zenkakuSpace;

            SizeF zSpaceSize = TextRenderer.MeasureText(e.Graphics, Constants.String2ByteSpace, e.CellStyle.Font, e.CellBounds.Size, _measuringFlags);
            _spaceStringWidth = zSpaceSize.Width;
        }

        void AppendNonSpaceString(string text)
        {
            _nonSpaceStringBuilder.Append(text);
        }

        RectangleF DrawSpace(RectangleF drawingRectangle, DataGridViewCellPaintingEventArgs e)
        {
            if (!drawingRectangle.IntersectsWith(e.CellBounds))
                return drawingRectangle; 
            
            RectangleF targetRectangle = new RectangleF(drawingRectangle.Location, drawingRectangle.Size);
           
            SetDrawSpaceSetting(_spaceType, e);

            SizeF nonSpaceSize = TextRenderer.MeasureText(e.Graphics, _nonSpaceStringBuilder.ToString(), e.CellStyle.Font, Size.Round(targetRectangle.Size), _measuringFlags);
            targetRectangle.X = targetRectangle.X + nonSpaceSize.Width;
            TextRenderer.DrawText(e.Graphics, _spaceString, e.CellStyle.Font, Rectangle.Round(targetRectangle), Color.Gray, _drawingFlags);
            targetRectangle.X += _spaceStringWidth;
            _nonSpaceStringBuilder.Clear();

            return targetRectangle;
        }

        public void DrawSpaceCharacterOldVersion(bool isPropotionalFont, DataGridViewCellPaintingEventArgs e)
        {

            //背景以外が描画されるようにする
            DataGridViewPaintParts paintParts = e.PaintParts & ~DataGridViewPaintParts.Background & ~DataGridViewPaintParts.ContentForeground;
            //セルを描画する
            e.Paint(e.ClipBounds, paintParts);

            string valStr = e.Value == null ? null : e.Value.ToString();

            // 描画用四角形をセット
            int padX = 3;
            float padY = (e.CellBounds.Height - e.CellStyle.Font.Height) / 2;
            RectangleF rect = new RectangleF(e.CellBounds.X + padX, e.CellBounds.Y + padY, e.CellBounds.Width, e.CellBounds.Height);

            // スペースサイズ取得
            TextFormatFlags fl = TextFormatFlags.Left | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
            TextFormatFlags fl2 = TextFormatFlags.NoPadding | TextFormatFlags.Left | TextFormatFlags.PreserveGraphicsClipping;
            SizeF hSpaceSize = TextRenderer.MeasureText(e.Graphics, Constants.StringSpace, e.CellStyle.Font, e.CellBounds.Size, fl2);
            SizeF zSpaceSize = TextRenderer.MeasureText(e.Graphics, Constants.String2ByteSpace, e.CellStyle.Font, e.CellBounds.Size, fl2);

            RectangleF bounds = e.CellBounds;
            RectangleF newrect = e.CellBounds;
            // 描画領域外（行ヘッダー）を描画しないようClip設定
            e.Graphics.Clip = new Region(e.Graphics.VisibleClipBounds);

            // NULLの場合はそのまま描画
            if (String.IsNullOrEmpty(valStr))
            {
                valStr = _nullString;
                TextRenderer.DrawText(e.Graphics, valStr, e.CellStyle.Font, Rectangle.Round(newrect), Color.Gray, fl);
            }
            else
            {
                // テキスト全体を描画
                TextRenderer.DrawText(e.Graphics, valStr, e.CellStyle.Font, Rectangle.Round(newrect), Color.Black, fl);

                // 測定用文字列
                string strForMeasure = null;
                // スペース文字
                string strSpace = null;
                bool drawText = false;
                float spaceWidth = 0;

                // 文字数分ループし、空白を描画
                for (int i = 0; i < valStr.Length; i++)
                {
                    string valChar = valStr.Substring(i, 1);

                    if (valChar == Constants.StringSpace)
                    {
                        strSpace = _hankakuSpace;
                        drawText = true;
                        spaceWidth = hSpaceSize.Width;
                    }
                    else if (valChar == Constants.String2ByteSpace)
                    {
                        if (isPropotionalFont)
                        {
                            strSpace = _propotionalZenkakuSpace;
                        }
                        else
                        {
                            strSpace = _zenkakuSpace;
                        }
                        drawText = true;
                        spaceWidth = zSpaceSize.Width;

                    }
                    else
                    {
                        strForMeasure += valChar;
                        drawText = false;
                    }

                    if (drawText)
                    {
                        if (bounds.IntersectsWith(newrect))
                        {
                            SizeF strSize = TextRenderer.MeasureText(e.Graphics, strForMeasure, e.CellStyle.Font, Size.Round(newrect.Size), fl2);
                            newrect.X = newrect.X + strSize.Width;
                            TextRenderer.DrawText(e.Graphics, strSpace, e.CellStyle.Font, Rectangle.Round(newrect), Color.Gray, fl);
                            newrect.X += spaceWidth;
                            strForMeasure = null;
                        }
                    }

                }
            }

            e.Handled = true;
        }
    }
}
