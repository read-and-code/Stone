using System.Collections.Generic;
using Stone.Exceptions;

namespace Stone.Interpreter
{
    public class StoneObject
    {
        public StoneObject(ClassInfo classInfo, int size)
        {
            this.ClassInfo = classInfo;
            this.Fields = new object[size];
        }

        public ClassInfo ClassInfo
        {
            get;
        }

        private object[] Fields
        {
            get;
        }

        public object Read(string memberName)
        {
            int index = this.ClassInfo.GetFieldIndex(memberName);

            if (index != -1)
            {
                return this.Fields[index];
            }
            else
            {
                index = this.ClassInfo.GetMethodIndex(memberName);

                if (index != -1)
                {
                    return this.GetMethod(index);
                }
            }

            throw new AccessException();
        }

        public object Read(int index)
        {
            return this.Fields[index];
        }

        public void Write(string memberName, object value)
        {
            int index = this.ClassInfo.GetFieldIndex(memberName);

            if (index == -1)
            {
                throw new AccessException();
            }
            else
            {
                this.Fields[index] = value;
            }
        }

        public void Write(int index, object value)
        {
            this.Fields[index] = value;
        }

        public object GetMethod(int index)
        {
            return this.ClassInfo.GetMethod(this, index);
        }
    }
}