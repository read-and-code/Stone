using System.Collections.Generic;
using System.Linq;
using Stone.Exceptions;
using Stone.Interpreter;

namespace Stone.AST
{
    public class Arguments : Postfix
    {
        public Arguments(List<ASTNode> children)
            : base(children)
        {
        }

        private int Size
        {
            get
            {
                return this.NumberOfChildren;
            }
        }

        public override object Eval(IEnvironment environment, object value)
        {
            if (!(value is Function || value is NativeFunction))
            {
                throw new StoneException("Bad function", this);
            }

            if (value is NativeFunction)
            {
                return this.EvalNativeFunction(environment, value);
            }

            Function function = (Function)value;
            ParameterList parameters = function.Parameters;

            if (this.Size != parameters.Size)
            {
                throw new StoneException("Bad number of arguments", this);
            }

            int index = 0;
            IEnvironment newEnvironment = function.MakeEnvironment();

            foreach (ASTNode astNode in this)
            {
                parameters.Eval(newEnvironment, index, astNode.Eval(environment));

                index++;
            }

            return function.Body.Eval(newEnvironment);
        }

        private object EvalNativeFunction(IEnvironment environment, object value)
        {
            NativeFunction nativeFunction = (NativeFunction)value;

            if (this.Size != nativeFunction.NumberOfParameters)
            {
                throw new StoneException("Bad number of arguments", this);
            }

            int index = 0;
            object[] arguments = new object[nativeFunction.NumberOfParameters];

            foreach (ASTNode astNode in this)
            {
                arguments[index++] = astNode.Eval(environment);
            }

            return nativeFunction.Invoke(arguments, this);
        }
    }
}