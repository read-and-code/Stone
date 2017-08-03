using System;
using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class WhileStatement : ASTList
    {
        public WhileStatement(List<ASTree> asTrees)
            : base(asTrees)
        {
        }

        public ASTree Condition
        {
            get
            {
                return this.GetChild(0);
            }
        }

        public ASTree Body
        {
            get
            {
                return this.GetChild(1);
            }
        }

        public override string ToString()
        {
            return string.Format("(while {0} {1})", this.Condition, this.Body);
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
    }
}