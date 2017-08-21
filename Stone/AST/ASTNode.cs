using System.Collections;
using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public abstract class ASTNode : IEnumerable<ASTNode>
    {
        public abstract string Location
        {
            get;
        }

        public abstract int NumberOfChildren
        {
            get;
        }

        public abstract ASTNode GetChild(int i);

        public abstract object Eval(IEnvironment environment);

        public abstract object Eval(IEnvironment environment, object value);

        public abstract void Eval(IEnvironment environment, int index, object value);

        public abstract IEnumerator<ASTNode> GetChildren();

        public IEnumerator<ASTNode> GetEnumerator()
        {
            return this.GetChildren();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}