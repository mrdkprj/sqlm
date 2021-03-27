using Antlr4.Runtime.Tree;
using System.Collections.Generic;

namespace MasudaManager.Utility
{
    public class AntlrSqlParseTreeVisitor : IParseTreeVisitor<IEnumerable<ITree>>
    {
        IEnumerable<ITree> GetTreeList(ITree root)
        {
            for (int i = 0; i < root.ChildCount; i++)
            {
                yield return root.GetChild(i);
            }
        }

        public IEnumerable<ITree> Visit(IParseTree tree)
        {
            return GetTreeList(tree);
        }

        public IEnumerable<ITree> VisitChildren(IRuleNode node)
        {
            return GetTreeList(node);
        }

        public IEnumerable<ITree> VisitErrorNode(IErrorNode node)
        {
            return GetTreeList(node);
        }

        public IEnumerable<ITree> VisitTerminal(ITerminalNode node)
        {
            return GetTreeList(node);
        }
    }
}
