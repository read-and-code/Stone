using Stone.Exceptions;

namespace Stone.Tokens
{
    public abstract class Token
    {
        public static readonly Token EOF = new EOFToken(-1);
        public static readonly string EOL = "\\n";

        protected Token(int lineNumber)
        {
            this.LineNumber = lineNumber;
        }

        public int LineNumber
        {
            get;
        }

        public virtual bool IsIdentifier
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsNumber
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsString
        {
            get
            {
                return false;
            }
        }

        public virtual int Number
        {
            get
            {
                throw new StoneException("Not number token");
            }
        }

        public virtual string Text
        {
            get
            {
                return string.Empty;
            }
        }
    }
}