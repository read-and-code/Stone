using System.Collections.Generic;
using Stone.Exceptions;
using Stone.Interpreter;

namespace Stone.AST
{
    public class DefStatement : ASTBranchNode
    {
        public DefStatement(List<ASTNode> children)
            : base(children)
        {
        }

        public string Name
        {
            get
            {
                return ((ASTLeaf)this.GetChild(0)).Token.Text;
            }
        }

        public ParameterList Parameters
        {
            get
            {
                return (ParameterList)this.GetChild(1);
            }
        }

        public BlockStatement Body
        {
            get
            {
                return (BlockStatement)this.GetChild(2);
            }
        }

        public int Size
        {
            get;
            set;
        }

        private int Index
        {
            get;
            set;
        }

        public override object Eval(IEnvironment environment)
        {
            environment.Put(0, this.Index, new Function(this.Parameters, this.Body, environment, this.Size));

            return this.Name;
        }

        public override void Lookup(SymbolTable symbolTable)
        {
            this.Index = symbolTable.PutNew(this.Name);
            this.Size = AnonymousFunction.Lookup(symbolTable, this.Parameters, this.Body);
        }

        public void LookupAsMethod(SymbolTable symbolTable)
        {
            SymbolTable newSymbolTable = new SymbolTable(symbolTable);

            newSymbolTable.PutNew("this");

            this.Parameters.Lookup(newSymbolTable);
            this.Body.Lookup(newSymbolTable);

            this.Size = newSymbolTable.Size;
        }

        public override string ToString()
        {
            return string.Format("(def {0} {1} {2})", this.Name, this.Parameters, this.Body);
        }
    }
}