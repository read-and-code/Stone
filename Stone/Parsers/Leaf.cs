using System;
using System.Collections.Generic;
using System.Linq;
using Stone.AST;
using Stone.Exceptions;
using Stone.Tokens;

namespace Stone.Parsers
{
    public class Leaf : Element
    {
        public Leaf(List<string> tokens)
        {
            this.Tokens = tokens;
        }

        public List<string> Tokens
        {
            get;
            private set;
        }

        public override void Parse(Lexer lexer, List<ASTree> asTrees)
        {
            Token token = lexer.Read();

            if (token.IsIdentifier)
            {
                foreach (string t in this.Tokens)
                {
                    if (t.Equals(token.Text))
                    {
                        this.Find(asTrees, token);

                        return;
                    }
                }
            }

            if (this.Tokens.Count > 0)
            {
                throw new ParseException(string.Format("{0} expected.", this.Tokens[0]), token);
            }
            else
            {
                throw new ParseException(token);
            }
        }

        public override bool Match(Lexer lexer)
        {
            Token token = lexer.Peek(0);

            if (token.IsIdentifier)
            {
                return this.Tokens.Any(t => t == token.Text);
            }
            else
            {
                return false;
            }
        }

        protected virtual void Find(List<ASTree> asTrees, Token token)
        {
            asTrees.Add(new ASTLeaf(token));
        }
    }
}