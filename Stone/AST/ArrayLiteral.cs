using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ArrayLiteral : ASTList
    {
        public ArrayLiteral(List<ASTree> asTrees)
            : base(asTrees)
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

            foreach (ASTree asTree in this)
            {
                array[i++] = asTree.Eval(environment);
            }

            return array;
        }
    }
}