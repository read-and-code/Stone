using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class PrimaryExpression : ASTList
    {
        public PrimaryExpression(List<ASTree> asTrees)
            : base(asTrees)
        {
        }

        public ASTree Operand
        {
            get
            {
                return this.GetChild(0);
            }
        }

        public static ASTree Create(List<ASTree> asTrees)
        {
            return asTrees.Count == 1 ? asTrees[0] : new PrimaryExpression(asTrees);
        }

        public Postfix GetPostfix(int nest)
        {
            return (Postfix)this.GetChild(this.NumberOfChildren - nest - 1);
        }

        public bool HasPostfix(int nest)
        {
            return this.NumberOfChildren - nest > 1;
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

                return ((Postfix)this.GetPostfix(nest)).Eval(environment, target);
            }
            else
            {
                return this.Operand.Eval(environment);
            }
        }
    }
}