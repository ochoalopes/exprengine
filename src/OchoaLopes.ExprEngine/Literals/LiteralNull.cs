using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Literals
{
    internal class LiteralNull : IExpression
    {
        public object? Value { get; }

        public LiteralNull(object? value)
        {
            Value = value;
        }

        public object Evaluate(IDictionary<string, object> variables)
        {
            return Value;
        }
    }
}