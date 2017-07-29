namespace Stone.Tokens
{
    public class IdentifierToken : Token
    {
        private string text;

        public IdentifierToken(int lineNumber, string identifier)
            : base(lineNumber)
        {
            this.text = identifier;
        }

        public override bool IsIdentifier
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
                return this.text;
            }
        }
    }
}