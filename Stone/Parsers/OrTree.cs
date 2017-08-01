using System.Collections.Generic;
using System.Linq;
using Stone.AST;
using Stone.Exceptions;

namespace Stone.Parsers
{
    public class OrTree : Element
    {
        public OrTree(List<Parser> parsers)
        {
            this.Parsers = parsers;
        }

        public List<Parser> Parsers { get; private set; }

        public override void Parse(Lexer lexer, List<ASTree> asTrees)
        {
            Parser parser = this.Choose(lexer);

            if (parser == null)
            {
                throw new ParseException(lexer.Peek(0));
            }
            else
            {
                asTrees.Add(parser.Parse(lexer));
            }
        }

        public override bool Match(Lexer lexer)
        {
            return this.Choose(lexer) != null;
        }

        public void Insert(Parser parser)
        {
            this.Parsers.Insert(0, parser);
        }

        private Parser Choose(Lexer lexer)
        {
            foreach (Parser parser in this.Parsers)
            {
                if (parser.Match(lexer))
                {
                    return parser;
                }
            }

            return null;
        }
    }
}