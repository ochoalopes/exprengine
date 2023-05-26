using OchoaLopes.ExprEngine.Helpers;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;
using OchoaLopes.ExprEngine.Validators;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class LessThan : BinaryOperation
    {
        public LessThan(IExpression left, IExpression right) : base(left, right) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var leftResult = Left.Evaluate(variables);
            var rightResult = Right.Evaluate(variables);

            ExpressionValidator.ValidateLessThan(leftResult, rightResult);

            return OperationHelper.Compare(leftResult, rightResult) < 0;
        }
    }
}