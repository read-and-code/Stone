using System.Collections.Generic;
using Stone.Exceptions;
using Stone.Interpreter;

namespace Stone.AST
{
    public class Dot : Postfix
    {
        public Dot(List<ASTNode> children)
            : base(children)
        {
        }

        public string Name
        {
            get
            {
                return ((ASTLeaf)this.GetChild(0)).Token.Text;
            }
        }

        public override object Eval(IEnvironment environment, object value)
        {
            string memberName = this.Name;

            if (value is ClassInfo && memberName == "new")
            {
                ClassInfo classInfo = (ClassInfo)value;
                IEnvironment newEnvironment = new Environment(1, classInfo.Environment);
                StoneObject stoneObject = new StoneObject(classInfo, classInfo.Size);

                newEnvironment.Put(0, 0, stoneObject);

                this.InitializeObject(classInfo, stoneObject, newEnvironment);

                return stoneObject;
            }
            else if (value is StoneObject)
            {
                try
                {
                    return ((StoneObject)value).Read(memberName);
                }
                catch
                {
                }
            }

            throw new StoneException(string.Format("Bad member access: {0}", memberName), this);
        }

        public override string ToString()
        {
            return string.Format(".{0}", this.Name);
        }

        private void InitializeObject(ClassInfo classInfo, IEnvironment environment)
        {
            if (classInfo.SuperClass != null)
            {
                this.InitializeObject(classInfo.SuperClass, environment);
            }

            classInfo.Body.Eval(environment);
        }

        private void InitializeObject(ClassInfo classInfo, StoneObject stoneObject, IEnvironment environment)
        {
            if (classInfo.SuperClass != null)
            {
                this.InitializeObject(classInfo.SuperClass, stoneObject, environment);
            }

            classInfo.Body.Eval(environment);
        }
    }
}