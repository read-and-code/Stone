using System.Collections.Generic;

namespace Stone.AST
{
    public class NullStatement : ASTList
    {
        public NullStatement(List<ASTree> asTrees)
            : base(asTrees)
        {
        }
    }
}