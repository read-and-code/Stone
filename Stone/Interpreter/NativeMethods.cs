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

        public static void AppendToEnvironment(IEnvironment environment)
        {
            AppendNativeMethods(environment);
        }

        private static void AppendNativeMethods(IEnvironment environment)
        {
            Type type = typeof(NativeMethods);

            AppendNativeMethod(environment, "print", type, "Print", typeof(object));
            AppendNativeMethod(environment, "length", type, "Length", typeof(string));
            AppendNativeMethod(environment, "toInt", type, "ToInt", typeof(object));
            AppendNativeMethod(environment, "currentTime", type, "CurrentTime");
        }

        private static void AppendNativeMethod(IEnvironment environment, string methodName, Type type, string nativeMethodName, params Type[] types)
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