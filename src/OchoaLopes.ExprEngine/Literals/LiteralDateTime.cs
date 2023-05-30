using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Literals
{
    internal class LiteralDateTime : IExpression
    {
        public DateTime Value { get; }

        public LiteralDateTime(DateTime value)
        {
            Value = value;
        }

        public object Evaluate(IDictionary<string, object> variables)
        {
            return Value;
        }
    }
}