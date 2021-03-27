using System;

namespace MasudaManager
{
    class ParseErrorException : Exception
    {
        public ParseErrorException(string message)
            : base(message)
        {
        }

        public ParseErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ParseErrorException(string message, int position)
            :base(message)
        {
            this.Position = position;
        }

        public int Position { get; set; }
    }
}
