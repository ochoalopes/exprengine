using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Operations;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class IsNotNull : UnaryOperation
    {
        public IsNotNull(IExpression operand) : base(operand) { }

        public override object Evaluate(IDictionary<string, object> variables)
        {
            return Operand.Evaluate(variables) != null;
        }
    }
}