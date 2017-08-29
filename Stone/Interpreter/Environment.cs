using System;
using System.Collections.Generic;
using Stone.Exceptions;

namespace Stone.Interpreter
{
    public class Environment : IEnvironment
    {
        public Environment()
            : this(null)
        {
        }

        public Environment(IEnvironment outerEnvironment)
            : this(10, outerEnvironment)
        {
        }

        public Environment(int size, IEnvironment outerEnvironment)
        {
            this.OuterEnvironment = outerEnvironment;
            this.SymbolTable = new SymbolTable();
            this.Values = new object[size];

            NativeMethods.AppendToEnvironment(this);
        }

        public SymbolTable SymbolTable
        {
            get;
        }

        private IEnvironment OuterEnvironment
        {
            get;
        }

        private object[] Values
        {
            get;
            set;
        }

        public object Get(int nest, int index)
        {
            if (nest == 0)
            {
                return this.Values[index];
            }
            else if (this.OuterEnvironment == null)
            {
                return null;
            }
            else
            {
                return this.OuterEnvironment.Get(nest - 1, index);
            }
        }

        public object Get(string name)
        {
            int index = this.SymbolTable.FindIndex(name);

            if (index == -1)
            {
                if (this.OuterEnvironment == null)
                {
                    return null;
                }
                else
                {
                    return this.OuterEnvironment.Get(name);
                }
            }
            else
            {
                return this.Values[index];
            }
        }

        public void Put(int nest, int index, object value)
        {
            if (nest == 0)
            {
                this.Assign(index, value);
            }
            else if (this.OuterEnvironment == null)
            {
                new StoneException("No outer environment");
            }
            else
            {
                this.OuterEnvironment.Put(nest - 1, index, value);
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
            this.Assign(this.SymbolTable.PutNew(name), value);
        }

        public IEnvironment Where(string name)
        {
            if (this.SymbolTable.FindIndex(name) != -1)
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

        private void Assign(int index, object value)
        {
            if (index >= this.Values.Length)
            {
                int newLength = this.Values.Length * 2;

                if (index >= newLength)
                {
                    newLength = index + 1;
                }

                object[] newValues = new object[newLength];

                Array.Copy(this.Values, newValues, this.Values.Length);

                this.Values = newValues;
            }

            this.Values[index] = value;
        }
    }
}