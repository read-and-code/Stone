using System.Collections;
using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public abstract class ASTree : IEnumerable<ASTree>
    {
        public abstract int NumberOfChildren { get; }

        public abstract string Location { get; }

        public abstract ASTree GetChild(int i);

        public abstract IEnumerator<ASTree> GetChildren();

        public abstract object Eval(IEnvironment environment);

        public IEnumerator<ASTree> GetEnumerator()
        {
            return this.GetChildren();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}