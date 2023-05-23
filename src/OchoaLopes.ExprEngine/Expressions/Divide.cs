using OchoaLopes.ExprEngine.Helpers;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class Divide : BinaryOperation
    {
        public Divide(IExpression left, IExpression right) : base(left, right) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var leftResult = Left.Evaluate(variables);
            var rightResult = Right.Evaluate(variables);

            if (leftResult is string leftString || rightResult is string rightString)
            {
                throw new InvalidOperationException("Both values cannot be a string");
            }

            if (rightResult is double rightDouble && Math.Abs(rightDouble) < double.Epsilon)
            {
                throw new DivideByZeroException("Division by zero is not allowed.");
            }

            if (leftResult is bool leftBool || rightResult is bool rightBool)
            {
                throw new InvalidOperationException("Both operands of a comparison cannot be a boolean type.");
            }

            return OperationHelper.Operate(leftResult, rightResult, (a, b) => a / b);
        }
    }
}