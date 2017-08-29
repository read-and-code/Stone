using System.Collections.Generic;
using System.Linq;

namespace Stone.Interpreter
{
    public class SymbolTable
    {
        public SymbolTable()
            : this(null)
        {
        }

        public SymbolTable(SymbolTable outerSymbolTable)
        {
            this.OuterSymbolTable = outerSymbolTable;
            this.Values = new Dictionary<string, int>();
        }

        public SymbolTable OuterSymbolTable
        {
            get;
        }

        public int Size
        {
            get
            {
                return this.Values.Count;
            }
        }

        private Dictionary<string, int> Values
        {
            get;
            set;
        }

        public void Append(SymbolTable symbolTable)
        {
            this.Values = this.Values.Concat(symbolTable.Values).ToDictionary(x => x.Key, x => x.Value);
        }

        public int FindIndex(string key)
        {
            return this.Values.ContainsKey(key) ? this.Values[key] : -1;
        }

        public EntityLocation GetEntityLocation(string key)
        {
            return this.GetEntityLocation(key, 0);
        }

        public virtual EntityLocation GetEntityLocation(string key, int nest)
        {
            int index = this.FindIndex(key);

            if (index == -1)
            {
                if (this.OuterSymbolTable == null)
                {
                    return null;
                }
                else
                {
                    return this.OuterSymbolTable.GetEntityLocation(key, nest + 1);
                }
            }
            else
            {
                return new EntityLocation(nest, index);
            }
        }

        public virtual int PutNew(string key)
        {
            int index = this.FindIndex(key);

            if (index == -1)
            {
                return this.Add(key);
            }
            else
            {
                return index;
            }
        }

        public virtual EntityLocation Put(string key)
        {
            EntityLocation entityLocation = this.GetEntityLocation(key, 0);

            if (entityLocation == null)
            {
                return new EntityLocation(0, this.Add(key));
            }
            else
            {
                return entityLocation;
            }
        }

        public int Add(string key)
        {
            int index = this.Size;

            this.Values.Add(key, index);

            return index;
        }
    }
}