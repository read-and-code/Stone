using Stone.AST;

namespace Stone.Interpreter
{
    public class Function
    {
        public Function(ParameterList parameters, BlockStatement body, IEnvironment environment)
        {
            this.Parameters = parameters;
            this.Body = body;
            this.Environment = environment;
        }

        public ParameterList Parameters { get; private set; }

        public BlockStatement Body { get; private set; }

        protected IEnvironment Environment { get; private set; }

        public IEnvironment MakeEnvironment()
        {
            return new NestedEnvironment(this.Environment);
        }

        public override string ToString()
        {
            return string.Format("<func:{0}>", this.GetHashCode());
        }
    }
}