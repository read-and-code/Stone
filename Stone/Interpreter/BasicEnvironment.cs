using System.Collections.Generic;

namespace Stone.Interpreter
{
    public class BasicEnvironment : IEnvironment
    {
        public BasicEnvironment()
        {
            this.Values = new Dictionary<string, object>();
        }

        protected Dictionary<string, object> Values { get; private set; }

        public void Put(string name, object value)
        {
            if (this.Values.ContainsKey(name))
            {
                this.Values[name] = value;
            }
            else
            {
                this.Values.Add(name, value);
            }
        }

        public object Get(string name)
        {
            return this.Values.ContainsKey(name) ? this.Values[name] : null;
        }
    }
}