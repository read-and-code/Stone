using System.Collections;
using System.Collections.Generic;
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
                foreach (ASTree asTree in this.Children)
                {
                    if (!string.IsNullOrEmpty(asTree.Location))
                    {
                        return asTree.Location;
                    }
                }

                return string.Empty;
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
            string separator = string.Empty;
            StringBuilder stringBuilder = new StringBuilder('(');

            foreach (ASTree asTree in this.Children)
            {
                stringBuilder.Append(separator);
                stringBuilder.Append(asTree.ToString());
                separator = " ";
            }

            return stringBuilder.Append(')').ToString();
        }
    }
}