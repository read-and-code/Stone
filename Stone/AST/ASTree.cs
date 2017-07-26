using System.Collections;
using System.Collections.Generic;

namespace Stone.AST
{
    public abstract class ASTree : IEnumerable<ASTree>
    {
        public abstract int NumberOfChildren { get; }

        public abstract string Location { get; }

        public abstract ASTree GetChild(int i);

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