using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;
using OchoaLopes.ExprEngine.Validators;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class Not : UnaryOperation
    {
        public Not(IExpression operand) : base(operand) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var result = Operand.Evaluate(variables);

            ExpressionValidator.ValidateNot(result);

            return !(bool)result;
        }
    }
}