using System.Collections.Generic;
using Stone.AST;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ClassBody : ASTList
    {
        public ClassBody(List<ASTree> asTrees)
            : base(asTrees)
        {
        }

        public override object Eval(IEnvironment environment)
        {
            foreach (ASTree asTree in this)
            {
                asTree.Eval(environment);
            }

            return null;
        }
    }
}