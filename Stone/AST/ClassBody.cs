using System.Collections.Generic;
using Stone.AST;
using Stone.Interpreter;

namespace Stone.AST
{
    public class ClassBody : ASTBranchNode
    {
        public ClassBody(List<ASTNode> children)
            : base(children)
        {
        }

        public override object Eval(IEnvironment environment)
        {
            foreach (ASTNode astNode in this)
            {
                if (!(astNode is DefStatement))
                {
                    astNode.Eval(environment);
                }
            }

            return null;
        }

        public void Lookup(SymbolTable symbolTable, SymbolTable methodsSymbolTable, SymbolTable fieldsSymbolTable, List<DefStatement> methodDefinitions)
        {
            foreach (ASTNode astNode in this)
            {
                if (astNode is DefStatement)
                {
                    DefStatement defStatement = (DefStatement)astNode;
                    int oldSize = methodsSymbolTable.Size;
                    int index = methodsSymbolTable.PutNew(defStatement.Name);

                    if (index >= oldSize)
                    {
                        methodDefinitions.Add(defStatement);
                    }
                    else
                    {
                        methodDefinitions[index] = defStatement;
                    }

                    defStatement.LookupAsMethod(fieldsSymbolTable);
                }
                else
                {
                    astNode.Lookup(symbolTable);
                }
            }
        }
    }
}