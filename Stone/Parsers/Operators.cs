using System.Collections.Generic;

namespace Stone.Parsers
{
    public class Operators : Dictionary<string, Precedence>
    {
        public static bool Left
        {
            get
            {
                return true;
            }
        }

        public static bool RIGHT
        {
            get
            {
                return true;
            }
        }

        public void Put(string name, int precedenceValue, bool leftAssociative)
        {
            this.Add(name, new Precedence(precedenceValue, leftAssociative));
        }
    }
}