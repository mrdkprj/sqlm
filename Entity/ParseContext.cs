using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public class ParseContext
    {
        public Exception ParseError { get; set; }
        
        public string ParsedSql { get; set; }

        public string RawSql { get; set; }

        public string RuleName { get; set; }

        public SqlType SqlType { get; set; }

        public string SqlCommandToken { get; set; }

    }
}
