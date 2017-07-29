using System;
using Stone.AST;
using Stone.Tokens;

namespace Stone.Parsers
{
    public class NumberTokenElement : TokenElement
    {
        public NumberTokenElement(Type type)
            : base(type)
        {
        }

        public override bool Test(Token token)
        {
            return token.IsNumber;
        }
    }
}