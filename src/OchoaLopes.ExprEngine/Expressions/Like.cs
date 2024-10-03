using OchoaLopes.ExprEngine.Helpers;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Literals;
using OchoaLopes.ExprEngine.Operations;
using OchoaLopes.ExprEngine.Validators;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class Like : BinaryOperation
    {
        public Like(IExpression left, IExpression right) : base(left, right) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var leftResult = Left.Evaluate(variables);
            var rightResult = Right.Evaluate(variables);

            ExpressionValidator.ValidateLike(leftResult, rightResult);

            var type = Right.GetType() == typeof(LiteralString) ? (LiteralString)(Right) : null;

            var leftString = leftResult?.ToString() ?? string.Empty;
            var rightString = rightResult?.ToString() ?? string.Empty;

            return OperationHelper.OperateLikeString(type?.Type, leftString, rightString);
        }
    }
}