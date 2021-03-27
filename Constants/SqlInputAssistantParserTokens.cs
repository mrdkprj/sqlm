using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager
{
    public static class SqlInputAssistantParserTokens
    {
        public static readonly string Marker = Guid.NewGuid().ToString();
        public static readonly string Splitter = Constants.StringSpace;
        public static readonly List<string> TernminalIdentifiers = new List<string>(new string[]
            {
                "on", "join", "("
            });

        public static readonly List<string> InlineViewAliasPreIdentifiers = new List<string>(new string[]
            {
                ")"
            });

        public static readonly List<string> AliasPreIdentifiers = new List<string>(new string[]
            {
                "from", "into", "as", "join", "("
            });

        public static readonly List<string> AliasPostIdentifiers = new List<string>(new string[] 
            { 
                "inner", "join", "on", "outer",
                "left", "rlght", ",", "where",
                "values", "select", "set", ";",
                "group", "order", "union", "except",
                ")", "without" , ",", "'"
            });
    }
}
