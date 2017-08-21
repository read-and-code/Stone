using System;
using System.Collections.Generic;
using Stone.Exceptions;
using Stone.Interpreter;

namespace Stone.AST
{
    public class NegativeExpression : ASTBranchNode
    {
        public NegativeExpression(List<ASTNode> children)
            : base(children)
        {
        }

        public ASTNode Operand
        {
            get
            {
                return this.GetChild(0);
            }
        }

        public override object Eval(IEnvironment environment)
        {
            object value = this.Operand.Eval(environment);

            if (value is int)
            {
                return -Convert.ToInt32(value);
            }
            else
            {
                throw new StoneException("Bad type for -", this);
            }
        }

        public override string ToString()
        {
            return string.Format("-{0}", this.Operand);
        }
    }
}