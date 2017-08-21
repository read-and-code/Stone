using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class BlockStatement : ASTBranchNode
    {
        public BlockStatement(List<ASTNode> children)
            : base(children)
        {
        }

        public override object Eval(IEnvironment environment)
        {
            object result = 0;

            foreach (ASTNode astNode in this)
            {
                if (!(astNode is NullStatement))
                {
                    result = astNode.Eval(environment);
                }
            }

            return result;
        }
    }
}