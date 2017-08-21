using System.Collections.Generic;

namespace Stone.AST
{
    public class Postfix : ASTBranchNode
    {
        public Postfix(List<ASTNode> children)
            : base(children)
        {
        }
    }
}