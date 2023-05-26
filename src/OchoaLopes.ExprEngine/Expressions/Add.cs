using OchoaLopes.ExprEngine.Helpers;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;
using OchoaLopes.ExprEngine.Validators;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class Add : BinaryOperation
    {
        public Add(IExpression left, IExpression right) : base(left, right) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var leftResult = Left.Evaluate(variables);
            var rightResult = Right.Evaluate(variables);

            ExpressionValidator.ValidateAdd(leftResult, rightResult);

            if (leftResult is string)
            {
                return OperationHelper.OperateAddString(leftResult, rightResult);
            }

            if (leftResult is DateTime)
            {
                return OperationHelper.OperateAddDate(leftResult, rightResult);
            }

            return OperationHelper.OperateNumbers(leftResult, rightResult, (a, b) => a + b);
        }
    }
}