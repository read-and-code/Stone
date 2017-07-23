namespace Stone
{
    public class StringToken : Token
    {
        private string literal;

        protected StringToken(int lineNumber, string literal)
            : base(lineNumber)
        {
            this.literal = literal;
        }

        public override bool IsString
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
                return this.literal;
            }
        }
    }
}