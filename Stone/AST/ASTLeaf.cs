using System;
using System.Collections.Generic;

namespace Stone.AST
{
    public class ASTLeaf : ASTree
    {
        private static List<ASTree> empty = new List<ASTree>();

        public ASTLeaf(Token token)
        {
            this.Token = token;
        }

        public override int NumberOfChildren
        {
            get
            {
                return 0;
            }
        }

        public override string Location
        {
            get
            {
                return string.Format("at line {0}", this.Token.LineNumber);
            }
        }

        public Token Token { get; private set; }

        public override ASTree GetChild(int i)
        {
            throw new IndexOutOfRangeException();
        }

        public override IEnumerator<ASTree> GetChildren()
        {
            return empty.GetEnumerator();
        }

        public override string ToString()
        {
            return this.Token.Text;
        }
    }
}