using System;
using System.Collections.Generic;
using Stone.AST;
using Stone.Exceptions;
using Stone.Tokens;

namespace Stone.Parsers
{
    public abstract class TokenElement : Element
    {
        public TokenElement(Type type)
        {
            this.Type = type;
        }

        protected Type Type { get; private set; }

        public override void Parse(Lexer lexer, List<ASTree> asTrees)
        {
            Token token = lexer.Read();

            if (this.Test(token))
            {
                ASTree asTree = ASTreeFactory.Make(this.Type, null);

                asTrees.Add(asTree);
            }
            else
            {
                throw new ParseException(token);
            }
        }

        public override bool Match(Lexer lexer)
        {
            return this.Test(lexer.Peek(0));
        }

        public abstract bool Test(Token token);
    }
}