using OchoaLopes.ExprEngine.Helpers;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;
using OchoaLopes.ExprEngine.Validators;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class Subtract : BinaryOperation
    {
        public Subtract(IExpression left, IExpression right) : base(left, right) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var leftResult = Left.Evaluate(variables);
            var rightResult = Right.Evaluate(variables);

            ExpressionValidator.ValidateSubtract(leftResult, rightResult);

            if (leftResult is DateTime)
            {
                return OperationHelper.OperateSubtractDate(leftResult, rightResult);
            }

            return OperationHelper.OperateNumbers(leftResult, rightResult, (a, b) => a - b);
        }
    }
}