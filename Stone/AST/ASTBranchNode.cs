using System.Collections.Generic;
using System.Linq;
using Stone.Exceptions;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ASTBranchNode : ASTNode
    {
        public ASTBranchNode(List<ASTNode> children)
        {
            this.Children = children;
        }

        public override string Location
        {
            get
            {
                return this.Children.Select(child => child.Location)
                    .FirstOrDefault(location => !string.IsNullOrEmpty(location));
            }
        }

        public override int NumberOfChildren
        {
            get
            {
                return this.Children.Count;
            }
        }

        protected List<ASTNode> Children
        {
            get;
        }

        public override ASTNode GetChild(int i)
        {
            return this.Children[i];
        }

        public override object Eval(IEnvironment environment)
        {
            throw new StoneException(string.Format("Cannot eval: {0}", this), this);
        }

        public override object Eval(IEnvironment environment, object value)
        {
            throw new StoneException(string.Format("Cannot eval: {0}", this), this);
        }

        public override void Eval(IEnvironment environment, int index, object value)
        {
            throw new StoneException(string.Format("Cannot eval: {0}", this), this);
        }

        public override void Lookup(SymbolTable symbolTable)
        {
            foreach (ASTNode astNode in this)
            {
                astNode.Lookup(symbolTable);
            }
        }

        public override IEnumerator<ASTNode> GetChildren()
        {
            return this.Children.GetEnumerator();
        }

        public override string ToString()
        {
            return "(" + string.Join(" ", this.Children) + ")";
        }
    }
}