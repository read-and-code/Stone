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

        public Parser Parser { get; private set; }

        public bool OnlyOnce { get; private set; }

        public override void Parse(Lexer lexer, List<ASTree> asTrees)
        {
            while (this.Parser.Match(lexer))
            {
                ASTree asTree = this.Parser.Parse(lexer);

                if (asTree.GetType().Name != typeof(ASTList).Name || asTree.NumberOfChildren > 0)
                {
                    asTrees.Add(asTree);
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