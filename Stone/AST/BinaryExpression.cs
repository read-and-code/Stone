using System.Collections;
using System.Collections.Generic;

namespace Stone.AST
{
    public class BinaryExpression : ASTList
    {
        public BinaryExpression(List<ASTree> children)
            : base(children)
        {
        }

        public ASTree Left
        {
            get
            {
                return this.Children[0];
            }
        }

        public string Operator
        {
            get
            {
                return ((ASTLeaf)this.Children[1]).Token.Text;
            }
        }

        public ASTree Right
        {
            get
            {
                return this.Children[2];
            }
        }
    }
}