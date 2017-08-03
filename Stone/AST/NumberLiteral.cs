using Stone.Interpreter;
using Stone.Tokens;

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

        public override object Eval(IEnvironment environment)
        {
            return this.Value;
        }
    }
}