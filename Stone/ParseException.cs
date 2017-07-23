using System;
using System.IO;

namespace Stone
{
    public class ParseException : Exception
    {
        public ParseException(Token token)
            : this(string.Empty, token)
        {
        }

        public ParseException(string message, Token token)
            : base(string.Format("Syntax error around {0}. {1}", GetLocation(token), message))
        {
        }

        public ParseException(IOException exception)
            : base("IO error", exception)
        {
        }

        public ParseException(string message)
            : base(message)
        {
        }

        private static string GetLocation(Token token)
        {
            if (token == Token.EOF)
            {
                return "the last line";
            }
            else
            {
                return string.Format("\"{0}\" at line {1}", token.Text, token.LineNumber);
            }
        }
    }
}