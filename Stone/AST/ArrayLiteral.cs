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

        public int Size
        {
            get
            {
                return this.NumberOfChildren;
            }
        }

        public override object Eval(IEnvironment environment)
        {
            int i = 0;
            int size = this.NumberOfChildren;
            object[] array = new object[size];

            foreach (ASTNode astNode in this)
            {
                array[i++] = astNode.Eval(environment);
            }

            return array;
        }
    }
}