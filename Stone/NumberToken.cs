using System;

namespace Stone
{
    public class NumberToken : Token
    {
        private int value;

        public NumberToken(int lineNumber, int value)
            : base(lineNumber)
        {
            this.value = value;
        }

        public override bool IsNumber
        {
            get
            {
                return true;
            }
        }

        public override string Text
        {
            get
            {
                return Convert.ToString(this.value);
            }
        }

        public override int Number
        {
            get
            {
                return this.value;
            }
        }
    }
}