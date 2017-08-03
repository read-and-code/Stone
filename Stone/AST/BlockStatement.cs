using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class BlockStatement : ASTList
    {
        public BlockStatement(List<ASTree> asTrees)
            : base(asTrees)
        {
        }

        public override object Eval(IEnvironment environment)
        {
            object result = 0;

            foreach (ASTree asTree in this)
            {
                if (!(asTree is NullStatement))
                {
                    result = asTree.Eval(environment);
                }
            }

            return result;
        }
    }
}