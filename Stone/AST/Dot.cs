using System.Collections.Generic;
using Stone.Exceptions;
using Stone.Interpreter;

namespace Stone.AST
{
    public class Dot : Postfix
    {
        public Dot(List<ASTree> asTrees)
            : base(asTrees)
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
                NestedEnvironment nestedEnvironment = new NestedEnvironment(classInfo.Environment);
                StoneObject stoneObject = new StoneObject(nestedEnvironment);

                nestedEnvironment.PutNew("this", stoneObject);

                this.InitializeObject(classInfo, nestedEnvironment);

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
    }
}