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

        public string Name
        {
            get;
        }

        public MethodInfo MethodInfo
        {
            get;
        }

        public int NumberOfParameters
        {
            get;
        }

        public object Invoke(object[] arguments, ASTree asTree)
        {
            try
            {
                return this.MethodInfo.Invoke(null, arguments);
            }
            catch
            {
                throw new StoneException(string.Format("Bad native function call: {0}", this.Name), asTree);
            }
        }

        public override string ToString()
        {
            return string.Format("<native:{0}>", this.GetHashCode());
        }
    }
}