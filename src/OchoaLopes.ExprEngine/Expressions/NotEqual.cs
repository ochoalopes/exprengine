using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class NotEqual : BinaryOperation
    {
        public NotEqual(IExpression left, IExpression right) : base(left, right) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var leftResult = Left.Evaluate(variables);
            var rightResult = Right.Evaluate(variables);

            if (leftResult is decimal leftDecimal && rightResult is decimal rightDecimal)
            {
                return leftDecimal != rightDecimal;
            }
            else if (leftResult is double leftDouble && rightResult is double rightDouble)
            {
                return leftDouble != rightDouble;
            }
            else if (leftResult is int leftInt && rightResult is int rightint)
            {
                return leftInt != rightint;
            }
            else if (leftResult is bool leftBool && rightResult is bool rightBool)
            {
                return leftBool != rightBool;
            }
            else if (leftResult is string leftString && rightResult is string rightString)
            {
                return leftString != rightString;
            }
            else
            {
                return leftResult != rightResult;
            }
        }
    }
}