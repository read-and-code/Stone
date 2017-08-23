using System.Collections.Generic;
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

        private int Index
        {
            get;
            set;
        }

        private int Size
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

        public override string ToString()
        {
            return string.Format("(def {0} {1} {2})", this.Name, this.Parameters, this.Body);
        }
    }
}