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

        public static bool Right
        {
            get
            {
                return false;
            }
        }

        public void Put(string name, int precedenceValue, bool isLeftAssociative)
        {
            this.Add(name, new Precedence(precedenceValue, isLeftAssociative));
        }
    }
}