using System.Collections.Generic;

namespace Stone.AST
{
    public class Arguments : Postfix
    {
        public Arguments(List<ASTree> asTrees)
            : base(asTrees)
        {
        }

        public int Size
        {
            get
            {
                return this.NumberOfChildren;
            }
        }
    }
}