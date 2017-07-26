using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stone.AST
{
    public class ASTList : ASTree
    {
        public ASTList(List<ASTree> children)
        {
            this.Children = children;
        }

        public override int NumberOfChildren
        {
            get
            {
                return this.Children.Count;
            }
        }

        public override string Location
        {
            get
            {
                return this.Children.Select(asTree => asTree.Location)
                    .FirstOrDefault(location => !string.IsNullOrEmpty(location));
            }
        }

        protected List<ASTree> Children { get; private set; }

        public override ASTree GetChild(int i)
        {
            return this.Children[i];
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