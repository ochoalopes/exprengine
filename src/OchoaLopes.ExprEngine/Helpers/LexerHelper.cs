using System.Globalization;
using System.Text;
using OchoaLopes.ExprEngine.Enums;
using OchoaLopes.ExprEngine.Validators;

namespace OchoaLopes.ExprEngine.Helpers
{
    internal static class LexerHelper
    {
        public static TokenTypeEnum GetTokenType(string token, CultureInfo cultureInfo)
        {
            switch (token.ToLower())
            {
                case var _ when TokenValidator.IsVariable(token):
                    return TokenTypeEnum.Variable;
                case var _ when TokenValidator.IsLeftParenthesis(token):
                    return TokenTypeEnum.LeftParenthesis;
                case var _ when TokenValidator.IsRightParenthesis(token):
                    return TokenTypeEnum.RightParenthesis;
                case var _ when TokenValidator.IsAdd(token):
                    return TokenTypeEnum.Add;
                case var _ when TokenValidator.IsAnd(token):
                    return TokenTypeEnum.And;
                case var _ when TokenValidator.IsDivide(token):
                    return TokenTypeEnum.Divide;
                case var _ when TokenValidator.IsEqual(token):
                    return TokenTypeEnum.Equal;
                case var _ when TokenValidator.IsGreaterThan(token):
                    return TokenTypeEnum.GreaterThan;
                case var _ when TokenValidator.IsGreaterThanOrEqual(token):
                    return TokenTypeEnum.GreaterThanOrEqual;
                case var _ when TokenValidator.IsLessThan(token):
                    return TokenTypeEnum.LessThan;
                case var _ when TokenValidator.IsLessThanOrEqual(token):
                    return TokenTypeEnum.LessThanOrEqual;
                case var _ when TokenValidator.IsModulo(token):
                    return TokenTypeEnum.Modulo;
                case var _ when TokenValidator.IsMutiply(token):
                    return TokenTypeEnum.Multiply;
                case var _ when TokenValidator.IsNot(token):
                    return TokenTypeEnum.Not;
                case var _ when TokenValidator.IsNotEqual(token):
                    return TokenTypeEnum.NotEqual;
                case var _ when TokenValidator.IsOr(token):
                    return TokenTypeEnum.Or;
                case var _ when TokenValidator.IsSubtract(token):
                    return TokenTypeEnum.Subtract;
                case var _ when TokenValidator.IsLiteralInteger(token):
                    return TokenTypeEnum.LiteralInteger;
                case var _ when TokenValidator.IsLiteralDouble(token, cultureInfo):
                    return TokenTypeEnum.LiteralDouble;
                case var _ when TokenValidator.IsLiteralDecimal(token, cultureInfo):
                    return TokenTypeEnum.LiteralDecimal;
                case var _ when TokenValidator.IsLiteralFloat(token, cultureInfo):
                    return TokenTypeEnum.LiteralFloat;
                case var _ when TokenValidator.IsLiteralString(token):
                    return TokenTypeEnum.LiteralString;
                case var _ when TokenValidator.IsLiteralChar(token):
                    return TokenTypeEnum.LiteralChar;
                case var _ when TokenValidator.IsLiteralBoolean(token):
                    return TokenTypeEnum.LiteralBoolean;
                case var _ when TokenValidator.IsLiteralNull(token):
                    return TokenTypeEnum.LiteralNull;
                case var _ when TokenValidator.IsLiteralDate(token, cultureInfo):
                    return TokenTypeEnum.LiteralDateTime;
                default:
                    throw new InvalidOperationException($"Unknown token type: {token}");
            }
        }

        public static string CleanUpTokenValue(TokenTypeEnum type, string token)
        {
            token = RemoveSingleQuotes(type, token);
            token = RemovePrefix(type, token);
            token = RemoveSuffix(type, token);
            token = RemoveDateFormat(type, token);

            return token;
        }

        #region Private Methods
        private static string RemoveSuffix(TokenTypeEnum type, string token)
        {
            var types = new TokenTypeEnum[] {
                TokenTypeEnum.LiteralInteger,
                TokenTypeEnum.LiteralDouble,
                TokenTypeEnum.LiteralDecimal,
                TokenTypeEnum.LiteralFloat,
                TokenTypeEnum.LiteralDateTime
            };

            if (types.Contains(type))
            {
                return token.Substring(0, token.Length - 1).Trim();
            }

            return token.Trim();
        }

        private static string RemovePrefix(TokenTypeEnum type, string token)
        {
            if (type == TokenTypeEnum.Variable)
            {
                return token.Substring(1).Trim();
            }

            return token.Trim();
        }

        private static string RemoveSingleQuotes(TokenTypeEnum type, string token)
        {
            var types = new TokenTypeEnum[] {
                TokenTypeEnum.LiteralString,
                TokenTypeEnum.LiteralDateTime
            };

            if (types.Contains(type))
            {
                return token.Replace("'", "").Trim();
            }

            return token.Trim();
        }

        private static string RemoveDateFormat(TokenTypeEnum type, string token)
        {
            if (type == TokenTypeEnum.LiteralDateTime)
            {
                return token.TrimEnd('t').Trim('\'');
            }

            return token;
        }
        #endregion
    }
}