namespace Stone.Tokens
{
    public class IdentifierToken : Token
    {
        public IdentifierToken(int lineNumber, string identifier)
            : base(lineNumber)
        {
            this.Text = identifier;
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
            get;
        }

        public override string ToString()
        {
            return string.Format("Token: identifier, name: {0}", this.Text);
        }
    }
}