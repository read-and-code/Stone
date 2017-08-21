using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ParameterList : ASTBranchNode
    {
        public ParameterList(List<ASTNode> children)
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

        public override void Eval(IEnvironment environment, int index, object value)
        {
            environment.PutNew(this.GetName(index), value);
        }

        private string GetName(int index)
        {
            return ((ASTLeaf)this.GetChild(index)).Token.Text;
        }
    }
}