using System;

namespace MasudaManager
{
    public class TableKeyNotFoundException : Exception
    {
        public TableKeyNotFoundException()
        {
        }

        public TableKeyNotFoundException(string message)
            : base(message)
        {
        }

        public TableKeyNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
