using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class AnonymousFunction : ASTBranchNode
    {
        public AnonymousFunction(List<ASTNode> children)
            : base(children)
        {
        }

        public ParameterList Parameters
        {
            get
            {
                return (ParameterList)this.GetChild(0);
            }
        }

        public BlockStatement Body
        {
            get
            {
                return (BlockStatement)this.GetChild(1);
            }
        }

        public override object Eval(IEnvironment environment)
        {
            return new Function(this.Parameters, this.Body, environment);
        }

        public override string ToString()
        {
            return string.Format("(func {0} {1})", this.Parameters, this.Body);
        }
    }
}