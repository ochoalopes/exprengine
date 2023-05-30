using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Literals
{
    internal class LiteralBool : IExpression
    {
        public bool Value { get; }

        public LiteralBool(bool value)
        {
            Value = value;
        }

        public object Evaluate(IDictionary<string, object> variables)
        {
            return Value;
        }
    }
}