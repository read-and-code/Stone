using System;
using System.Collections.Generic;
using Stone.Exceptions;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ArrayReference : Postfix
    {
        public ArrayReference(List<ASTree> asTrees)
            : base(asTrees)
        {
        }

        public ASTree Index
        {
            get
            {
                return this.GetChild(0);
            }
        }

        public override object Eval(IEnvironment environment, object value)
        {
            if (value is object[])
            {
                object index = this.Index.Eval(environment);

                if (index is int)
                {
                    return ((object[])value)[Convert.ToInt32(index)];
                }
            }

            throw new StoneException("Bad array access", this);
        }

        public override string ToString()
        {
            return string.Format("[{0}]", this.Index);
        }
    }
}