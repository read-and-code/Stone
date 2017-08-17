using System.Collections.Generic;

namespace Stone.Interpreter
{
    public class NestedEnvironment : IEnvironment
    {
        public NestedEnvironment()
            : this(null)
        {
        }

        public NestedEnvironment(IEnvironment environment)
        {
            this.OuterEnvironment = environment;
            this.Values = new Dictionary<string, object>();
        }

        public IEnvironment OuterEnvironment
        {
            get;
            set;
        }

        protected Dictionary<string, object> Values
        {
            get;
        }

        public object Get(string name)
        {
            object value = null;

            this.Values.TryGetValue(name, out value);

            if (value == null && this.OuterEnvironment != null)
            {
                return this.OuterEnvironment.Get(name);
            }
            else
            {
                return value;
            }
        }

        public void Put(string name, object value)
        {
            IEnvironment environment = this.Where(name);

            if (environment == null)
            {
                environment = this;
            }

            environment.PutNew(name, value);
        }

        public void PutNew(string name, object value)
        {
            this.Values[name] = value;
        }

        public IEnvironment Where(string name)
        {
            if (this.Values.ContainsKey(name) && this.Values[name] != null)
            {
                return this;
            }
            else if (this.OuterEnvironment == null)
            {
                return null;
            }
            else
            {
                return this.OuterEnvironment.Where(name);
            }
        }
    }
}