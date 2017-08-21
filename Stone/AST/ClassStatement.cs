using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ClassStatement : ASTBranchNode
    {
        public ClassStatement(List<ASTNode> children)
            : base(children)
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

        public override object Eval(IEnvironment environment)
        {
            ClassInfo classInfo = new ClassInfo(this, environment);

            environment.Put(this.Name, classInfo);

            return this.Name;
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