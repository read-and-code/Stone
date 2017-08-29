using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public class Repeat : Element
    {
        public Repeat(Parser parser, bool onlyOnce)
        {
            this.Parser = parser;
            this.OnlyOnce = onlyOnce;
        }

        private Parser Parser
        {
            get;
        }

        private bool OnlyOnce
        {
            get;
        }

        public override void Parse(Lexer lexer, List<ASTNode> astNodes)
        {
            while (this.Parser.Match(lexer))
            {
                ASTNode astNode = this.Parser.Parse(lexer);

                if (astNode.GetType().Name != typeof(ASTBranchNode).Name || astNode.NumberOfChildren > 0)
                {
                    astNodes.Add(astNode);
                }

                if (this.OnlyOnce)
                {
                    break;
                }
            }
        }

        public override bool Match(Lexer lexer)
        {
            return this.Parser.Match(lexer);
        }
    }
}