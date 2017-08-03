namespace Stone.Interpreter
{
    public interface IEnvironment
    {
         void Put(string name, object value);

         object Get(string name);
    }
}