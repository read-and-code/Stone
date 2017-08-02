namespace Stone.Tokens
{
    public class StringToken : Token
    {
        private string literal;

        public StringToken(int lineNumber, string literal)
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

        public override string ToString()
        {
            return string.Format("Token: string, value: \"{0}\"", this.Text);
        }
    }
}