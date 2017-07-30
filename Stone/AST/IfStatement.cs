using System.Collections.Generic;

namespace Stone.AST
{
    public class IfStatement : ASTList
    {
        public IfStatement(List<ASTree> asTrees)
            : base(asTrees)
        {
        }

        public ASTree Condition
        {
            get
            {
                return this.GetChild(0);
            }
        }

        public ASTree ThenBlock
        {
            get
            {
                return this.GetChild(1);
            }
        }

        public ASTree ElseBlock
        {
            get
            {
                return this.NumberOfChildren > 2 ? this.GetChild(2) : null;
            }
        }

        public override string ToString()
        {
            return string.Format("(if {0} {1} else {2})", this.Condition, this.ThenBlock, this.ElseBlock);
        }
    }
}