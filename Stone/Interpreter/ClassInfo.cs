using System.Collections.Generic;
using Stone.AST;
using Stone.Exceptions;

namespace Stone.Interpreter
{
    public class ClassInfo
    {
        public ClassInfo(ClassStatement classStatement, IEnvironment environment, SymbolTable methodsSymbolTable, SymbolTable fieldsSymbolTable)
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

            this.MethodsSymbolTable = methodsSymbolTable;
            this.FieldsSymbolTable = fieldsSymbolTable;
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

        public SymbolTable MethodsSymbolTable
        {
            get;
        }

        public SymbolTable FieldsSymbolTable
        {
            get;
        }

        public List<DefStatement> MethodDefinitions
        {
            get;
            set;
        }

        public ClassBody Body
        {
            get
            {
                return this.Definition.Body;
            }
        }

        public int Size
        {
            get
            {
                return this.FieldsSymbolTable.Size;
            }
        }

        public void CopyTo(SymbolTable methodsSymbolTable, SymbolTable fieldsSymbolTable, List<DefStatement> methodDefinitions)
        {
            methodsSymbolTable.Append(this.MethodsSymbolTable);
            fieldsSymbolTable.Append(this.FieldsSymbolTable);

            if (this.MethodDefinitions != null)
            {
                methodDefinitions.AddRange(this.MethodDefinitions);
            }
        }

        public int GetMethodIndex(string name)
        {
            return this.MethodsSymbolTable.FindIndex(name);
        }

        public int GetFieldIndex(string name)
        {
            return this.FieldsSymbolTable.FindIndex(name);
        }

        public object GetMethod(StoneObject self, int index)
        {
            DefStatement defStatement = this.MethodDefinitions[index];

            return new Function(defStatement.Parameters, defStatement.Body, this.Environment, defStatement.Size, self);
        }

        public override string ToString()
        {
            return string.Format("<class {0}>", this.Name);
        }
    }
}