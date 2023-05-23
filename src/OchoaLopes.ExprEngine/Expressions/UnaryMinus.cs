using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class UnaryMinus : UnaryOperation
    {
        public UnaryMinus(IExpression operand) : base(operand) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var result = Operand.Evaluate(variables);

            if (result is float resultFloat)
            {
                return -resultFloat;
            }
            else if (result is decimal resultDecimal)
            {
                return -resultDecimal;
            }
            else if (result is double resultDouble)
            {
                return -resultDouble;
            }
            else if (result is int resultInt)
            {
                return -resultInt;
            }
            else
            {
                throw new InvalidOperationException("Operand of a Negate operation must be of type decimal, double, or int.");
            }
        }
    }
}