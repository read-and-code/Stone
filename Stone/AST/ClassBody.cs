using System.Collections.Generic;
using Stone.AST;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ClassBody : ASTBranchNode
    {
        public ClassBody(List<ASTNode> children)
            : base(children)
        {
        }

        public override object Eval(IEnvironment environment)
        {
            foreach (ASTNode astNode in this)
            {
                astNode.Eval(environment);
            }

            return null;
        }
    }
}