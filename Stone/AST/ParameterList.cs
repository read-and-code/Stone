using System.Collections.Generic;
using System.Linq;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ParameterList : ASTBranchNode
    {
        public ParameterList(List<ASTNode> children)
            : base(children)
        {
        }

        public int Size
        {
            get
            {
                return this.NumberOfChildren;
            }
        }

        private int[] Offsets
        {
            get;
            set;
        }

        public override void Eval(IEnvironment environment, int index, object value)
        {
            environment.Put(0, this.Offsets[index], value);
        }

        public override void Lookup(SymbolTable symbolTable)
        {
            this.Offsets = Enumerable.Range(0, this.Size)
                .Select(i => symbolTable.PutNew(this.GetName(i)))
                .ToArray();
        }

        private string GetName(int index)
        {
            return ((ASTLeaf)this.GetChild(index)).Token.Text;
        }
    }
}