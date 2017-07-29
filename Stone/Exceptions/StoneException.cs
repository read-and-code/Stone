using System;
using Stone.AST;

namespace Stone.Exceptions
{
    public class StoneException : Exception
    {
        public StoneException(string message)
            : base(message)
        {
        }

        public StoneException(string message, ASTree asTree)
            : base(string.Format("{0} {1}", message, asTree.Location))
        {
        }
    }
}