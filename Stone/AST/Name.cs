namespace Stone.AST
{
    public class Name : ASTLeaf
    {
        public Name(Token token)
            : base(token)
        {
        }

        public string Value
        {
            get
            {
                return this.Token.Text;
            }
        }
    }
}