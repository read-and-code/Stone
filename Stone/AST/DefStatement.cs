using System.Collections.Generic;

namespace Stone.AST
{
    public class DefStatement : ASTList
    {
        public DefStatement(List<ASTree> asTrees)
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

        public ParameterList Parameters
        {
            get
            {
                return (ParameterList)this.GetChild(1);
            }
        }

        public BlockStatement Body
        {
            get
            {
                return (BlockStatement)this.GetChild(2);
            }
        }

        public override string ToString()
        {
            return string.Format("(def {0} {1} {2})", this.Name, this.Parameters, this.Body);
        }
    }
}