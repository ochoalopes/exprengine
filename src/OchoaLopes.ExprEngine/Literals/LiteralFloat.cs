using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Literals
{
    internal class LiteralFloat : IExpression
    {
        public float Value { get; }

        public LiteralFloat(float value)
        {
            Value = value;
        }

        public object Evaluate(IDictionary<string, object> variables)
        {
            return Value;
        }
    }
}