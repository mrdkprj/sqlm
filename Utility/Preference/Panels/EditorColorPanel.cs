using ScintillaNET;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MasudaManager.Utility.Preference.Setting
{
    [ToolboxItem(false)]
    public partial class EditorColorPanel : PreferencePanelBase
    {
        EditorColorData _colorData = new EditorColorData();
        readonly string _sampleText =
            "/* comment starts" + Environment.NewLine +
            " comment ends */" + Environment.NewLine +
            "-- line comment" + Environment.NewLine +
            "select" + Environment.NewLine +
            "  \"col1\", 'col2', col3, max(col4)" + Environment.NewLine +
            "from table1" + Environment.NewLine +
            "where col5 * 100 = 12345" + Environment.NewLine +
            "and col6 in ( 123, 'abc', \"def\");";

        public EditorColorPanel()
        {
            InitializeComponent();          
            this.splitContainer1.TabStop = false;
            this.sqlInputViewSample.TabStop = false;
            this.sqlInputViewSample.TabIndex = MaxTabIndex;
            DisplayPreference();
        }

        public override void SetPanelItemTabIndex(int startIndex, int increment)
        {
            InitializeItemTabIndex(startIndex);
            this.propertyGrid.TabIndex = GetIncrementedTabIndex(increment);
        }

        public override void DisplayPreference()
        {
            SetHighlightSettingToColorData();

            this.propertyGrid.SelectedObject = _colorData;

            this.sqlInputViewSample.ApplySetting();

            this.sqlInputViewSample.Text = _sampleText;
            this.sqlInputViewSample.ReadOnly = true;
            this.sqlInputViewSample.HideLineNumber();
            this.sqlInputViewSample.HideSelectionMargin();
        }
        
        public override void RetrievePreference()
        {
            EditorColorData currentData = (EditorColorData)this.propertyGrid.SelectedObject;

            UserPreference.Setting.EditorColor.MatchingBracketBackColor = currentData.BracketBackColor;
            UserPreference.Setting.EditorColor.MatchingBracketForeColor = currentData.BracketForeColor;
            UserPreference.Setting.EditorColor.CharColor = currentData.Character;
            UserPreference.Setting.EditorColor.CommentColor = currentData.Comment;
            UserPreference.Setting.EditorColor.KeywordColor = currentData.Keyword;
            UserPreference.Setting.EditorColor.NumberColor = currentData.Number;
            UserPreference.Setting.EditorColor.OperatorColor = currentData.Operator;
            UserPreference.Setting.EditorColor.StringColor = currentData.String;
        }

        void SetHighlightSettingToColorData()
        {
            _colorData.BracketBackColor = UserPreference.Setting.EditorColor.MatchingBracketBackColor;
            _colorData.BracketForeColor = UserPreference.Setting.EditorColor.MatchingBracketForeColor;
            _colorData.Character = UserPreference.Setting.EditorColor.CharColor;
            _colorData.Comment = UserPreference.Setting.EditorColor.CommentColor;
            _colorData.Keyword = UserPreference.Setting.EditorColor.KeywordColor;
            _colorData.Number = UserPreference.Setting.EditorColor.NumberColor;
            _colorData.Operator = UserPreference.Setting.EditorColor.OperatorColor;
            _colorData.String = UserPreference.Setting.EditorColor.StringColor;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateSampleText();
        }

        void UpdateSampleText()
        {
            sqlInputViewSample.Styles[Style.BraceLight].BackColor = _colorData.BracketBackColor;
            sqlInputViewSample.Styles[Style.BraceLight].ForeColor = _colorData.BracketForeColor;
            sqlInputViewSample.Styles[Style.Sql.Comment].ForeColor = _colorData.Comment;
            sqlInputViewSample.Styles[Style.Sql.CommentDoc].ForeColor = _colorData.Comment;
            sqlInputViewSample.Styles[Style.Sql.CommentDocKeyword].ForeColor = _colorData.Comment;
            sqlInputViewSample.Styles[Style.Sql.CommentLine].ForeColor = _colorData.Comment;
            sqlInputViewSample.Styles[Style.Sql.CommentLineDoc].ForeColor = _colorData.Comment;
            sqlInputViewSample.Styles[Style.Sql.Character].ForeColor = _colorData.Character;
            sqlInputViewSample.Styles[Style.Sql.Number].ForeColor = _colorData.Number;
            sqlInputViewSample.Styles[Style.Sql.Operator].ForeColor = _colorData.Operator;
            sqlInputViewSample.Styles[Style.Sql.String].ForeColor = _colorData.String;
            sqlInputViewSample.Styles[Style.Sql.Word].ForeColor = _colorData.Keyword;
        }

    }
}
