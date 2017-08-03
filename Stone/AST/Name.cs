using Stone.Exceptions;
using Stone.Interpreter;
using Stone.Tokens;

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

        public override object Eval(IEnvironment environment)
        {
            object value = environment.Get(this.Value);

            if (value == null)
            {
                throw new StoneException(string.Format("Undefined name: {0}", this.Value), this);
            }
            else
            {
                return value;
            }
        }
    }
}