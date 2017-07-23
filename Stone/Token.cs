namespace Stone
{
    public abstract class Token
    {
        public static readonly Token EOF = new EOFToken(-1);
        public static readonly string EOL = "\\n";

        protected Token(int lineNumber)
        {
            this.LineNumber = lineNumber;
        }

        public int LineNumber { get; private set; }

        public bool IsIdentifier
        {
            get
            {
                return false;
            }
        }

        public bool IsNumber
        {
            get
            {
                return false;
            }
        }

        public bool IsString
        {
            get
            {
                return false;
            }
        }

        public int Number
        {
            get
            {
                throw new StoneException("Not number token");
            }
        }

        public string Text
        {
            get
            {
                return string.Empty;
            }
        }
    }
}