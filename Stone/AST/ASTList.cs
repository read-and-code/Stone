using System.Collections.Generic;
using System.Linq;
using Stone.Exceptions;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ASTList : ASTree
    {
        public ASTList(List<ASTree> children)
        {
            this.Children = children;
        }

        public override string Location
        {
            get
            {
                return this.Children.Select(asTree => asTree.Location)
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

        protected List<ASTree> Children { get; private set; }

        public override ASTree GetChild(int i)
        {
            return this.Children[i];
        }

        public override object Eval(IEnvironment environment)
        {
            throw new StoneException(string.Format("Cannot eval: {0}", this.ToString()), this);
        }

        public override object Eval(IEnvironment environment, object value)
        {
            throw new StoneException(string.Format("Cannot eval: {0}", this.ToString()), this);
        }

        public override void Eval(IEnvironment environment, int index, object value)
        {
            throw new StoneException(string.Format("Cannot eval: {0}", this.ToString()), this);
        }

        public override IEnumerator<ASTree> GetChildren()
        {
            return this.Children.GetEnumerator();
        }

        public override string ToString()
        {
            return "(" + string.Join(" ", this.Children) + ")";
        }
    }
}