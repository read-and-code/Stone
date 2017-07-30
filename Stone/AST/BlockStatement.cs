using System.Collections.Generic;

namespace Stone.AST
{
    public class BlockStatement : ASTList
    {
        public BlockStatement(List<ASTree> asTrees)
            : base(asTrees)
        {
        }
    }
}