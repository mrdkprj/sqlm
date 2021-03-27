using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using MasudaManager.Utility.Parsers;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace MasudaManager.Utility
{
    public static class SqlParser
    {
        static readonly string _errorPositionFormat = "({0}:{1}) :";
        static readonly string _errorMessageFormat = "{0} Syntax Error [{1}]";

        static List<ParseContext> _contexts = new List<ParseContext>();
        //static SQLiteParser _parser;
        static plsqlParser _parser;
        //static AntlrSQLiteListener _sqlListener = new AntlrSQLiteListener();
        static AntlrPLSQLListener _sqlListener = new AntlrPLSQLListener();
        static AntlrSqlErrorListener _sqlErrorListener = new AntlrSqlErrorListener();
        static AntlrSqlParseTreeVisitor _sqlTreeVisitor = new AntlrSqlParseTreeVisitor();

        //static string _parsedSql = null;

        public static IEnumerable<ParseContext> Parse(string input)
        {
            //if (input == _parsedSql)
            //    return _contexts;

            //_parsedSql = input;
            return ParseSql(input);
        }

        #region Parse SQL
        static IEnumerable<ParseContext> ParseSql(string sql)
        {
            _contexts = new List<ParseContext>();

            if (String.IsNullOrEmpty(sql))
                return _contexts;

            string targetSql = sql.Replace(Constants.StringTab, Constants.StringSpace);

            AntlrInputStream inputStream = new AntlrInputStream(targetSql);
            //SQLiteLexer lexer = new SQLiteLexer(inputStream);
            ITokenSource lexer = new plsqlLexer(inputStream);
            CommonTokenStream stream = new CommonTokenStream(lexer);

            InitializeParser(stream);

            try
            {
                //SQLiteParser.ParseContext parsedContext = _parser.parse();
                _parser.compilation_unit();

                if (_parser.NumberOfSyntaxErrors > 0)
                    _contexts.Add(GetErrorContext(sql, GerParseErrorException(_sqlErrorListener)));
                else
                    CollectNormalParseContexts(targetSql);

                return _contexts;
            }
            catch (Exception ex)
            {
                if (_sqlErrorListener.Exception == null)
                    _contexts.Add(GetErrorContext(sql, new Exception(ex.Message, new ParseErrorException(ex.Message, 0))));
                else
                    _contexts.Add(GetErrorContext(sql, GerParseErrorException(_sqlErrorListener)));

                return _contexts;
            }
            finally
            {
                FinalizeParser();
            }
        }
        #endregion

        #region Antrl parser methods
        static void InitializeParser(CommonTokenStream stream)
        {
            _sqlErrorListener = new AntlrSqlErrorListener();
            //_sqlListener = new AntlrSQLiteListener();
            _sqlListener = new AntlrPLSQLListener();
            //_parser = new SQLiteParser(stream);
            _parser = new plsqlParser(stream);
            _parser.AddErrorListener(_sqlErrorListener);
            _parser.AddParseListener(_sqlListener);
        }

        static void FinalizeParser()
        {
            _parser.Reset();
        }

        static void CollectNormalParseContexts(string sql)
        {
            foreach (ParserRuleContext ruleContext in _sqlListener.Contexts)
            {
                _contexts.Add(GetNormalParseContext(ruleContext, sql));
            }
        }

        static ParseContext GetNormalParseContext(ParserRuleContext ruleContext, string targetSql)
        {
            int startPosition = ruleContext.Start.StartIndex;
            int length = ruleContext.Stop.StopIndex - startPosition + 1; 
            
            ParseContext context = new ParseContext();
            context.RawSql = targetSql.Substring(startPosition, length);
            context.ParsedSql = RebuildSqlFromParseTree(ruleContext);
            context.ParseError = null;
            context.RuleName = GetRuleName(_parser, ruleContext);
            context.SqlType = SqlTypeParser.Parse(context.RuleName);
            context.SqlCommandToken = SqlTypeParser.GetSqlCommandToken(context.RuleName);
            return context;
        }

        static ParseContext GetErrorContext(string sql, Exception e)
        {
            ParseContext context = new ParseContext();
            context.ParsedSql = null;
            context.RuleName = null;
            context.ParseError = e;
            context.RawSql = sql;
            context.SqlType = SqlType.Invalid;
            context.SqlCommandToken = null;
            return context;
        }

        static Exception GerParseErrorException(AntlrSqlErrorListener errorListener)
        {
            string errorPosition = String.Format(_errorPositionFormat, errorListener.OffendingToken.Line, errorListener.OffendingToken.StartIndex);
            string errorMessage = String.Format(_errorMessageFormat, errorPosition, errorListener.OffendingToken.Text);
            ParseErrorException parseError = new ParseErrorException(errorMessage, errorListener.OffendingToken.StartIndex);
            return new Exception(errorMessage, parseError);
        }

        static string RebuildSqlFromParseTree(ITree root)
        {
            List<string> tokenTexts = new List<string>();

            var iTrees = Traverse.DepthFirst(root, tree => _sqlTreeVisitor.Visit((IParseTree)tree));
 
            foreach (var iTree in iTrees)
            {
                CommonToken token = iTree.Payload as CommonToken;
                if (token != null && token.Type >= 0)
                    tokenTexts.Add(token.Text);
            }

            return RemoveExtraSpaces(String.Join(Constants.StringSpace, tokenTexts));
        }

        static string RemoveExtraSpaces(string text)
        {
            return text.Replace(" . ", ".");
        }

        static string GetRuleName(Parser parser, ParserRuleContext context)
        {
            return context.start.Text;
            //if (context.ChildCount > 0)
            //    return parser.RuleNames[context.GetRuleContext<ParserRuleContext>(0).RuleIndex];
            //else
            //    return parser.RuleNames[context.RuleIndex];
        }
        #endregion

        #region Custom parse methods
        public static ParseContext ParseSqlType(string sql)
        {            
            string initialString = sql.Trim().Split(Constants.CharSpace).FirstOrDefault();

            ParseContext context = new ParseContext();
            context.ParsedSql = sql;
            context.ParseError = null;
            context.RawSql = sql;
            context.RuleName = null;
            context.SqlType = SqlTypeParser.Parse(initialString);
            context.SqlCommandToken = SqlTypeParser.GetSqlCommandToken(initialString);
            return context;
        }

        public static string AdjustSql(string sql)
        {
            string sqlWithoutComment = RemoveComments(sql.Trim());

            string adjustedSql = sqlWithoutComment
                .Replace(Constants.StringCarriageReturn, Constants.StringSpace)
                .Replace(Constants.StringNewLine, Constants.StringSpace)
                .TrimEnd(Constants.CharSemicolon);

            return adjustedSql;
        }
        #endregion

        #region Regex remove comments
        static string RemoveComments(string inputString)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"--(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";

            string noComments = Regex.Replace(inputString,
                                            blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
                                            me =>
                                            {
                                                if (me.Value.StartsWith("/*") || me.Value.StartsWith("--"))
                                                    return me.Value.StartsWith("--") ? Environment.NewLine : "";
                                                // Keep the literal strings
                                                return me.Value;
                                            },
                                            RegexOptions.Singleline);
            return noComments;
        }
        #endregion
    
        #region [Unuse] Traverse parse tree (Recursive)
        static string UseRecursive(ITree tree)
        {
            List<CommonToken> list = new List<CommonToken>();
            RecursiveMethod(list, tree);
            List<string> tokenTexts = new List<string>();
            foreach (var ta in list)
            {
                tokenTexts.Add(ta.Text);
            }

            return String.Join(Constants.StringSpace, tokenTexts);
        }

        static void RecursiveMethod(List<CommonToken> list, ITree tree)
        {
            CommonToken c = tree.Payload as CommonToken;
            if (c != null)
            {
                list.Add(c);
            }

            for (int i = 0; i < tree.ChildCount; ++i)
                RecursiveMethod(list, tree.GetChild(i));
        }
        #endregion
    }
}
