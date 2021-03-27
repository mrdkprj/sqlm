using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace MasudaManager.Utility.Parsers
{
    public class AntlrPLSQLListener : plsqlBaseListener
    {
        public AntlrPLSQLListener()
        {
            this.Contexts = new List<ParserRuleContext>();
        }

        public List<ParserRuleContext> Contexts { get; set; }

        //public override void EnterSql_stmt(SQLiteParser.Sql_stmtContext context)
        //{
        //    base.EnterSql_statement(
        //    this.Contexts.Add(context);
        //}

        //public override void EnterCompilation_unit(plsqlParser.Compilation_unitContext context)
        //{
        //    base.EnterCompilation_unit(context);
        //    this.Contexts.Add(context);
        //}
        public override void EnterUnit_statement(plsqlParser.Unit_statementContext context)
        {
            base.EnterUnit_statement(context);
            this.Contexts.Add(context);
        }

        //public override void EnterSql_statement(plsqlParser.Sql_statementContext context)
        //{
        //    base.EnterSql_statement(context);
        //    this.Contexts.Add(context);
        //}
    }
}
