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

        public Location GetLocation(string key)
        {
            return this.GetLocation(key, 0);
        }

        public virtual Location GetLocation(string key, int nest)
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
                    return this.OuterSymbolTable.GetLocation(key, nest + 1);
                }
            }
            else
            {
                return new Location(nest, index);
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

        public virtual Location Put(string key)
        {
            Location location = this.GetLocation(key, 0);

            if (location == null)
            {
                return new Location(0, this.Add(key));
            }
            else
            {
                return location;
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