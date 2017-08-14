using System.Collections.Generic;

namespace Stone.AST
{
    public class ClassBody : ASTList
    {
        public ClassBody(List<ASTree> asTrees)
            : base(asTrees)
        {
        }
    }
}