using Stone.AST;

namespace Stone.Interpreter
{
    public class Function
    {
        public Function(ParameterList parameters, BlockStatement body, IEnvironment environment)
            : this(parameters, body, environment, 0)
        {
        }

        public Function(ParameterList parameters, BlockStatement body, IEnvironment environment, int memorySize)
        {
            this.Parameters = parameters;
            this.Body = body;
            this.Environment = environment;
            this.MemorySize = memorySize;
        }

        public Function(ParameterList parameters, BlockStatement body, IEnvironment environment, int memorySize, StoneObject self)
            : this(parameters, body, environment, memorySize)
        {
            this.Self = self;
        }

        public StoneObject Self
        {
            get;
        }

        public ParameterList Parameters
        {
            get;
        }

        public BlockStatement Body
        {
            get;
        }

        protected IEnvironment Environment
        {
            get;
        }

        private int MemorySize
        {
            get;
        }

        public IEnvironment MakeEnvironment()
        {
            IEnvironment environment = new Environment(this.MemorySize, this.Environment);

            environment.Put(0, 0, this.Self);

            return environment;
        }

        public override string ToString()
        {
            return string.Format("<func:{0}>", this.GetHashCode());
        }
    }
}