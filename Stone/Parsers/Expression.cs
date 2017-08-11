using System;
using System.Collections.Generic;
using Stone.AST;
using Stone.Tokens;

namespace Stone.Parsers
{
    public class Expression : Element
    {
        public Expression(Type type, Parser factor, Operators operators)
        {
            this.Type = type;
            this.Factor = factor;
            this.Operators = operators;
        }

        public Operators Operators
        {
            get;
            private set;
        }

        public Parser Factor
        {
            get;
            private set;
        }

        private Type Type
        {
            get;
            set;
        }

        public override void Parse(Lexer lexer, List<ASTree> asTrees)
        {
            ASTree right = this.Factor.Parse(lexer);
            Precedence precedence = this.NextOperator(lexer);

            while (precedence != null)
            {
                right = this.DoShift(lexer, right, precedence.Value);

                precedence = this.NextOperator(lexer);
            }

            asTrees.Add(right);
        }

        public override bool Match(Lexer lexer)
        {
            return this.Factor.Match(lexer);
        }

        private static bool IsRightExpression(int precedence, Precedence nextPrecedence)
        {
            if (nextPrecedence.IsLeftAssociative)
            {
                return precedence < nextPrecedence.Value;
            }
            else
            {
                return precedence <= nextPrecedence.Value;
            }
        }

        private Precedence NextOperator(Lexer lexer)
        {
            Token token = lexer.Peek(0);

            if (token.IsIdentifier && this.Operators.ContainsKey(token.Text))
            {
                return this.Operators[token.Text];
            }
            else
            {
                return null;
            }
        }

        private ASTree DoShift(Lexer lexer, ASTree left, int precedenceValue)
        {
            List<ASTree> asTrees = new List<ASTree>();
            asTrees.Add(left);
            asTrees.Add(new ASTLeaf(lexer.Read()));

            ASTree right = this.Factor.Parse(lexer);
            Precedence nextPrecedence = this.NextOperator(lexer);

            while (nextPrecedence != null && IsRightExpression(precedenceValue, nextPrecedence))
            {
                right = this.DoShift(lexer, right, nextPrecedence.Value);

                nextPrecedence = this.NextOperator(lexer);
            }

            asTrees.Add(right);

            return ASTreeFactory.Make(this.Type, new[] { asTrees });
        }
    }
}