using System;
using System.Reflection;
using Stone.AST;
using Stone.Exceptions;

namespace Stone.Interpreter
{
    public class NativeFunction
    {
        public NativeFunction(string name, MethodInfo methodInfo)
        {
            this.Name = name;
            this.MethodInfo = methodInfo;
            this.NumberOfParameters = methodInfo.GetParameters().Length;
        }

        public int NumberOfParameters
        {
            get;
        }

        private string Name
        {
            get;
        }

        private MethodInfo MethodInfo
        {
            get;
        }

        public object Invoke(object[] arguments, ASTNode astNode)
        {
            try
            {
                return this.MethodInfo.Invoke(null, arguments);
            }
            catch
            {
                throw new StoneException(string.Format("Bad native function call: {0}", this.Name), astNode);
            }
        }

        public override string ToString()
        {
            return string.Format("<native:{0}>", this.GetHashCode());
        }
    }
}