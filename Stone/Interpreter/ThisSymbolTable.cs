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

        public override Location Put(string key)
        {
            Location location = this.OuterSymbolTable.Put(key);

            if (location.Nest >= 0)
            {
                location.Nest++;
            }

            return location;
        }
    }
}