using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public abstract class Element
    {
        public abstract void Parse(Lexer lexer, List<ASTNode> astNodes);

        public abstract bool Match(Lexer lexer);
    }
}