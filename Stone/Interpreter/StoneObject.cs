using Stone.Exceptions;

namespace Stone.Interpreter
{
    public class StoneObject
    {
        public StoneObject(IEnvironment environment)
        {
            this.Environment = environment;
        }

        public IEnvironment Environment
        {
            get;
        }

        public object Read(string memberName)
        {
            return this.GetEnvironment(memberName).Get(memberName);
        }

        public void Write(string memberName, object value)
        {
            this.GetEnvironment(memberName).PutNew(memberName, value);
        }

        private IEnvironment GetEnvironment(string memberName)
        {
            IEnvironment environment = this.Environment.Where(memberName);

            if (environment != null && environment == this.Environment)
            {
                return environment;
            }
            else
            {
                throw new AccessException();
            }
        }
    }
}