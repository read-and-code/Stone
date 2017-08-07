using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ParameterList : ASTList
    {
        public ParameterList(List<ASTree> asTrees)
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

        public string GetName(int index)
        {
            return ((ASTLeaf)this.GetChild(index)).Token.Text;
        }

        public override void Eval(IEnvironment environment, int index, object value)
        {
            environment.PutNew(this.GetName(index), value);
        }
    }
}