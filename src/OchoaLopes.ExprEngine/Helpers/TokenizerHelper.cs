using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Validators;

namespace OchoaLopes.ExprEngine.Helpers
{
    internal static class TokenizerHelper
    {
        public static TokenTypeEnum GetTokenType(string token)
        {
            switch (token.ToLower())
            {
                case var _ when TokenizerValidator.IsVariable(token):
                    return TokenTypeEnum.Variable;
                case var _ when TokenizerValidator.IsLeftParenthesis(token):
                    return TokenTypeEnum.LeftParenthesis;
                case var _ when TokenizerValidator.IsRightParenthesis(token):
                    return TokenTypeEnum.RightParenthesis;
                case var _ when TokenizerValidator.IsAdd(token):
                    return TokenTypeEnum.Add;
                case var _ when TokenizerValidator.IsAnd(token):
                    return TokenTypeEnum.And;
                case var _ when TokenizerValidator.IsDivide(token):
                    return TokenTypeEnum.Divide;
                case var _ when TokenizerValidator.IsEqual(token):
                    return TokenTypeEnum.Equal;
                case var _ when TokenizerValidator.IsGreaterThan(token):
                    return TokenTypeEnum.GreaterThan;
                case var _ when TokenizerValidator.IsGreaterThanOrEqual(token):
                    return TokenTypeEnum.GreaterThanOrEqual;
                case var _ when TokenizerValidator.IsLessThan(token):
                    return TokenTypeEnum.LessThan;
                case var _ when TokenizerValidator.IsLessThanOrEqual(token):
                    return TokenTypeEnum.LessThanOrEqual;
                case var _ when TokenizerValidator.IsModulo(token):
                    return TokenTypeEnum.Modulo;
                case var _ when TokenizerValidator.IsMutiply(token):
                    return TokenTypeEnum.Multiply;
                case var _ when TokenizerValidator.IsNot(token):
                    return TokenTypeEnum.Not;
                case var _ when TokenizerValidator.IsNotEqual(token):
                    return TokenTypeEnum.NotEqual;
                case var _ when TokenizerValidator.IsOr(token):
                    return TokenTypeEnum.Or;
                case var _ when TokenizerValidator.IsSubtract(token):
                    return TokenTypeEnum.Subtract;
                case var _ when TokenizerValidator.IsLiteralInteger(token):
                    return TokenTypeEnum.LiteralInteger;
                case var _ when TokenizerValidator.IsLiteralDouble(token):
                    return TokenTypeEnum.LiteralDouble;
                case var _ when TokenizerValidator.IsLiteralDecimal(token):
                    return TokenTypeEnum.LiteralDecimal;
                case var _ when TokenizerValidator.IsLiteralFloat(token):
                    return TokenTypeEnum.LiteralFloat;
                case var _ when TokenizerValidator.IsLiteralString(token):
                    return TokenTypeEnum.LiteralString;
                case var _ when TokenizerValidator.IsLiteralChar(token):
                    return TokenTypeEnum.LiteralChar;
                case var _ when TokenizerValidator.IsLiteralBoolean(token):
                    return TokenTypeEnum.LiteralBoolean;
                case var _ when TokenizerValidator.IsLiteralNull(token):
                    return TokenTypeEnum.LiteralNull;
                default:
                    throw new InvalidOperationException($"Unknown token type: {token}");
            }
        }

        public static string RemoveSuffix(TokenTypeEnum type,string token)
        {
            var types = new TokenTypeEnum[] {
                TokenTypeEnum.LiteralInteger,
                TokenTypeEnum.LiteralDouble,
                TokenTypeEnum.LiteralDecimal,
                TokenTypeEnum.LiteralFloat
            };

            if (types.Contains(type))
            {
                return token.Substring(0, token.Length - 1);
            }

            return token;
        }
    }
}