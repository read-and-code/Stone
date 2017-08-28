using System.Collections.Generic;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ClassStatement : ASTBranchNode
    {
        public ClassStatement(List<ASTNode> children)
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

        public string SuperClass
        {
            get
            {
                if (this.NumberOfChildren < 3)
                {
                    return null;
                }
                else
                {
                    return ((ASTLeaf)this.GetChild(1)).Token.Text;
                }
            }
        }

        public ClassBody Body
        {
            get
            {
                return (ClassBody)this.GetChild(this.NumberOfChildren - 1);
            }
        }

        public override object Eval(IEnvironment environment)
        {
            SymbolTable methodsSymbolTable = new MemberSymbolTable(environment.GetSymbolTable(), MemberSymbolTable.EntityTypeMethod);
            SymbolTable fieldsSymbolTable = new MemberSymbolTable(methodsSymbolTable, MemberSymbolTable.EntityTypeField);
            ClassInfo classInfo = new ClassInfo(this, environment, methodsSymbolTable, fieldsSymbolTable);

            environment.Put(this.Name, classInfo);

            List<DefStatement> methodDefinitions = new List<DefStatement>();

            if (classInfo.SuperClass != null)
            {
                classInfo.SuperClass.CopyTo(methodsSymbolTable, fieldsSymbolTable, methodDefinitions);
            }

            SymbolTable newSymbolTable = new ThisSymbolTable(fieldsSymbolTable);

            this.Body.Lookup(newSymbolTable, methodsSymbolTable, fieldsSymbolTable, methodDefinitions);

            classInfo.MethodDefinitions = methodDefinitions;

            return this.Name;
        }

        public override void Lookup(SymbolTable symbolTable)
        {
        }

        public override string ToString()
        {
            string parent = this.SuperClass;

            if (string.IsNullOrEmpty(parent))
            {
                parent = "*";
            }

            return string.Format("(class {0} {1} {1})", this.Name, parent, this.Body);
        }
    }
}