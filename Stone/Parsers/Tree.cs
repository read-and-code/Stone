using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public class Tree : Element
    {
        public Tree(Parser parser)
        {
            this.Parser = parser;
        }

        private Parser Parser
        {
            get;
        }

        public override void Parse(Lexer lexer, List<ASTNode> astNodes)
        {
            astNodes.Add(this.Parser.Parse(lexer));
        }

        public override bool Match(Lexer lexer)
        {
            return this.Parser.Match(lexer);
        }
    }
}