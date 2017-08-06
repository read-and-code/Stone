using System.Collections.Generic;

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
    }
}