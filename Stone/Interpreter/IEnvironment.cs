namespace Stone.Interpreter
{
    public interface IEnvironment
    {
        object Get(string name);

        void Put(string name, object value);

        void PutNew(string name, object value);

        IEnvironment Where(string name);
    }
}