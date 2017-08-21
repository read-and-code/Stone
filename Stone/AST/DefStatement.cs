using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class DefStatement : ASTBranchNode
    {
        public DefStatement(List<ASTNode> children)
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

        public override object Eval(IEnvironment environment)
        {
            environment.PutNew(this.Name, new Function(this.Parameters, this.Body, environment));

            return this.Name;
        }

        public override string ToString()
        {
            return string.Format("(def {0} {1} {2})", this.Name, this.Parameters, this.Body);
        }
    }
}