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
            this.Type = type == null ? typeof(ASTLeaf) : type;
        }

        protected Type Type
        {
            get;
        }

        public override void Parse(Lexer lexer, List<ASTNode> astNodes)
        {
            Token token = lexer.Read();

            if (this.Test(token))
            {
                ASTNode astNode = ASTNodeFactory.Make(this.Type, new[] { token });

                astNodes.Add(astNode);
            }
            else
            {
                throw new ParseException(token);
            }
        }

        public override bool Match(Lexer lexer)
        {
            Token token = lexer.Peek(0);

            return this.Test(token);
        }

        public abstract bool Test(Token token);
    }
}