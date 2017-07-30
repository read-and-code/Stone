using System.Collections.Generic;

namespace Stone.AST
{
    public class NegativeExpression : ASTList
    {
        public NegativeExpression(List<ASTree> asTrees)
            : base(asTrees)
        {
        }

        public ASTree Operand
        {
            get
            {
                return this.GetChild(0);
            }
        }

        public override string ToString()
        {
            return string.Format("-{0}", this.Operand);
        }
    }
}