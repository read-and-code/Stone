using Stone.Exceptions;

namespace Stone.Interpreter
{
    public class ThisSymbolTable : SymbolTable
    {
        public ThisSymbolTable(SymbolTable outerSymbolTable)
            : base(outerSymbolTable)
        {
            this.Add("this");
        }

        public override int PutNew(string key)
        {
            throw new StoneException("Fatal");
        }

        public override EntityLocation Put(string key)
        {
            EntityLocation entityLocation = this.OuterSymbolTable.Put(key);

            if (entityLocation.Nest >= 0)
            {
                entityLocation.Nest++;
            }

            return entityLocation;
        }
    }
}