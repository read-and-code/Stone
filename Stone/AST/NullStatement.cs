using System.Collections.Generic;

namespace Stone.AST
{
    public class NullStatement : ASTBranchNode
    {
        public NullStatement(List<ASTNode> children)
            : base(children)
        {
        }
    }
}