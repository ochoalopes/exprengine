using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Expressions
{
    internal class Literal : IExpression
    {
        public object Value { get; }

        public Literal(object value)
        {
            Value = value;
        }

        public object Evaluate(IDictionary<string, object> variables)
        {
            return Value;
        }
    }
}