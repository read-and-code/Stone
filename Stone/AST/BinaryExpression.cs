using System;
using System.Collections;
using System.Collections.Generic;
using Stone.Exceptions;
using Stone.Interpreter;

namespace Stone.AST
{
    public class BinaryExpression : ASTList
    {
        public BinaryExpression(List<ASTree> children)
            : base(children)
        {
        }

        public ASTree Left
        {
            get
            {
                return this.Children[0];
            }
        }

        public string Operator
        {
            get
            {
                return ((ASTLeaf)this.Children[1]).Token.Text;
            }
        }

        public ASTree Right
        {
            get
            {
                return this.Children[2];
            }
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

        private object ComputeAssignment(IEnvironment environment, object value)
        {
            ASTree left = this.Left;

            if (left is Name)
            {
                environment.Put((left as Name).Value, value);

                return value;
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
    }
}