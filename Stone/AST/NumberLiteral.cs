namespace Stone.AST
{
    public class NumberLiteral : ASTLeaf
    {
        public NumberLiteral(Token token)
            : base(token)
        {
        }

        public int Value
        {
            get
            {
                return this.Token.Number;
            }
        }
    }
}