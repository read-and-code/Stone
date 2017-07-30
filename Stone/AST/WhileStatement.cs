using System.Collections.Generic;

namespace Stone.AST
{
    public class WhileStatement : ASTList
    {
        public WhileStatement(List<ASTree> asTrees)
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

        public ASTree Body
        {
            get
            {
                return this.GetChild(1);
            }
        }

        public override string ToString()
        {
            return string.Format("(while {0} {1})", this.Condition, this.Body);
        }
    }
}