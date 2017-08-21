using System;
using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class WhileStatement : ASTBranchNode
    {
        public WhileStatement(List<ASTNode> children)
            : base(children)
        {
        }

        public ASTNode Condition
        {
            get
            {
                return this.GetChild(0);
            }
        }

        public ASTNode Body
        {
            get
            {
                return this.GetChild(1);
            }
        }

        public override object Eval(IEnvironment environment)
        {
            object result = 0;

            while (true)
            {
                object condition = this.Condition.Eval(environment);

                if (condition is int && Convert.ToInt32(condition) == 0)
                {
                    return result;
                }
                else
                {
                    result = this.Body.Eval(environment);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("(while {0} {1})", this.Condition, this.Body);
        }
    }
}