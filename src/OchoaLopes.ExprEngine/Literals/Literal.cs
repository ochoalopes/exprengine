using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Literals
{
    internal abstract class Literal : IExpression
    {
        public object Value { get; }

        public Literal(object value)
        {
            Value = value;
        }

        public abstract object Evaluate(IDictionary<string, object> variables);
    }
}