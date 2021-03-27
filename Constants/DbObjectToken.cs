using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    public static class DbObjectToken
    {
        public const string Table = "Table";
        public const string View = "View";
        public const string Index = "Index";
        public const string Package = "Package";
        public const string PackageBody = "PackageBody";
        public const string Procedure = "Procedure";
        public const string Function = "Function";
        public const string Type = "Type";
        public const string Trigger = "Trigger";
        public const string Synonym = "Synonym";
        public const string Sequence = "Sequence";

        public readonly static string[] Tokens =
        {
            Table, View, Index, Package, PackageBody, Procedure,
            Function, Type, Trigger, Synonym, Sequence
        };
        
        public static DbObjectType ConverToDbObjectType(string objectToken)
        {
            switch (objectToken)
            {
                case Table:
                    return DbObjectType.Table;
                case View:
                    return DbObjectType.View;
                case Index:
                    return DbObjectType.Index;
                case Package:
                    return DbObjectType.Package;
                case PackageBody:
                    return DbObjectType.PackageBody;
                case Procedure:
                    return DbObjectType.Procedure;
                case Function:
                    return DbObjectType.Function;
                case Type:
                    return DbObjectType.Type;
                case Trigger:
                    return DbObjectType.Trigger;
                case Synonym:
                    return DbObjectType.Synonym;
                case Sequence:
                    return DbObjectType.Sequence;
            }

            throw new ArgumentException("No matched DbObjectType");
        }
    }
}
