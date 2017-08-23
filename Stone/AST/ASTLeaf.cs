using System;
using System.Collections.Generic;
using Stone.Exceptions;
using Stone.Interpreter;
using Stone.Tokens;

namespace Stone.AST
{
    public class ASTLeaf : ASTNode
    {
        private static List<ASTNode> empty = new List<ASTNode>();

        public ASTLeaf(Token token)
        {
            this.Token = token;
        }

        public override string Location
        {
            get
            {
                return string.Format("at line {0}", this.Token.LineNumber);
            }
        }

        public override int NumberOfChildren
        {
            get
            {
                return 0;
            }
        }

        public Token Token
        {
            get;
        }

        public override ASTNode GetChild(int i)
        {
            throw new IndexOutOfRangeException();
        }

        public override object Eval(IEnvironment environment)
        {
            throw new StoneException(string.Format("Cannot eval: {0}", this.ToString()), this);
        }

        public override object Eval(IEnvironment environment, object value)
        {
            throw new StoneException(string.Format("Cannot eval: {0}", this), this);
        }

        public override void Eval(IEnvironment environment, int index, object value)
        {
            throw new StoneException(string.Format("Cannot eval: {0}", this), this);
        }

        public override void Lookup(SymbolTable symbolTable)
        {
        }

        public override IEnumerator<ASTNode> GetChildren()
        {
            return empty.GetEnumerator();
        }

        public override string ToString()
        {
            return this.Token.Text;
        }
    }
}