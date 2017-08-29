using Stone.Exceptions;
using Stone.Interpreter;
using Stone.Tokens;

namespace Stone.AST
{
    public class Name : ASTLeaf
    {
        public Name(Token token)
            : base(token)
        {
            this.Index = -1;
        }

        private string Value
        {
            get
            {
                return this.Token.Text;
            }
        }

        private int Nest
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
            if (this.Index == -1)
            {
                return environment.Get(this.Value);
            }
            else if (this.Nest == MemberSymbolTable.EntityTypeField)
            {
                return this.GetThis(environment).Read(this.Index);
            }
            else if (this.Nest == MemberSymbolTable.EntityTypeMethod)
            {
                return this.GetThis(environment).GetMethod(this.Index);
            }
            else
            {
                return environment.Get(this.Nest, this.Index);
            }
        }

        public override void Lookup(SymbolTable symbolTable)
        {
            EntityLocation entityLocation = symbolTable.GetEntityLocation(this.Value);

            if (entityLocation == null)
            {
                throw new StoneException(string.Format("Undefined name: {0}", this.Value), this);
            }
            else
            {
                this.Nest = entityLocation.Nest;
                this.Index = entityLocation.Index;
            }
        }

        public void LookupForAssignment(SymbolTable symbolTable)
        {
            EntityLocation entityLocation = symbolTable.Put(this.Value);

            this.Nest = entityLocation.Nest;
            this.Index = entityLocation.Index;
        }

        public void EvalForAssignment(IEnvironment environment, object value)
        {
            if (this.Index == -1)
            {
                environment.Put(this.Value, value);
            }
            else if (this.Nest == MemberSymbolTable.EntityTypeField)
            {
                this.GetThis(environment).Write(this.Index, value);
            }
            else if (this.Nest == MemberSymbolTable.EntityTypeMethod)
            {
                throw new StoneException(string.Format("Cannot update a method: {0}", this.Value), this);
            }
            else
            {
                environment.Put(this.Nest, this.Index, value);
            }
        }

        private StoneObject GetThis(IEnvironment environment)
        {
            return (StoneObject)environment.Get(0, 0);
        }
    }
}