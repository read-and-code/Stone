using System.Collections.Generic;

namespace Stone.AST
{
    public class ClassStatement : ASTList
    {
        public ClassStatement(List<ASTree> asTrees)
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

        public string SuperClass
        {
            get
            {
                if (this.NumberOfChildren < 3)
                {
                    return null;
                }
                else
                {
                    return ((ASTLeaf)this.GetChild(1)).Token.Text;
                }
            }
        }

        public ClassBody Body
        {
            get
            {
                return (ClassBody)this.GetChild(this.NumberOfChildren - 1);
            }
        }

        public override string ToString()
        {
            string parent = this.SuperClass;

            if (string.IsNullOrEmpty(parent))
            {
                parent = "*";
            }

            return string.Format("(class {0} {1} {1})", this.Name, parent, this.Body);
        }
    }
}