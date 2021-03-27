using System;
using System.Collections.Generic;
using System.Linq;
using MasudaManager.Utility.Preference;
using MasudaManager.DataAccess;
using System.Text;

namespace MasudaManager.Utility
{
    public class SqlInputAssistantParser : IParser<IDbCommandBuilder>
    {
        IDataAccess _dataAccess = DataAccessProvider.GetDataAccess();
        SqlInputAssistantDbCommandBuilder _builder = new SqlInputAssistantDbCommandBuilder();
        SqlInputAssistantComplementMode _compementMode = SqlInputAssistantComplementMode.None;
        StringBuilder _stringBuilder = new StringBuilder();
        List<string> _splitList = new List<string>();
        List<string> _objectNames = new List<string>();
        int _endPosition = 0;
        string _keyword = null;
        int _inputPosition = 0;
        
        readonly static SqlInputAssistantParser _instance = new SqlInputAssistantParser();
        static SqlInputAssistantParser() { }
        private SqlInputAssistantParser() { }
        public static SqlInputAssistantParser Instance
        {
            get { return _instance; }
        }

        #region Property
        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }          
        }

        public int InputPosition
        {
            get { return _inputPosition; }
            set { _inputPosition = value; }
        }

        public IEnumerable<string> ObjectNames
        {
            get { return _objectNames; }
            set { _objectNames = value.ToList(); }
        }

        public SqlInputAssistantComplementMode ComplementMode
        {
            get { return _compementMode; }
        }
        #endregion

        #region PrerequisiteCheck
        bool CheckPrerequisite()
        {
            if (String.IsNullOrEmpty(_keyword))
                return false;

            if (_objectNames.Count <= 0)
                return false;

            return true;
        }
        #endregion

        #region Parse
        public IDbCommandBuilder Parse(string input)
        {
            if (!CheckPrerequisite())
                return null;

            _compementMode = SqlInputAssistantComplementMode.None;
            _builder.Clear();

            if (UserPreference.Setting.Input.ShowTableNameSupport)
            {
                if (TryGetSchemaName())
                    return _builder;
            }

            if (UserPreference.Setting.Input.ShowColumnNameSupport)
            {
                if (TryGetObjectName())
                    return _builder;

                if (TryParseAlias(input))
                    return _builder;
            }

            return null;
        }
        #endregion

        #region Try get schema name
        bool TryGetSchemaName()
        {
            if (!IsSchemaName(_keyword))
                return false;

            _compementMode = SqlInputAssistantComplementMode.ObjectName;
            _builder.ComplementMode = _compementMode;
            _builder.CreateCommand(_keyword);

            return true;
        }
        #endregion

        #region Try get object name
        bool TryGetObjectName()
        {
            if (!_objectNames.Contains(_keyword, StringComparer.OrdinalIgnoreCase))
                return false;

            _compementMode = SqlInputAssistantComplementMode.ColumnName;
            _builder.ComplementMode = _compementMode;
            _builder.CreateCommand(_keyword);

            return true;
        }
        #endregion

        #region Try parse alias
        bool TryParseAlias(string input)
        {
            PrepareParse(input);

            int aliasIndex = GetAliasIndex();

            if (TryGetInlineView(aliasIndex))
                return true;

            if (TryGetObjectName(aliasIndex))
                return true;

            return false;
        }
        #endregion

        #region Prepare parse
        void PrepareParse(string input)
        {
            _splitList = GetSplittableString(input).Split(SqlInputAssistantParserTokens.Splitter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            int keywordIndex = _splitList.IndexOf(SqlInputAssistantParserTokens.Marker) - 1;

            //// remove maker
            _splitList.RemoveAt(keywordIndex + 1);

            _endPosition = GetEndPosition(keywordIndex);

            if (_endPosition < 0)
                _endPosition = _splitList.Count - 1;
        }

        string GetSplittableString(string value)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(value);

            //// Remove semicolon
            _stringBuilder.Replace(Constants.StringSemicolon, string.Empty);
            //// Remove Carrige Return and Line Feed
            _stringBuilder.Replace(Constants.StringCarriageReturn, Constants.StringSpace);
            _stringBuilder.Replace(Constants.StringNewLine, Constants.StringSpace);
            //// Remove Tab
            _stringBuilder.Replace(Constants.StringTab, Constants.StringSpace);

            //// Insert position marker
            _stringBuilder.Insert(_inputPosition + 1, GetSplitterEnclosedValue(SqlInputAssistantParserTokens.Marker));
            //// keep parentheses by inserting space
            _stringBuilder.Replace(Constants.StringLeftParenthesis, GetSplitterEnclosedValue(Constants.StringLeftParenthesis));
            _stringBuilder.Replace(Constants.StringRightParenthesis, GetSplitterEnclosedValue(Constants.StringRightParenthesis));

            return _stringBuilder.ToString();
        }

        string GetSplitterEnclosedValue(string value)
        {
            return SqlInputAssistantParserTokens.Splitter + value + SqlInputAssistantParserTokens.Splitter;
        }

        int GetEndPosition(int keywordIndex)
        {
            if (keywordIndex == _splitList.Count - 1)
                return _splitList.Count - 1;

            return GetTerminalIdentifierIndex(keywordIndex);
        }

        int GetTerminalIdentifierIndex(int keywordIndex)
        {
            return _splitList.FindIndex
                (
                    keywordIndex,
                    s => SqlInputAssistantParserTokens.TernminalIdentifiers.Contains(s, StringComparer.OrdinalIgnoreCase)
                );
        }
        #endregion

        #region Get alias index
        int GetAliasIndex()
        {
            var aliasIndex = _splitList.Select((value, index) => new { index, value })
                                       .Where(s => s.value == _keyword)
                                       .Where(s => !IsReservedWord(_splitList[s.index - 1]))
                                       .Select(s => s.index);

            if (aliasIndex.Count() > 1)
                return aliasIndex.Where(i => i <= _endPosition).LastOrDefault();
            else
                return aliasIndex.LastOrDefault();
        }
        #endregion

        #region Try get inline view
        bool TryGetInlineView(int aliasIndex)
        {
            if (!InlineViewPreIdentifierExists(_splitList, aliasIndex - 1) || !PostIdentifierExists(_splitList, aliasIndex + 1))
                return false;

            var inlineViewList = _splitList.TakeWhile(s => s != _keyword);
            string inlineViewSql = ReconstructInlineViewSql(inlineViewList);

            _compementMode = SqlInputAssistantComplementMode.InlineViewColumn;
            _builder.ComplementMode = _compementMode;
            _builder.CreateCommand(inlineViewSql);

            return true;
        }
        #endregion

        #region Try get object name
        bool TryGetObjectName(int aliasIndex)
        {
            if (!PreIdentifierExists(_splitList, aliasIndex - 2) || !PostIdentifierExists(_splitList, aliasIndex + 1))
                return false;

            _compementMode = SqlInputAssistantComplementMode.ColumnName;
            _builder.ComplementMode = _compementMode;
            _builder.CreateCommand(RemoveModifierFromObjectName(_splitList[aliasIndex - 1]));

            return true;
        }
        #endregion

        #region Identifier check

        bool IsSchemaName(string word)
        {
            if (word.Equals(_dataAccess.CurrentConnectionData.UserId, StringComparison.OrdinalIgnoreCase))
                return true;

            if (word.Equals(_dataAccess.DataDictioanryOwner, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        
        bool IsReservedWord(string word)
        {
            return Constants.ReservedWords.Contains(word, StringComparer.OrdinalIgnoreCase);
        }

        bool InlineViewPreIdentifierExists(List<string> list, int index)
        {
            if (index <= 0)
                return true;

            if (SqlInputAssistantParserTokens.InlineViewAliasPreIdentifiers.Contains(list[index], StringComparer.OrdinalIgnoreCase))
                return true;

            return false;
        }

        bool PreIdentifierExists(List<string> list, int index)
        {
            if (index <= 0)
                return true;

            if (SqlInputAssistantParserTokens.AliasPreIdentifiers.Contains(list[index], StringComparer.OrdinalIgnoreCase))
                return true;

            return false;
        }

        bool PostIdentifierExists(List<string> list, int index)
        {
            if (list.Count <= index)
                return true;

            if (SqlInputAssistantParserTokens.AliasPostIdentifiers.Contains(list[index], StringComparer.OrdinalIgnoreCase))
                return true;

            return false;
        }
        #endregion

        #region Remove modifier from ObjectName
        string RemoveModifierFromObjectName(string nameString)
        {
            List<string> splitNameList = nameString
                                        .Replace(Constants.StringDoubleQuotation, string.Empty)
                                        .Split(Constants.CharPeriod)
                                        .ToList();

            if (splitNameList.Count > 1)
                return splitNameList[1];
            else
                return nameString;
        }
        #endregion

        #region ReconstructInlineView
        string ReconstructInlineViewSql(IEnumerable<string> list)
        {
            //var reversedString = list.Reverse().TakeWhile(s => s != Constants.StringLeftParenthesis);

            //string extractedSql = String.Join(Constants.StringSpace, reversedString.Reverse());

            //return extractedSql.TrimStart(Constants.CharLeftParenthesis).TrimEnd(Constants.CharRightParenthesis).Trim();

            var reversedList = list.Reverse();

            int takeCount = GetOpeningBracePosition(reversedList);

            var reconstructedList = reversedList.Take(takeCount).Reverse();
            
            string reconstructedSql = String.Join(Constants.StringSpace, reconstructedList);

            return reconstructedSql.TrimStart(Constants.CharLeftParenthesis).TrimEnd(Constants.CharRightParenthesis).Trim();
        }
        #endregion

        
        int GetOpeningBracePosition(IEnumerable<string> list)
        {
            Stack<String> closingBrace = new Stack<String>();

            int openingBraceePosition = 0;

            foreach (string value in list)
            {
                if (value == Constants.StringRightParenthesis)
                    closingBrace.Push(value);

                if (value == Constants.StringLeftParenthesis)
                {
                    if (closingBrace.Count > 1)
                        closingBrace.Pop();
                    else
                        return openingBraceePosition;
                }

                openingBraceePosition++;
            }

            return 0;
        }
    }
}
