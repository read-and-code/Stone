using Stone.Interpreter;
using Stone.Tokens;

namespace Stone.AST
{
    public class StringLiteral : ASTLeaf
    {
        public StringLiteral(Token token)
            : base(token)
        {
        }

        private string Value
        {
            get
            {
                return this.Token.Text;
            }
        }

        public override object Eval(IEnvironment environment)
        {
            return this.Value;
        }
    }
}