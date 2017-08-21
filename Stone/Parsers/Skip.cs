using System.Collections.Generic;
using Stone.AST;
using Stone.Tokens;

namespace Stone.Parsers
{
    public class Skip : Leaf
    {
        public Skip(List<string> tokens)
            : base(tokens)
        {
        }

        protected override void Find(List<ASTNode> astNodes, Token token)
        {
        }
    }
}