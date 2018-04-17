using System;
using Stone.Tokens;

namespace Stone.Parsers
{
    public class StringTokenElement : TokenElement
    {
        public StringTokenElement(Type type)
            : base(type)
        {
        }

        public override bool Test(Token token)
        {
            return token.IsString;
        }
    }
}