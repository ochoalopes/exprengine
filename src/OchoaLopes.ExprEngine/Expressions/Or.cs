using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class Or : BinaryOperation
    {
        public Or(IExpression left, IExpression right) : base(left, right) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var leftResult = Left.Evaluate(variables);
            var rightResult = Right.Evaluate(variables);

            if (leftResult is bool leftBool && rightResult is bool rightBool)
            {
                return leftBool || rightBool;
            }
            else
            {
                throw new InvalidOperationException("Both operands of an Or operation must be booleans.");
            }
        }
    }
}