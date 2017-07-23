using System.Collections;

namespace Stone.AST
{
    public abstract class ASTree : IEnumerable
    {
        public abstract int NumberOfChildren { get; }

        public abstract string Location { get; }

        public abstract ASTree GetChild(int i);

        public abstract IEnumerator GetChildren();

        public IEnumerator GetEnumerator()
        {
            return this.GetChildren();
        }
    }
}