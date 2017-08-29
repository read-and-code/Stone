namespace Stone.Interpreter
{
    public interface IEnvironment
    {
        SymbolTable SymbolTable
        {
            get;
        }

        object Get(string name);

        object Get(int nest, int index);

        void Put(string name, object value);

        void Put(int nest, int index, object value);

        void PutNew(string name, object value);

        IEnvironment Where(string name);
    }
}