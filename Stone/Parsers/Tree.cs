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

        public Parser Parser
        {
            get;
            private set;
        }

        public override void Parse(Lexer lexer, List<ASTree> asTrees)
        {
            asTrees.Add(this.Parser.Parse(lexer));
        }

        public override bool Match(Lexer lexer)
        {
            return this.Parser.Match(lexer);
        }
    }
}