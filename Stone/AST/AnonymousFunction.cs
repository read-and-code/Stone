using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class AnonymousFunction : ASTBranchNode
    {
        public AnonymousFunction(List<ASTNode> children)
            : base(children)
        {
        }

        public ParameterList Parameters
        {
            get
            {
                return (ParameterList)this.GetChild(0);
            }
        }

        public BlockStatement Body
        {
            get
            {
                return (BlockStatement)this.GetChild(1);
            }
        }

        private int Size
        {
            get;
            set;
        }

        public static int Lookup(SymbolTable symbolTable, ParameterList parameterList, BlockStatement body)
        {
            SymbolTable newSymbolTable = new SymbolTable(symbolTable);

            parameterList.Lookup(newSymbolTable);
            body.Lookup(newSymbolTable);

            return newSymbolTable.Size;
        }

        public override object Eval(IEnvironment environment)
        {
            return new Function(this.Parameters, this.Body, environment, this.Size);
        }

        public override void Lookup(SymbolTable symbolTable)
        {
            this.Size = Lookup(symbolTable, this.Parameters, this.Body);
        }

        public override string ToString()
        {
            return string.Format("(func {0} {1})", this.Parameters, this.Body);
        }
    }
}