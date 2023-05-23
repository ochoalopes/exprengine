using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class Not : UnaryOperation
    {
        public Not(IExpression operand) : base(operand) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var result = Operand.Evaluate(variables);

            if (result is bool boolResult)
            {
                return !boolResult;
            }
            else
            {
                throw new InvalidOperationException("The operand of a Not operation must be a boolean.");
            }
        }
    }
}