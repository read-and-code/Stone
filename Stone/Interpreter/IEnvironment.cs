namespace Stone.Interpreter
{
    public interface IEnvironment
    {
        object Get(string name);

        object Get(int nest, int index);

        void Put(string name, object value);

        void Put(int nest, int index, object value);

        void PutNew(string name, object value);

        IEnvironment Where(string name);

        SymbolTable GetSymbolTable();
    }
}