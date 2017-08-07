using System.Collections.Generic;
using Stone.Exceptions;
using Stone.Interpreter;

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

        public override object Eval(IEnvironment environment, object value)
        {
            if (!(value is Function))
            {
                throw new StoneException("Bad function", this);
            }

            Function function = (Function)value;
            ParameterList parameters = function.Parameters;

            if (this.Size != parameters.Size)
            {
                throw new StoneException("Bad number of arguments", this);
            }

            int index = 0;
            IEnvironment newEnvironment = function.MakeEnvironment();

            foreach (ASTree asTree in this)
            {
                parameters.Eval(newEnvironment, index, asTree.Eval(environment));

                index++;
            }

            return function.Body.Eval(newEnvironment);
        }
    }
}