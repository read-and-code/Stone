using System;
using System.Collections;
using System.Collections.Generic;
using Stone.Exceptions;
using Stone.Interpreter;

namespace Stone.AST
{
    public class BinaryExpression : ASTBranchNode
    {
        public BinaryExpression(List<ASTNode> children)
            : base(children)
        {
        }

        private ASTNode Left
        {
            get
            {
                return this.Children[0];
            }
        }

        private string Operator
        {
            get
            {
                return ((ASTLeaf)this.Children[1]).Token.Text;
            }
        }

        private ASTNode Right
        {
            get
            {
                return this.Children[2];
            }
        }

        private ClassInfo ClassInfo
        {
            get;
            set;
        }

        private int Index
        {
            get;
            set;
        }

        public override object Eval(IEnvironment environment)
        {
            if (this.Operator == "=")
            {
                object right = this.Right.Eval(environment);

                return this.ComputeAssignment(environment, right);
            }
            else
            {
                object left = this.Left.Eval(environment);
                object right = this.Right.Eval(environment);

                return this.ComputeOperator(left, this.Operator, right);
            }
        }

        public override void Lookup(SymbolTable symbolTable)
        {
            ASTNode left = this.Left;

            if (this.Operator == "=" && left is Name)
            {
                ((Name)left).LookupForAssignment(symbolTable);

                this.Right.Lookup(symbolTable);

                return;
            }

            left.Lookup(symbolTable);

            this.Right.Lookup(symbolTable);
        }

        private object ComputeAssignment(IEnvironment environment, object rightValue)
        {
            ASTNode left = this.Left;

            if (left is PrimaryExpression)
            {
                PrimaryExpression primaryExpression = (PrimaryExpression)left;

                if (primaryExpression.HasPostfix(0) && primaryExpression.GetPostfix(0) is Dot)
                {
                    object stoneObject = primaryExpression.EvalSubExpression(environment, 1);

                    if (stoneObject is StoneObject)
                    {
                        this.SetField((StoneObject)stoneObject, (Dot)primaryExpression.GetPostfix(0), rightValue);

                        return rightValue;
                    }
                }
                else if (primaryExpression.HasPostfix(0) && primaryExpression.GetPostfix(0) is ArrayReference)
                {
                    object array = primaryExpression.EvalSubExpression(environment, 1);

                    if (array is object[])
                    {
                        ArrayReference arrayReference = (ArrayReference)primaryExpression.GetPostfix(0);
                        object index = arrayReference.Index.Eval(environment);

                        if (index is int)
                        {
                            ((object[])array)[Convert.ToInt32(index)] = rightValue;

                            return rightValue;
                        }
                    }

                    throw new StoneException("Bad array access", this);
                }
            }

            if (left is Name)
            {
                ((Name)left).EvalForAssignment(environment, rightValue);

                return rightValue;
            }
            else
            {
                throw new StoneException("Bad assignment", this);
            }
        }

        private object ComputeOperator(object left, string objectOperator, object right)
        {
            if (left is int && right is int)
            {
                return this.ComputeNumber(Convert.ToInt32(left), objectOperator, Convert.ToInt32(right));
            }

            switch (objectOperator)
            {
                case "+":
                    return Convert.ToString(left) + Convert.ToString(right);
                case "==":
                    return left == null ? (right == null ? 1 : 0) : (left.Equals(right) ? 1 : 0);
                default:
                    throw new StoneException("Bad type", this);
            }
        }

        private object ComputeNumber(int left, string numberOperator, int right)
        {
            switch (numberOperator)
            {
                case "+":
                    return left + right;
                case "-":
                    return left - right;
                case "*":
                    return left * right;
                case "/":
                    return left / right;
                case "%":
                    return left % right;
                case "==":
                    return left == right ? 1 : 0;
                case ">":
                    return left > right ? 1 : 0;
                case "<":
                    return left < right ? 1 : 0;
                default:
                    throw new StoneException("Bad operator", this);
            }
        }

        private void SetField(StoneObject stoneObject, Dot expression, object rightValue)
        {
            if (stoneObject.ClassInfo != this.ClassInfo)
            {
                string memberName = expression.Name;
                this.ClassInfo = stoneObject.ClassInfo;
                int index = this.ClassInfo.GetFieldIndex(memberName);

                if (index == -1)
                {
                    throw new StoneException(string.Format("Bad member access: {0}", memberName), this);
                }

                this.Index = index;
            }

            stoneObject.Write(this.Index, rightValue);
        }
    }
}