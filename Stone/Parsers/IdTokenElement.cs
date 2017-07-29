using System;
using System.Collections.Generic;
using Stone.Tokens;

namespace Stone.Parsers
{
    public class IdTokenElement : TokenElement
    {
        public IdTokenElement(Type type, HashSet<string> reservedKeywords)
            : base(type)
        {
            if (reservedKeywords != null)
            {
                this.ReservedKeywords = reservedKeywords;
            }
        }

        private HashSet<string> ReservedKeywords { get; set; }

        public override bool Test(Token token)
        {
            return token.IsIdentifier && !this.ReservedKeywords.Contains(token.Text);
        }
    }
}