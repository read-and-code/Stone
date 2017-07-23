namespace Stone
{
    public class IdentifierToken : Token
    {
        private string text;

        protected IdentifierToken(int lineNumber, string identifier)
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