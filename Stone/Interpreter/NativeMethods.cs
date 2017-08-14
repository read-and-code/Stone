using System;
using System.Reflection;
using Stone.Exceptions;

namespace Stone.Interpreter
{
    public class NativeMethods
    {
        public static int Print(object value)
        {
            Console.WriteLine(value);

            return 0;
        }

        public static int Length(string value)
        {
            return string.IsNullOrEmpty(value) ? 0 : value.Length;
        }

        public static int ToInt(object value)
        {
            return Convert.ToInt32(value);
        }

        public static int CurrentTime()
        {
            return DateTime.Now.Millisecond;
        }

        public IEnvironment GetEnvironment(IEnvironment environment)
        {
            this.AppendNativeMethods(environment);

            return environment;
        }

        private void AppendNativeMethods(IEnvironment environment)
        {
            this.AppendNativeMethod(environment, "print", this.GetType(), "Print", typeof(object));
            this.AppendNativeMethod(environment, "length", this.GetType(), "Length", typeof(string));
            this.AppendNativeMethod(environment, "toInt", this.GetType(), "ToInt", typeof(object));
            this.AppendNativeMethod(environment, "currentTime", this.GetType(), "CurrentTime");
        }

        private void AppendNativeMethod(IEnvironment environment, string methodName, Type type, string nativeMethodName, params Type[] types)
        {
            MethodInfo methodInfo;

            try
            {
                methodInfo = type.GetMethod(nativeMethodName, types);
            }
            catch
            {
                throw new StoneException(string.Format("Cannot find a native function: {0}", nativeMethodName));
            }

            environment.Put(methodName, new NativeFunction(nativeMethodName, methodInfo));
        }
    }
}