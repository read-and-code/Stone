using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ArrayLiteral : ASTBranchNode
    {
        public ArrayLiteral(List<ASTNode> children)
            : base(children)
        {
        }

        private int Size
        {
            get
            {
                return this.NumberOfChildren;
            }
        }

        public override object Eval(IEnvironment environment)
        {
            int i = 0;
            object[] array = new object[this.Size];

            foreach (ASTNode astNode in this)
            {
                array[i++] = astNode.Eval(environment);
            }

            return array;
        }
    }
}