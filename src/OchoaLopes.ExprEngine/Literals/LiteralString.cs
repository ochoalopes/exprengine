using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Interfaces;

namespace OchoaLopes.ExprEngine.Literals
{
    internal class LiteralString : IExpression
    {
        public string Value { get; }
        public TokenTypeEnum Type { get; }

        public LiteralString(string value, TokenTypeEnum type = TokenTypeEnum.LiteralString)
        {
            Value = value;
            Type = type;
        }

        public object Evaluate(IDictionary<string, object> variables)
        {
            return Value;
        }
    }
}