using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class PrimaryExpression : ASTBranchNode
    {
        public PrimaryExpression(List<ASTNode> children)
            : base(children)
        {
        }

        private ASTNode Operand
        {
            get
            {
                return this.GetChild(0);
            }
        }

        public override object Eval(IEnvironment environment)
        {
            return this.EvalSubExpression(environment, 0);
        }

        public object EvalSubExpression(IEnvironment environment, int nest)
        {
            if (this.HasPostfix(nest))
            {
                object target = this.EvalSubExpression(environment, nest + 1);
                Postfix postfix = (Postfix)this.GetPostfix(nest);

                return postfix.Eval(environment, target);
            }
            else
            {
                return this.Operand.Eval(environment);
            }
        }

        public Postfix GetPostfix(int nest)
        {
            return (Postfix)this.GetChild(this.NumberOfChildren - nest - 1);
        }

        public bool HasPostfix(int nest)
        {
            return this.NumberOfChildren - nest > 1;
        }
    }
}