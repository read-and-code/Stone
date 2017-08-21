using System;
using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class IfStatement : ASTBranchNode
    {
        public IfStatement(List<ASTNode> children)
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

        public ASTNode ThenBlock
        {
            get
            {
                return this.GetChild(1);
            }
        }

        public ASTNode ElseBlock
        {
            get
            {
                return this.NumberOfChildren > 2 ? this.GetChild(2) : null;
            }
        }

        public override object Eval(IEnvironment environment)
        {
            object condition = this.Condition.Eval(environment);

            if (condition is int && Convert.ToInt32(condition) != 0)
            {
                return this.ThenBlock.Eval(environment);
            }
            else
            {
                ASTNode body = this.ElseBlock;

                if (body == null)
                {
                    return 0;
                }
                else
                {
                    return body.Eval(environment);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("(if {0} {1} else {2})", this.Condition, this.ThenBlock, this.ElseBlock);
        }
    }
}