using System.Collections.Generic;
using Stone.Exceptions;

namespace Stone.Interpreter
{
    public class BasicEnvironment : IEnvironment
    {
        public BasicEnvironment()
        {
            this.Values = new Dictionary<string, object>();
        }

        protected Dictionary<string, object> Values
        {
            get;
        }

        public object Get(string name)
        {
            return this.Values.ContainsKey(name) ? this.Values[name] : null;
        }

        public void Put(string name, object value)
        {
            this.Values[name] = value;
        }

        public void PutNew(string name, object value)
        {
            this.Put(name, value);
        }

        public IEnvironment Where(string name)
        {
            throw new StoneException("Not implemented.");
        }
    }
}