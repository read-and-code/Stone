using System.Collections.Generic;

namespace Stone.AST
{
    public class Dot : Postfix
    {
        public Dot(List<ASTree> asTrees)
            : base(asTrees)
        {
        }

        public string Name
        {
            get
            {
                return ((ASTLeaf)this.GetChild(0)).Token.Text;
            }
        }

        public override string ToString()
        {
            return string.Format(".{0}", this.Name);
        }
    }
}