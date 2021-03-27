using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace MasudaManager.Utility.Parsers
{
    public class AntlrSqlErrorListener : BaseErrorListener
    {
        public IRecognizer Recognizer { get; set; }
        public IToken OffendingToken { get; set; }
        public int ErrorLine { get; set; }
        public int ErrorPosition { get; set; }
        public string Message { get; set; }
        public RecognitionException Exception { get; set; }

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);

            this.Recognizer = recognizer;
            this.OffendingToken = offendingSymbol;
            this.ErrorLine = line;
            this.ErrorPosition = charPositionInLine;
            this.Message = msg;
            this.Exception = e;
        }
    }
}
