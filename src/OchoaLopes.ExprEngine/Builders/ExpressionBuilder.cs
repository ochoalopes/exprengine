using System.Globalization;
using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Expressions;
using OchoaLopes.ExprEngine.Interfaces;
using OchoaLopes.ExprEngine.Literals;

namespace OchoaLopes.ExprEngine.Builders
{
    internal static class ExpressionBuilder
	{
		public static IExpression BuildExpression(TokenTypeEnum? type, IExpression left, IExpression right)
		{
            switch (type)
            {
                case TokenTypeEnum.Or:
                    return new Or(left, right);
                case TokenTypeEnum.And:
                    return new And(left, right);
                case TokenTypeEnum.Equal:
                    return new Equal(left, right);
                case TokenTypeEnum.NotEqual:
                    return new NotEqual(left, right);
                case TokenTypeEnum.GreaterThan:
                    return new GreaterThan(left, right);
                case TokenTypeEnum.GreaterThanOrEqual:
                    return new GreaterThanOrEqual(left, right);
                case TokenTypeEnum.LessThan:
                    return new LessThan(left, right);
                case TokenTypeEnum.LessThanOrEqual:
                    return new LessThanOrEqual(left, right);
                case TokenTypeEnum.Add:
                    return new Add(left, right);
                case TokenTypeEnum.BinaryMinus:
                    return new Subtract(left, right);
                case TokenTypeEnum.Multiply:
                    return new Multiply(left, right);
                case TokenTypeEnum.Divide:
                    return new Divide(left, right);
                case TokenTypeEnum.Modulo:
                    return new Modulo(left, right);
                case TokenTypeEnum.Like:
                    return new Like(left, right);
                case TokenTypeEnum.NotLike:
                    return new NotLike(left, right);
                default:
                    throw new InvalidOperationException($"Unknown token type: {type}");
            }
        }

        public static IExpression BuildLiteral(TokenTypeEnum? type, string value, CultureInfo cultureInfo)
        {
            switch (type)
            {
                case TokenTypeEnum.LiteralInteger:
                    return new LiteralInteger(int.Parse(value, cultureInfo));
                case TokenTypeEnum.LiteralDouble:
                    return new LiteralDouble(double.Parse(value, NumberStyles.Any, cultureInfo));
                case TokenTypeEnum.LiteralDecimal:
                    return new LiteralDecimal(decimal.Parse(value, NumberStyles.Any, cultureInfo));
                case TokenTypeEnum.LiteralFloat:
                    return new LiteralFloat(float.Parse(value, NumberStyles.Any, cultureInfo));
                case TokenTypeEnum.LiteralStringContains:
                    return new LiteralString(value.ToString(), TokenTypeEnum.LiteralStringContains);
                case TokenTypeEnum.LiteralStringEndsWith:
                    return new LiteralString(value.ToString(), TokenTypeEnum.LiteralStringEndsWith);
                case TokenTypeEnum.LiteralStringStartsWith:
                    return new LiteralString(value.ToString(), TokenTypeEnum.LiteralStringStartsWith);
                case TokenTypeEnum.LiteralString:
                    return new LiteralString(value.ToString(), TokenTypeEnum.LiteralString);
                case TokenTypeEnum.LiteralChar:
                    return new LiteralChar(char.Parse(value));
                case TokenTypeEnum.LiteralBoolean:
                    return new LiteralBool(bool.Parse(value));
                case TokenTypeEnum.LiteralNull:
                    return new LiteralNull(null);
                case TokenTypeEnum.LiteralDateTime:
                    return new LiteralDateTime(DateTime.Parse(value, cultureInfo));
                case TokenTypeEnum.Variable:
                    return new Variable(value);
                default:
                    throw new InvalidOperationException("Unexpected token.");
            }
        }
	}
}