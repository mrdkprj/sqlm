using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasudaManager.Utility
{
    /// <summary>
    /// http://www.codeducky.org/easy-tree-and-linked-list-traversal-in-c/
    /// </summary>
    public static class Traverse
    {
        public static IEnumerable<T> DepthFirst<T>(T root, Func<T, IEnumerable<T>> children)
        {
            if (children == null) { throw new ArgumentNullException("Children is null"); }

            return DepthFirstIterator(root, children);
        }

        private static IEnumerable<T> DepthFirstIterator<T>(T root, Func<T, IEnumerable<T>> children)
        {
            var current = root;
            var stack = new Stack<IEnumerator<T>>();

            try
            {
                while (true)
                {
                    yield return current;

                    var childrenEnumerator = children(current).GetEnumerator();
                    if (childrenEnumerator.MoveNext())
                    {
                        // if we have children, the first child is our next current
                        // and push the new enumerator
                        current = childrenEnumerator.Current;
                        stack.Push(childrenEnumerator);
                    }
                    else
                    {
                        // otherwise, cleanup the empty enumerator and...
                        childrenEnumerator.Dispose();

                        // ...search up the stack for an enumerator with elements left
                        while (true)
                        {
                            if (stack.Count == 0)
                            {
                                // we didn't find one, so we're all done
                                yield break;
                            }

                            // consider the next enumerator on the stack
                            var topEnumerator = stack.Peek();
                            if (topEnumerator.MoveNext())
                            {
                                // if it has an element, use it
                                current = topEnumerator.Current;
                                break;
                            }
                            else
                            {
                                // otherwise discard it
                                stack.Pop().Dispose();
                            }
                        }
                    }
                }
            }
            finally
            {
                // guarantee that everything is cleaned up even
                // if we don't enumerate all the way through
                while (stack.Count > 0)
                {
                    stack.Pop().Dispose();
                }
            }
        }
    }
}
