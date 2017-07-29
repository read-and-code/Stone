using System;
using Stone.AST;

namespace Stone.Parsers
{
    public static class ASTreeFactory
    {
        public static ASTree Make(Type type, object[] arguments)
        {
            return (ASTree)Activator.CreateInstance(type, arguments);
        }
    }
}