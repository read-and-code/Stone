namespace Stone.Tokens
{
    public class EOFToken : Token
    {
        public EOFToken(int lineNumber)
            : base(lineNumber)
        {
        }

        public override string ToString()
        {
            return "Token: end of file";
        }
    }
}