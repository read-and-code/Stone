using System;
using Stone.AST;

namespace Stone.Parsers
{
    public static class ASTNodeFactory
    {
        public static ASTNode Make(Type type, object[] arguments)
        {
            return arguments != null ? (ASTNode)Activator.CreateInstance(type, arguments) : (ASTNode)Activator.CreateInstance(type);
        }
    }
}