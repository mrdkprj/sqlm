using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;
using MasudaManager.Utility.Preference;
using System.Drawing;
using MasudaManager.Utility;
using System.ComponentModel;

namespace MasudaManager.Controls
{
    public partial class SqlInputView : XSciEditor, IMsdControl
    {
        public event EventHandler SaveStatusChanged;

        readonly int _adjustZoomRatio = 100;
        readonly int _baseZoom = 0;
        readonly int _defaultMarginWidth = 20;
        readonly int _marginPadding = 2;
        readonly char[] _braceCharacters = { Constants.CharLeftParenthesis, Constants.CharRightParenthesis };
        int _lastCaretPosition = 0;
        int _maxLineNumberLength = 0;
        bool _saved = true;
        FileStatus _fileSatus = FileStatus.None;

        public SqlInputView()
            : base()
        {
            InitializeComponent();

            InitializeSetting();
            ApplySetting();
        }

        void InitializeSetting()
        {
            this.UsePopup(false);
            this.ViewWhitespace = WhitespaceMode.Invisible;
            this.ViewEol = false;
            this.WhitespaceSize = 2;
            this.Lexer = ScintillaNET.Lexer.Sql;
            this.Zoom = _baseZoom;
            this.DisplayControlChar = false;
            this.FilePath = null;
            this.MouseDwellTime = 100;
        }

        public string FilePath { get; set; }
        public bool Saved { get { return _saved; } }
        public string ZoomRatio { get { return GetZoomRatio().ToString(); } }

        #region Events override
        protected void OnSaveStatusChanged(bool saved)
        {
            if (_saved == saved)
                return;

            _saved = saved;

            if (this.SaveStatusChanged != null)
                this.SaveStatusChanged(this, EventArgs.Empty);
        }

        //protected override void OnFontChanged(EventArgs e)
        //{
        //    base.OnFontChanged(e);

        //    ResetDefault();
        //}

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (UserPreference.Setting.Editor.ShowLineNumber)
                AdjustLineNumberWidth();

            ChangeFileStatus();
        }

        protected override void OnUpdateUI(UpdateUIEventArgs e)
        {
            base.OnUpdateUI(e);

            if (UserPreference.Setting.Editor.EnableBraceHighlight)
                HighlightMatchingBrace();
            else
                ResetBraceHighlight();
        }

        #endregion

        #region LoadText
        public void LoadText(string text)
        {
            _fileSatus = FileStatus.Loading;

            this.Text = text;
            this.EmptyUndoBuffer();

            _fileSatus = FileStatus.None;
        }
        #endregion

        #region Mark as Saved
        public void MarkAsSaved()
        {
            _fileSatus = FileStatus.Saved;
            OnSaveStatusChanged(true);
        }
        #endregion

        #region FileStatus
        void ChangeFileStatus()
        {
            if (String.IsNullOrEmpty(this.FilePath))
                return;

            if (_fileSatus == FileStatus.Loading)
                return;

            if (_fileSatus == FileStatus.Changed)
                ResetFileStatus();
            else
                SetFileStatus();
        }
        


        void SetFileStatus()
        {
            OnSaveStatusChanged(false);
            _fileSatus = FileStatus.Changed;
        }

        void ResetFileStatus()
        {
            if (!this.CanUndo)
            {
                OnSaveStatusChanged(true);
                _fileSatus = FileStatus.None;
            }
        }
        #endregion

        #region Margin
        public void ShowLineNumber()
        {
            AdjustLineNumberWidth();
        }

        public void HideLineNumber()
        {
            this.Margins[0].Width = 0;
        }

        public void ShowSelectionMargin()
        {
            this.Margins[1].Width = _defaultMarginWidth;
        }

        public void HideSelectionMargin()
        {
            this.Margins[1].Width = 0;
        }
        #endregion

        #region ApplySetting
        public void ApplySetting()
        {
            ResetDefault();
            ApplyMargin();
            ApplyWordwrap();
            ApplyColorSetting();
            ApplyBraceMatching();
        }

        void ResetDefault()
        {
            this.WrapMode = UserPreference.Setting.Editor.WordwrapMode;

            this.StyleResetDefault();
            this.Font = UserPreference.Setting.Input.Font;
            this.Styles[Style.Default].Font = UserPreference.Setting.Input.Font.Name;
            this.Styles[Style.Default].SizeF = UserPreference.Setting.Input.Font.Size;
            this.Styles[Style.Default].ForeColor = System.Drawing.Color.Black;
            this.StyleClearAll();

            this.SetKeywords(0, String.Join(Constants.StringSpace, Constants.ReservedWords));
        }

        void ApplyMargin()
        {
            if (UserPreference.Setting.Editor.ShowLineNumber)
                ShowLineNumber();
            else
                HideLineNumber();

            if (UserPreference.Setting.Editor.ShowSelectionMargin)
                ShowSelectionMargin();
            else
                HideSelectionMargin();
        }

        void ApplyWordwrap()
        {
            if (UserPreference.Setting.Editor.EnableWordwrap)
                this.WrapMode = UserPreference.Setting.Editor.WordwrapMode;
            else
                this.WrapMode = ScintillaNET.WrapMode.None;
        }

        void ApplyBraceMatching()
        {
            if (UserPreference.Setting.Editor.EnableBraceHighlight)
            {
                this.Styles[Style.BraceLight].BackColor = UserPreference.Setting.EditorColor.MatchingBracketBackColor;
                this.Styles[Style.BraceLight].ForeColor = UserPreference.Setting.EditorColor.MatchingBracketForeColor;
            }
            else
            {
                ResetBraceHighlight();
            }
        }

        void ApplyColorSetting()
        {
            this.Styles[Style.Sql.Comment].ForeColor = UserPreference.Setting.EditorColor.CommentColor;
            this.Styles[Style.Sql.CommentDoc].ForeColor = UserPreference.Setting.EditorColor.CommentColor;
            this.Styles[Style.Sql.CommentDocKeyword].ForeColor = UserPreference.Setting.EditorColor.CommentColor;
            this.Styles[Style.Sql.CommentLine].ForeColor = UserPreference.Setting.EditorColor.CommentColor;
            this.Styles[Style.Sql.CommentLineDoc].ForeColor = UserPreference.Setting.EditorColor.CommentColor;

            this.Styles[Style.Sql.Character].ForeColor = UserPreference.Setting.EditorColor.CharColor;
            this.Styles[Style.Sql.Identifier].ForeColor = System.Drawing.Color.Black;
            this.Styles[Style.Sql.QuotedIdentifier].ForeColor = System.Drawing.Color.Black;
            this.Styles[Style.Sql.Number].ForeColor = UserPreference.Setting.EditorColor.NumberColor;
            this.Styles[Style.Sql.Operator].ForeColor = UserPreference.Setting.EditorColor.OperatorColor;
            this.Styles[Style.Sql.QOperator].ForeColor = UserPreference.Setting.EditorColor.OperatorColor;
            this.Styles[Style.Sql.String].ForeColor = UserPreference.Setting.EditorColor.StringColor;
            this.Styles[Style.Sql.Word].ForeColor = UserPreference.Setting.EditorColor.KeywordColor;
        }
        #endregion

        #region HighlightMatchingBrace
        void HighlightMatchingBrace()
        {
            if (_lastCaretPosition == this.CurrentPosition)
                return;

            _lastCaretPosition = this.CurrentPosition;

            int braceStartPosition = GetBraceStartPosition();

            if (braceStartPosition >= 0)
                SetBraceHighlight(braceStartPosition);
            else
                ResetBraceHighlight();
        }

        int GetBraceStartPosition()
        {
            if (this.CurrentPosition > 0 && IsBrace(this.GetCharAt(this.CurrentPosition - 1)))
                return (this.CurrentPosition - 1);

            if (IsBrace(this.GetCharAt(this.CurrentPosition)))
                return this.CurrentPosition;

            return -1;
        }

        bool IsBrace(int character)
        {
            return _braceCharacters.Contains((char)character);
        }

        void SetBraceHighlight(int braceStartPosition)
        {
            int braceEndPosition = this.BraceMatch(braceStartPosition);

            if (braceEndPosition == Scintilla.InvalidPosition)
                this.BraceBadLight(braceStartPosition);
            else
                this.BraceHighlight(braceStartPosition, braceEndPosition);
        }

        void ResetBraceHighlight()
        {
            //// Turn off brace matching
            this.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
        }
        #endregion

        #region ShowInputAssistant
        public void ShowInputAssistant(Control container)
        {
            int endPosition = this.SelectionStart;
            int startPosition = this.WordStartPosition(endPosition, false);
            if (startPosition > endPosition)
                return;

            string inputWord = this.GetWordFromPosition(startPosition);

            SqlInputAssistantPresenter.Instance.SetAssistantContainer(container);
            SqlInputAssistantPresenter.Instance.SubscribeAssistant(this);
            SqlInputAssistantPresenter.Instance.ShowAssistant(this.Text, inputWord, startPosition);
        }
        #endregion

        #region IsCommented
        public bool IsCommented(int caretPosition)
        {
            int style = this.GetStyleAt(caretPosition);

            switch (style)
            {
                case Style.Sql.Comment:
                case Style.Sql.CommentDoc:
                case Style.Sql.CommentLine:
                case Style.Sql.CommentLineDoc:
                    return true;
            }

            return false;
        }
        #endregion

        #region GetZoomRatio
        int GetZoomRatio()
        {
            return (this.Zoom * 10) + _adjustZoomRatio;
        }
        #endregion
        
        #region Adjust line number width
        void AdjustLineNumberWidth()
        {
            int maxlineNumberLength = this.Lines.Count.ToString().Length;
            if (maxlineNumberLength == _maxLineNumberLength)
                return;

            this.Margins[0].Width = this.TextWidth(Style.LineNumber, new string('9', maxlineNumberLength + 1)) + _marginPadding;
            _maxLineNumberLength = maxlineNumberLength;
        }
        #endregion

        #region ApplyPreference
        public void ApplyPreference()
        {
            ApplySetting();
        }
        #endregion
    }
}
