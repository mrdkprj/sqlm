
using WinFormsMvp.Messaging;
namespace MasudaManager
{
    public static class Constants
    {
        public static readonly char CharQuotation = '\'';
        public static readonly char CharDoubleQuotation = '"';
        public static readonly char CharComma = ',';
        public static readonly char CharPeriod = '.';
        public static readonly char CharTab = '\t';
        public static readonly char CharCarriageReturn = '\r';
        public static readonly char CharNewLine = '\n';
        public static readonly char CharSpace = ' ';
        public static readonly char Char2ByteSpace = '　';
        public static readonly char CharAsterisk = '*';
        public static readonly char CharLeftParenthesis = '(';
        public static readonly char CharRightParenthesis = ')';
        public static readonly char CharSemicolon = ';';
        public static readonly char CharEquals = '=';

        public static readonly string StringQuotation = "'";
        public static readonly string StringDoubleQuotation = "\"";
        public static readonly string StringComma = ",";
        public static readonly string StringPeriod = ".";
        public static readonly string StringTab = "\t";
        public static readonly string StringCarriageReturn = "\r";
        public static readonly string StringNewLine = "\n";
        public static readonly string StringSpace = " ";
        public static readonly string String2ByteSpace = "　";
        public static readonly string StringAsterisk = "*";
        public static readonly string StringLeftParenthesis = "(";
        public static readonly string StringRightParenthesis = ")";
        public static readonly string StringSemicolon = ";";
        public static readonly string StringDoubleSpace = "  ";
        public static readonly string StringEquals = "=";

        public static readonly string[] CsvSpecialCharacters = new string[]
        {
            StringDoubleQuotation, StringComma, StringCarriageReturn, StringNewLine
        };

        public static readonly string[] ReservedWords = new string[]
        {
            "access","add","all","alter","and","any","as","asc",
            "audit","between","by","char","check","cluster","column",
            "commit","comment","compress","connect","create","current",
            "date","decimal","default","delete","desc","distinct",
            "drop","else","end","exclusive","exists","file","float","for",
            "from","grant","group","having","join","identified","immediate",
            "in","increment","index","initial","insert","integer",
            "intersect","into","is","left","level","like","lock","long",
            "maxextents","minus","mlslabel","mode","modify","noaudit",
            "nocompress","not","nowait","null","number","of","offline",
            "on","online","option","or","order","pctfree","prior",
            "privileges","public","raw","rename","resource","revoke","right",
            "rollback","row","rowid","rownum","rows","select","session","set",
            "share","size","smallint","start","successful","synonym",
            "sysdate","table","then","to","trigger","truncate", "uid","union",
            "unique","update","user","validate","values","varchar",
            "varchar2","view","when","whenever","where","with"
        };

        public static readonly GenericMessage<object> NullGenericMessage = new GenericMessage<object>(null);

        public static class RDBMS
        {
            public static readonly string Oracle = "Oracle";
            public static readonly string HiRDB = "HiRDB";
        }

        public static class Language
        {
            public const string Japanese = "Japanese";
            public const string English = "English";
        }
    }
}
