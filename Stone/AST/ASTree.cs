using System.Collections;
using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public abstract class ASTree : IEnumerable<ASTree>
    {
        public abstract string Location { get; }

        public abstract int NumberOfChildren { get; }

        public abstract ASTree GetChild(int i);

        public abstract object Eval(IEnvironment environment);

        public abstract object Eval(IEnvironment environment, object value);

        public abstract void Eval(IEnvironment environment, int index, object value);

        public abstract IEnumerator<ASTree> GetChildren();

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