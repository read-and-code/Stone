using Stone.AST;
using Stone.Exceptions;

namespace Stone.Interpreter
{
    public class ClassInfo
    {
        public ClassInfo(ClassStatement classStatement, IEnvironment environment)
        {
            this.Definition = classStatement;
            this.Environment = environment;

            object superClass = string.IsNullOrEmpty(classStatement.SuperClass) ? null : environment.Get(classStatement.SuperClass);

            if (superClass == null)
            {
                this.SuperClass = null;
            }
            else if (superClass is ClassInfo)
            {
                this.SuperClass = (ClassInfo)superClass;
            }
            else
            {
                throw new StoneException(string.Format("Unknown super class: {0}", classStatement.SuperClass), classStatement);
            }
        }

        public ClassStatement Definition
        {
            get;
        }

        public IEnvironment Environment
        {
            get;
        }

        public ClassInfo SuperClass
        {
            get;
        }

        public string Name
        {
            get
            {
                return this.Definition.Name;
            }
        }

        public ClassBody Body
        {
            get
            {
                return this.Definition.Body;
            }
        }

        public override string ToString()
        {
            return string.Format("<class {0}>", this.Name);
        }
    }
}