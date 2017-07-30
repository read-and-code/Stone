using System.Collections.Generic;

namespace Stone.AST
{
    public class PrimaryExpression : ASTList
    {
        public PrimaryExpression(List<ASTree> asTrees)
            : base(asTrees)
        {
        }

        public static ASTree Create(List<ASTree> asTrees)
        {
            return asTrees.Count == 1 ? asTrees[0] : new PrimaryExpression(asTrees);
        }
    }
}