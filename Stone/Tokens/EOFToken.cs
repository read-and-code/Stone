namespace Stone.Tokens
{
    public class EOFToken : Token
    {
        public EOFToken(int lineNumber)
            : base(lineNumber)
        {
        }
    }
}